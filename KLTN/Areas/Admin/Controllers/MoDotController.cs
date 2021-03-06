﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Interfaces;
using Data.Enum;
using Data.Models;

namespace KLTN.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoDotController : Controller
    {
        private readonly IMoDot _service;
        private readonly IXetDuyetDanhGia _serviceXDDG;
        private readonly INhom _serviceNhom;
        private readonly IHoiDong _serviceHoiDong;
        private readonly IDeTaiNghienCuu _serviceDeTai;

        public MoDotController(IMoDot service, INhom serviceNhom, IDeTaiNghienCuu serviceDeTai, IXetDuyetDanhGia serviceXDDG, IHoiDong serviceHoiDong)
        {
            _serviceNhom = serviceNhom;
            _serviceHoiDong = serviceHoiDong;
            _serviceXDDG = serviceXDDG;
            _service = service;
            _serviceDeTai = serviceDeTai;
        }

        public async Task<IActionResult> Index(string mess)
        {
            IEnumerable<MoDot> listDotDangKy = await _service.GetAll(x => x.Loai == (int)MoDotLoai.DangKy);
            if(!listDotDangKy.Any())
            {
                return View();
            }
            MoDot DotDangKyMoiNhat = listDotDangKy.ToList().Last();

            MoDot moDot = await _service.GetEntity(x => x.Status == (int)MoDotStatus.Mo);
            DeTaiNghienCuu detai = await _serviceDeTai.GetEntity(x => x.NgayThucHien != null && x.NgayDangKy > DotDangKyMoiNhat.ThoiGianBd && x.NgayDangKy < DotDangKyMoiNhat.ThoiGianKt);
            if(mess != "")
            {
                ViewBag.mess = mess;
            }
            if(detai != null)
            {
                ViewBag.NgayBdDeTai = detai.NgayThucHien.Value.ToString("yyyy-MM-dd'T'HH:mm:ss");
                ViewBag.NgayKtDeTai = detai.NgayKetThuc.Value.ToString("yyyy-MM-dd'T'HH:mm:ss");
            }

            if(moDot != null)
            {
                if(DateTime.Now > moDot.ThoiGianKt.Value)
                {
                    moDot.Status = (int)MoDotStatus.Dong;
                    await _service.Update(moDot);
                }
            }

            return View(moDot);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MoDot moDot)
        {
            var allXDDG = await _serviceXDDG.GetAll();
            if (allXDDG.Any())
            {
                foreach (var item in allXDDG)
                {
                    item.Status = 0;
                    await _serviceXDDG.Update(item);
                }
            }
            var HoiDong = await _serviceHoiDong.GetAll();
            if (HoiDong.Any())
            {
                foreach (var item in HoiDong)
                {
                    item.StatusPhanCong = (int)StatusPhanCong.ChuaPhanCong;
                    await _serviceHoiDong.Update(item);
                }
            }
            var DeTai = await _serviceDeTai.GetAll();
            if (DeTai.Any())
            {
                foreach (var item in DeTai)
                {
                    item.TinhTrangPhanCong = (int)StatusPhanCong.ChuaPhanCong;
                    await _serviceDeTai.Update(item);
                }
            }
            if(moDot.Loai == (int)MoDotLoai.DangKy)
            {
                var Nhom = await _serviceNhom.GetAll();
                if(Nhom.Any())
                {
                    foreach (var item in Nhom)
                    {
                        item.Status = (int)BaseStatus.Disable;
                        await _serviceNhom.Update(item);
                    }
                }
            }
            moDot.Status = 1;
            moDot.IdquanLy = long.Parse(User.Identity.Name);
            await _service.Add(moDot);
            return RedirectToAction("Index", new { mess = "Mở đợt thành công" });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MoDot moDot)
        {
            
            await _service.Update(moDot);
            return RedirectToAction("Index", new { mess = "Cập nhật thành công" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            MoDot moDot = await _service.GetById(id);
            moDot.Status = 0;
            await _service.Update(moDot);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTGThucHienDeTai(DateTime NgayBdDeTai, DateTime NgayKtDeTai)
        {
            IEnumerable<MoDot> listDotDangKy = await _service.GetAll(x => x.Loai == (int)MoDotLoai.DangKy);
            MoDot DotDangKyMoiNhat = listDotDangKy.ToList().Last();

            IEnumerable<DeTaiNghienCuu> deTaiNghienCuus = await _serviceDeTai.GetAll(x => x.NgayDangKy > DotDangKyMoiNhat.ThoiGianBd && x.NgayDangKy < DotDangKyMoiNhat.ThoiGianKt);
            if(deTaiNghienCuus.Any())
            {
                foreach (var item in deTaiNghienCuus)
                {
                    item.NgayThucHien = NgayBdDeTai;
                    item.NgayKetThuc = NgayKtDeTai;
                    await _serviceDeTai.Update(item);
                }
                return RedirectToAction("Index", new { mess = "Tạo thời gian thực hiện thành công" });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditTGThucHienDeTai(DateTime NgayBdDeTai, DateTime NgayKtDeTai)
        {
            IEnumerable<MoDot> listDotDangKy = await _service.GetAll(x => x.Loai == (int)MoDotLoai.DangKy);
            MoDot DotDangKyMoiNhat = listDotDangKy.ToList().Last();

            IEnumerable<DeTaiNghienCuu> deTaiNghienCuus = await _serviceDeTai.GetAll(x => x.NgayDangKy > DotDangKyMoiNhat.ThoiGianBd && x.NgayDangKy < DotDangKyMoiNhat.ThoiGianKt);
            if (deTaiNghienCuus.Any())
            {
                foreach (var item in deTaiNghienCuus)
                {
                    item.NgayThucHien = NgayBdDeTai;
                    item.NgayKetThuc = NgayKtDeTai;
                    await _serviceDeTai.Update(item);
                }
            }

            return RedirectToAction("Index", new { mess = "Cập nhật thành công" });
        }
    }
}