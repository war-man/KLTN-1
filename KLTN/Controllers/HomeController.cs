﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KLTN.Models;
using Microsoft.AspNetCore.Identity;
using Data.Models;

namespace KLTN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginRequest { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (ModelState.IsValid)
            {
                
                    var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        var checkingUser = _userManager.FindByNameAsync(model.Username);
                        if ((bool)checkingUser.Result.IsEnabled)
                        {
                            var user = await _userManager.FindByNameAsync(model.Username);
                            var role = await _userManager.GetRolesAsync(user);

                            if (role[0] == "QuanLy" || role[0] == "Administrators")
                            {
                                return RedirectToAction("Index", "Home", new { area = "Admin" });
                            }
                            else if (role[0] == "GVHD")
                            {
                                return RedirectToAction("Index", "Home", new { area = "GVHD" });
                            }
                            else if (role[0] == "SinhVien")
                            {
                                return RedirectToAction("Index", "Home", new { area = "SinhVien" });
                            }
                        }
                    }
                
            }
            ModelState.AddModelError("", "Lỗi đăng nhập");
            return View(model);
        }
        //public async Task<IActionResult> Authencate (LoginRequest request)
        //{
        //    var user = await _userManager.FindByNameAsync(request.UserName);
        //    if (user == null) return false;
        //    var result = await _signInManager.PasswordSignInAsync(user, request.PassWord, request.RememberMe, true);
        //    if (!result.Succeeded) return false;
        //    var claims = new[]
        //    {
        //        new Claims
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
