using Joinup.API.Helpers;
using Joinup.Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;


namespace Joinup.API.Controllers
{
    public class UsersController : ApiController
    {
        public IHttpActionResult PostUser(UserRequest pUserRequest)
        {
            if ( pUserRequest.ImageArray != null && pUserRequest.ImageArray.Length > 0 )
            {
                var stream = new MemoryStream( pUserRequest.ImageArray );
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Users";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto( stream, folder, file );

                if ( response )
                {
                    pUserRequest.ImagePath = fullPath;
                }
            }

            var answer = UsersHelper.CreateUserASP( pUserRequest );

            if ( answer.IsSuccess )
            {
                return Ok( answer );
            }

            return BadRequest(answer.Message);
        }
        
    }
}