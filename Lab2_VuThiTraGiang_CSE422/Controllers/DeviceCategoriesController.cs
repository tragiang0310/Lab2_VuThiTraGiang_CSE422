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
    public class DeviceCategoriesController : Controller
    {
        private readonly DeviceContext context;

        public DeviceCategoriesController(DeviceContext context)
        {
            this.context = context;
        }

        // GET: DeviceCategories
        public IActionResult Index()
        {
            var deviceCategories = context.DeviceCategories.ToList();
            return View(deviceCategories);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(DeviceCategoryDto deviceCategoryDto)
        {
            if(deviceCategoryDto.Name == null)
            {
                ModelState.AddModelError("Name", "The Name is required");
            }

            if(!ModelState.IsValid)
            {
                return View(deviceCategoryDto);
            }

            //save to database
            DeviceCategory deviceCategory = new DeviceCategory()
            {
                Name = deviceCategoryDto.Name,
                Description = deviceCategoryDto.Description ?? "",
            };
             context.DeviceCategories.Add(deviceCategory);
            context.SaveChanges();

            return RedirectToAction("Index", "DeviceCategories");
        }


        public IActionResult Edit(int id)
        {
            var deviceCategory = context.DeviceCategories.Find(id);
          
            if (deviceCategory == null)
            {
                return RedirectToAction("Index", "DeviceCategories");
            }

            //create dto 
            var deviceCategoryDto = new DeviceCategoryDto()
            {
                Name = deviceCategory.Name,
                Description = deviceCategory.Description,
            };

            ViewData["ID"] = deviceCategory.ID;
            ViewData["Name"] = deviceCategory.Name;
            ViewData["Description"] = deviceCategory.Description;

            return View(deviceCategoryDto);
        }

        
        [HttpPost]
        public IActionResult Edit(int id, DeviceCategoryDto deviceCategoryDto)
        {
            var deviceCategory = context.DeviceCategories.Find(id);

            if (deviceCategory == null)
            {
                return RedirectToAction("Index", "DeviceCategories");
            }

            if (deviceCategoryDto.Name == null)
            {
                ModelState.AddModelError("Name", "The Name is required");
            }

            if (!ModelState.IsValid)
            {
                ViewData["Name"] = deviceCategory.Name;
                ViewData["Description"] = deviceCategory.Description;
                return View(deviceCategoryDto);
            }

            //update data
            deviceCategory.Name = deviceCategoryDto.Name;
            deviceCategory.Description = deviceCategoryDto.Description;
            context.DeviceCategories.Update(deviceCategory);
            context.SaveChanges();

            return RedirectToAction("Index", "DeviceCategories");
        }

        public IActionResult Delete(int id)
        {
            var deviceCategory = context.DeviceCategories.Find(id);

            if (deviceCategory == null)
            {
                return RedirectToAction("Index", "DeviceCategories");
            }

            context.DeviceCategories.Remove(deviceCategory);
            context.SaveChanges();

            return RedirectToAction("Index", "DeviceCategories");
        }
    }
}
