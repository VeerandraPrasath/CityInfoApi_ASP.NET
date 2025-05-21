namespace CityInfoApi.MailService
{
    public class CloudMailService : IMailService
    {
        private string _mailTo;
        private string _mailFrom;
        public CloudMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];

        }
        public void SendMail(string message)
        {
            Console.WriteLine($"Subject :{message}");
            Console.WriteLine($"Mail sent from {_mailFrom} to {_mailTo} in {nameof(CloudMailService)}"); 
        }
    }
}
