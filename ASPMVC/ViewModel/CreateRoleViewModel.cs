﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPMVC.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required(ErrorMessage = "Role name is required!")]
        [Display(Name = "Role name")]
        public string RoleName { get; set; }
    }
}
