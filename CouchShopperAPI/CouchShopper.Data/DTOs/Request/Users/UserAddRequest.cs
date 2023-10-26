using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Request.Users
{
    public class UserAddRequest : BaseRequest
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public bool IsSeller { get; set; }

    }
}
