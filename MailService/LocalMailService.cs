namespace CityInfoApi.MailService
{
    public class LocalMailService : IMailService
    {
        private string _mailTo;
        private string _mailFrom;
        public LocalMailService(IConfiguration configuration)
        {
            _mailTo = configuration["mailSettings:mailToAddress"];
            _mailFrom = configuration["mailSettings:mailFromAddress"];

        }
        public void SendMail(string message)
        {
            Console.WriteLine($"Subject : {message}");
            
            Console.WriteLine($"Email sent from {_mailFrom} to {_mailTo} in {nameof(LocalMailService)}");
        }
    }
  
}
