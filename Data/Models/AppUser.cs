﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public bool? IsEnabled { get; set; }
    }
}
