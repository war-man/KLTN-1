﻿using Data.Enum;
using KLTN.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KLTN.Views.Shared.Components.ListBaiPost
{
    public class ListBaiPostViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TabListViewModel ViewX)
        {
            if (ViewX.ViewType == (int)ViewType.ThaoLuanGV)
            {
                ViewX.tabCongKhai = ViewX.ListBaiPostThaoLuan.Where(x => x.Loai == (int)BaiPostType.CongKhai);
                ViewX.tabRiengTu = ViewX.ListBaiPostThaoLuan.Where(x => x.Loai == (int)BaiPostType.RiengTu);
            }
            else
            {
                
            }
            return View(ViewX);
        }
    }
}
