﻿using Joinup.API.Models;
using Joinup.Common.Models;
using Joinup.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Joinup.API.Helpers
{
    public class UsersHelper:IDisposable
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static DataContext db = new DataContext();

        public static Response CreateUserASP(UserRequest userRequest)
        {
            try
            {
                var userManager = new UserManager<ApplicationUser>( new UserStore<ApplicationUser>( userContext ) );
                var oldUserASP = userManager.FindByEmail( userRequest.Email );
                if ( oldUserASP != null )
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message="001.El usuario ya existe"
                    };
                }

                var userASP = new ApplicationUser
                {
                    Email = userRequest.Email,
                    UserName = userRequest.Email,
                };

                var result = userManager.Create( userASP, userRequest.Password );
                if ( result.Succeeded )
                {
                    var newUserASP = userManager.FindByEmail(userRequest.Email);
                    userManager.AddClaim( newUserASP.Id, new Claim( ClaimTypes.GivenName, userRequest.Name ) );
                    userManager.AddClaim( newUserASP.Id, new Claim( ClaimTypes.Name, userRequest.Surname ) );
                    if ( !string.IsNullOrEmpty( userRequest.ImagePath ) )
                    {
                        userManager.AddClaim( newUserASP.Id, new Claim( ClaimTypes.Uri, userRequest.ImagePath ) );
                    }

                    return new Response
                    {
                        IsSuccess = true,
                    };
                }

                var errors = string.Empty;
                foreach ( var error in result.Errors )
                {
                    errors += error + "\n";
                }
                return new Response
                {
                    IsSuccess = false,
                    Message = errors,
                };
            }
            catch ( Exception exc )
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = exc.Message.ToString()
                };
            }
        }

        public void Dispose()
        {
            userContext.Dispose();
            //db.Dispose();
        }
    }
}