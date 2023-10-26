namespace CouchShopper.Data.DTOs.Response.Users
{
    public class DropDownItemWithNotificationResponse
    {
        public string Name { get; set; }

        public string IconName { get; set; }

        public string Action { get; set; }

        public bool AlwaysShow { get; set; }

        public int? NumberOfNotifications { get; set; }

    }
}
