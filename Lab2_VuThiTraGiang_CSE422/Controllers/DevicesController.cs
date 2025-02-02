using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab2_VuThiTraGiang_CSE422.Data;
using Lab2_VuThiTraGiang_CSE422.Models;

namespace Lab2_VuThiTraGiang_CSE422.Controllers
{
    public class DevicesController : Controller
    {
        private readonly DeviceContext context;

        public DevicesController(DeviceContext context)
        {
            this.context = context;
        }

        // GET: Devices
        public IActionResult Index(string searchString)
        {
            var devices = context.Devices.Include(d => d.Category).ToList();
            if(!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                devices = devices.Where(d => d.Name.ToLower().Contains(searchString) || d.Code.ToLower().Contains(searchString)).ToList();
            }
            return View(devices);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(context.DeviceCategories.ToList(), "ID", "Name");

            return View();
        }


        [HttpPost]
        public IActionResult Create(DeviceDto deviceDto)
        {
            if (deviceDto.Name == null)
            {
                ModelState.AddModelError("Name", "The Name is required");
            }
            if (deviceDto.Code == null)
            {
                ModelState.AddModelError("Code", "The Code is required");
            }
            if (!ModelState.IsValid)
            {
                return View(deviceDto);
            }

            Device device = new()
            {
                Name = deviceDto.Name,
                Code = deviceDto.Code,
                CategoryID = deviceDto.CategoryID,
                Status = deviceDto.Status
            };

            context.Devices.Add(device);
            context.SaveChanges();

            return RedirectToAction("Index", "Devices");
        }


        public IActionResult Edit(int id)
        {
            var device = context.Devices.Find(id);
            if (device == null)
            {
                return RedirectToAction("Index", "Devices");
            }

            var deviceCat = context.DeviceCategories.Find(device.CategoryID);
            if (deviceCat != null)
            {
                ViewData["CategoryName"] = deviceCat.Name;
            }

            var statusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "In use", Text = "In use" },
                new SelectListItem { Value = "Broken", Text = "Broken" },
                new SelectListItem { Value = "Under maintenance", Text = "Under maintenance" }
            };

            ViewBag.StatusOptions = new SelectList(statusOptions, "Value", "Text");

            //create dto 
            var deviceDto = new DeviceDto()
            {
                Name = device.Name,
                Code = device.Code,
                CategoryID = device.CategoryID,
                Status = device.Status
            };

            ViewData["ID"] = device.ID;
            ViewData["Name"] = device.Name;
            ViewData["Code"] = device.Code;
            ViewData["Status"] = device.Status;
            ViewData["CategoryID"] = device.CategoryID;



            return View(deviceDto);
        }


        [HttpPost]
        public IActionResult Edit(int id, DeviceDto deviceDto)
        {
            var device = context.Devices.Find(id);

            if (device == null)
            {
                return RedirectToAction("Index", "Devices");
            }

            if (deviceDto.Name == null)
            {
                ModelState.AddModelError("Name", "The Name is required");
            }

            if (deviceDto.Code == null)
            {
                ModelState.AddModelError("Code", "The Code is required");
            }         

            if (!ModelState.IsValid)
            {
                ViewData["Name"] = device.Name;
                ViewData["Code"] = device.Code;
                return View(deviceDto);
            }

            //update data
            device.Name = deviceDto.Name;
            device.Code = deviceDto.Code;
            device.Status = deviceDto.Status;
            context.Devices.Update(device);
            context.SaveChanges();

            return RedirectToAction("Index", "Devices");
        }

        public IActionResult Delete(int id)
        {
            var device = context.Devices.Find(id);

            if (device == null)
            {
                return RedirectToAction("Index", "Devices");
            }

            context.Devices.Remove(device);
            context.SaveChanges();

            return RedirectToAction("Index", "Devices");
        }
    }
}
