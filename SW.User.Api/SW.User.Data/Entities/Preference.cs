namespace SW.User.Data.Entities
{
    public class Preference
    {
        public int Id { get; set; }
        public bool DisplayPhoneNumber { get; set; }
        public bool ReceiveNotificationNewArticle { get; set; }
        public bool ReceiveEmail { get; set; }
    }
}
