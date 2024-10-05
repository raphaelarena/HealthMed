namespace HealthMed.Configurations
{
    public class EmailSettings
    {
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
    }

}
