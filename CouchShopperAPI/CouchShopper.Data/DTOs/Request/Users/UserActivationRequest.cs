using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Users
{
    public class UserActivationRequest
    {
        public string Username { get; set; }

        public string Code { get; set; }
    }
}
