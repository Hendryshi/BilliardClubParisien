namespace BCP.Domain.Configuration
{
    public class SMTPOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
        public string Login { get; set; }
        public string Pwd { get; set; }
        public bool EnableSsl { get; set; }
        public SMTPSenderOptions Sender { get; set; }
        public string DefaultReceiver { get; set; }
    }

    public class SMTPSenderOptions
    {
        public string FromAdress { get; set; }
        public string FromNoResponseAddress { get; set; }
    }
}
