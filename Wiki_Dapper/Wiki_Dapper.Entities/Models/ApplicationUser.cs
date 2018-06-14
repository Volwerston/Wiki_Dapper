﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wiki_Dapper.Entities.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Login { get; set; }
    }
}
