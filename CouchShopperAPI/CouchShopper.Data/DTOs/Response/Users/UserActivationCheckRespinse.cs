using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.DTOs.Response.Users
{
    public class UserActivationCheckRespinse
    {
        public string Username { get; set; }
        public bool Active { get; set; }

    }
}
