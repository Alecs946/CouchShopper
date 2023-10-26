using System;

namespace CouchShopper.Data.DTOs.Response.Users
{
    public class UserResponse
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string ZipCode { get; set; }

    }
}
