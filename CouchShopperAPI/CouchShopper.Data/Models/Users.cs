using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouchShopper.Data.Models
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class Users : BaseEntity
    {
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }


        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public int Role { get; set; }

        public string ZipCode { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdatedBy { get; set; }

        public bool Active { get; set; }

        public string ActivationCode { get; set; }

        public double Balance { get; set; }

        public List<UserPayout> Payouts { get; set; }

        public List<UserEarning> Earnings { get; set; }

        public List<PaymentMethod> PaymentMethods { get; set; }

        public List<UserReview> Reviews { get; set; }

        public List<string> Favorites { get; set; }

        public Dictionary<string, DocumentAttachment>? _attachments { get; set; }

    }
}
