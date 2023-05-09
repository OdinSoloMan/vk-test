﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Infrastructure.Enums
{
    public enum UserGroupCode
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "User")]
        User = 2,
    }
}
