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
    public class UsersController : Controller
    {
        private readonly DeviceContext context;

        public UsersController(DeviceContext context)
        {
            this.context = context;
        }

        // GET: Users
        public IActionResult Index()
        {
            var users = context.Users.ToList();
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            if (userDto.FullName == null)
            {
                ModelState.AddModelError("FullName", "Full Name is required");
            }

            if (userDto.Email == null)
            {
                ModelState.AddModelError("Email", "Email is required");
            }

            if (userDto.PhoneNumber == null)
            {
                ModelState.AddModelError("PhoneNumber", "Phone Number is required");
            }

            if (!ModelState.IsValid)
            {
                return View(userDto);
            }

            //save to database
            User user = new User()
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber
            };
            context.Users.Add(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Edit(int id)
        {
            var user = context.Users.Find(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            //create dto 
            var userDto = new UserDto()
            {
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            ViewData["ID"] = user.ID;
            ViewData["FullName"] = user.FullName;
            ViewData["Email"] = user.Email;
            ViewData["PhoneNumber"] = user.PhoneNumber;

            return View(userDto);
        }


        [HttpPost]
        public IActionResult Edit(int id, UserDto userDto)
        {
            var user = context.Users.Find(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            if (userDto.FullName == null)
            {
                ModelState.AddModelError("FullName", "Full Name is required");
            }
            if (userDto.Email == null)
            {
                ModelState.AddModelError("Email", "The Email is required");
            }
            if (userDto.PhoneNumber == null)
            {
                ModelState.AddModelError("PhoneNumber", "The Phone Number is required");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ID"] = user.ID;
                ViewData["FullName"] = user.FullName;
                ViewData["Email"] = user.Email;
                ViewData["PhoneNumber"] = user.PhoneNumber;
                return View(userDto);
            }

            //update data
            user.FullName = userDto.FullName;
            user.Email = userDto.Email;
            user.PhoneNumber = userDto.PhoneNumber;
            context.Users.Update(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }

        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);

            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }

            context.Users.Remove(user);
            context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }
    }
}
