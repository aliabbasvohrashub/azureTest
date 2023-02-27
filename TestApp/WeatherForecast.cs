namespace TestApp
{
    public class AppSettings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
        public string IdentityServiceUrl { get; set; } = string.Empty;
        public bool UseVault { get; set; }
        public Vault? Vault { get; set; }
        public string[] CorsOrigins { get; set; } = new[] { string.Empty };
        public RedisCache? RedisCache { get; set; }
        public MailSettings? MailSettings { get; set; }
        public string? SendGridKey { get; set; }
        public string? DefaultFromEmail { get; set; }
        public string? DefaultFromEmailName { get; set; }
        public string? hostUrl { get; set; }
        public string? LockoutTimeSpanInMinutes { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
        public string TWILIO_ACCOUNT_SID { get; set; }
        public string TWILIO_AUTH_TOKEN { get; set; }
        public string SENDER_PHONE_NUMBER { get; set; }
        public string SENDER_WHATSAPP_NUMBER { get; set; }
        public string? WhatsAppPhoneNumberId { get; set; }
        public string? WhatsAppBusinessAccountId { get; set; }
        public string? WhatsAppTemporaryAccessToken { get; set; }
        public string? WhatsappUrl { get; set; }
        public string? BorrowerMailPriorPeriod { get; set; }
        public string? BorrowerMailPostPeriod { get; set; }
        public string? BorrowerMailPreTemplate { get; set; }
        public string? BorrowerMailPostTemplate { get; set; }

        public bool? SendEmail { get; set; }
        public string? BccEmailList { get; set; }
    }

    public class ConnectionStrings
    {
        public string Identity { get; set; } = string.Empty;
        public string Inspire { get; set; } = string.Empty;
        public string DbCon { get; set; } = string.Empty;
    }

    public class Vault
    {
        public string? Name { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
    }

    public class RedisCache
    {
        public string? InstanceName { get; set; }
        public string? HostAndPort { get; set; }
    }

    //TODO:Aliabbas change class to record
    public class MailSettings
    {
        public string? FromEmail { get; set; }
        public string? DisplayName { get; set; }
        public string? Host { get; set; }
        public int Port { get; set; }
        public string? Password { get; set; }
        public string? Bcc { get; set; }
    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    public class ChequeStatus
    {
       public string ChequeStatusId { get; set; }
       public string ChequeStatusName { get; set; }
       public DateTime DateAdded { get; set; }
       public DateTime DateEdited { get; set; }
       public string EditedBy { get; set; }
       public bool IsActive { get; set; }

    }

    /*
       `ChequeStatusId` varchar(36) NOT NULL,
  `ChequeStatusName` varchar(150) DEFAULT NULL,
  `DateAdded` datetime DEFAULT NULL,
  `DateEdited` datetime DEFAULT NULL,
  `AddedBy` varchar(36) DEFAULT NULL,
  `EditedBy` varchar(36) DEFAULT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ChequeStatusId`)
     */
}