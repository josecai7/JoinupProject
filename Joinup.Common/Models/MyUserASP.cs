﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joinup.Common.Models
{
    public class MyUserASP
    {
        public List<Claim> Claims { get; set; }
        public List<object> Logins { get; set; }
        public List<object> Roles { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public object PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public object LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }

        public string UserImage
        {
            get
            {
                if (Claims.Count>=3)
                {
                    return $"https://joinupapi.azurewebsites.net/{Claims[2].ClaimValue.Substring(1)}";
                }
                return "no_image.png";
            }
        }
    }
}