using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CouchShopper.Data.DTOs.Request.Account
{
    public class AccountProfilePictureUploadRequest
    {
        public string Username { get; set; }

        public string ProfilePicture { get; set; }
    }
}
