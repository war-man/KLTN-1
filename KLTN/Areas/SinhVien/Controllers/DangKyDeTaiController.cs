﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Bo;
using Data.Interfaces;
using Data.Models;

namespace KLTN.Areas.SinhVien.Controllers
{
    [Area("SinhVien")]
    public class DangKyDeTaiController : Controller
    {
        private readonly IDeTaiNghienCuu _service;
        public DangKyDeTaiController (IDeTaiNghienCuu service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }
        
    }
}