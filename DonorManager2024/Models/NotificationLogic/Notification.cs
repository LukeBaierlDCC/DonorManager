using System.Collections.Generic;

namespace DonorManager2024.Models.NotificationLogic
{
    public class Notification
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public List<NotificationApplicationUser> NotificationApplicationUsers { get; set; }
    }
}
