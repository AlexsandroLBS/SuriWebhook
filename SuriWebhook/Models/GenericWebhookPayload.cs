namespace SuriWebhook.Models
{
    public class GenericWebhookPayload
    {
        public string id { get; set; }
        public string type { get; set; }
        public DateTime timestamp { get; set; }
        public Payload payload { get; set; }
    }
    public class Agent
    {
        public int Status { get; set; }
        public object DepartmentId { get; set; }
        public object DateRequest { get; set; }
        public object DateAnswer { get; set; }
        public object PlatformUserId { get; set; }
        public object Tags { get; set; }
        public int UnreadMessages { get; set; }
        public bool IsTransferred { get; set; }
    }

    public class Attendant
    {
        public object Id { get; set; }
        public object Name { get; set; }
        public object Email { get; set; }
        public int Status { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public string ChatbotId { get; set; }
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string DeviceId { get; set; }
        public string Username { get; set; }
        public object ResourceId { get; set; }
        public object ResourceUrl { get; set; }
        public int Status { get; set; }
        public string StatusMessage { get; set; }
        public int Type { get; set; }
        public int Provider { get; set; }
        public object WhitelistedDomains { get; set; }
        public object PersistentMenu { get; set; }
        public object Greeting { get; set; }
        public object MessengerChannelId { get; set; }
        public object GupshupAppId { get; set; }
        public object GupshupPartnerAppToken { get; set; }
        public object WabaId { get; set; }
        public object PhoneNumberId { get; set; }
        public object FacebookAppId { get; set; }
        public PluginsData PluginsData { get; set; }
        public Data Data { get; set; }
    }

    public class CurrentDialog
    {
        public Uri Uri { get; set; }
        public Data Data { get; set; }
    }

    public class Data
    {
    }

    public class Message
    {
        public object Author { get; set; }
        public string mid { get; set; }
        public int seq { get; set; }
        public string text { get; set; }
        public object quick_reply { get; set; }
        public bool is_echo { get; set; }
        public object app_id { get; set; }
        public bool CurrentApp { get; set; }
        public object metadata { get; set; }
        public object attachments { get; set; }
        public object reply_to { get; set; }
        public bool is_deleted { get; set; }
    }

    public class Parameters
    {
        public string intent { get; set; }
    }

    public class Payload
    {
        public User user { get; set; }
        public Message Message { get; set; }
        public Channel channel { get; set; }
        public Attendant attendant { get; set; }
    }

    public class PluginsData
    {
    }

    public class ProfilePicture
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

   

    public class Uri
    {
        public string Path { get; set; }
        public string Query { get; set; }
        public Parameters Parameters { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ChatbotId { get; set; }
        public string ChannelId { get; set; }
        public int ChannelType { get; set; }
        public string Phone { get; set; }
        public object Email { get; set; }
        public ProfilePicture ProfilePicture { get; set; }
        public int Gender { get; set; }
        public object IdentificationDocument { get; set; }
        public object Note { get; set; }
        public object DefaultDepartmentId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime LastActivity { get; set; }
        public object Tags { get; set; }
        public object UserTags { get; set; }
        public CurrentDialog CurrentDialog { get; set; }
        public Agent Agent { get; set; }
    }


}
