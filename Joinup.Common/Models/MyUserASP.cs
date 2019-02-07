using Joinup.Common.Models.DatabaseModels;
using System;
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
        public string Name
        {
            get
            {
                if ( Claims.Count >= 1 )
                {
                    return Claims[0].ClaimValue;
                }
                return "Sin nombre";
            }
        }
        public string Surname
        {
            get
            {
                if ( Claims.Count >= 2 )
                {
                    return Claims[1].ClaimValue;
                }
                return "Sin apellido";
            }
        }
        public string UserImage
        {
            get
            {
                if ( Claims.Count >= 3 )
                {
                    return $"https://joinupapi.azurewebsites.net/{Claims[2].ClaimValue.Substring( 1 )}";
                }
                return "no_image.png";
            }
        }
        public string Image
        {
            get
            {
                if (Claims.Count >= 3)
                {
                    return Claims[2].ClaimValue;
                }
                return "no_image.png";
            }
        }
        public bool IsHost
        {
            get;
            set;
        }
        public User ToUser()
        {
            return new User { UserId = Id, Name = Name, Surname = Surname, Image = Image, Email = Email };
        }
    }
}