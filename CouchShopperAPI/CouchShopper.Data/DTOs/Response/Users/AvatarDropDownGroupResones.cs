using System.Collections.Generic;

namespace CouchShopper.Data.DTOs.Response.Users
{
    public class AvatarDropDownGroupResones
    {
        public string GroupName { get; set; }

        public List<DropDownItemWithNotificationResponse> DropDownItems { get; set; }

    }
}
