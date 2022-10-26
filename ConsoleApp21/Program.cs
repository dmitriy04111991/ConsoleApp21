using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Reflection;

namespace NetConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver(false, true, true);

            UnknownUser unknownUser = new UnknownUser();
            RegistrationClient registrationClient = new RegistrationClient();
            Company companyDelivery = new CompanyDelivery();
            unknownUser.Successor = registrationClient;
            registrationClient.Successor = companyDelivery;

            unknownUser.Handle(receiver);

            Console.Read();
        }
    }

    class Receiver
    {
        public bool UnknownUser { get; set; }
        
        public bool RegistrationClient { get; set; }
        
        public bool CompanyDelivery { get; set; }
        public Receiver(bool uu, bool rc, bool cd)
        {
            UnknownUser = uu;
            RegistrationClient = rc;
            CompanyDelivery = cd;
        }
    }
    abstract class unknownUser
    {
        public unknownUser Successor { get; set; }
        public abstract void Handle(Receiver receiver);
    }

    class UnknownUser : unknownUser
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.UnknownUser == true)
                Console.WriteLine("Новый пользователь");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }

    class RegistrationClient : registrationClient
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.RegistrationClient == true)
                Console.WriteLine("Регистрация");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
    class companyDelivery : CompanyDelivery
    {
        public override void Handle(Receiver receiver)
        {
            if (receiver.CompanyDelivery == true)
                Console.WriteLine("Успешный вход");
            else if (Successor != null)
                Successor.Handle(receiver);
        }
    }
    class Message
    {
        static void Main(string[] args)
        {

            SendEmailAsync().GetAwaiter();
            Console.Read();
        }

        private static async Task SendEmailAsync()
        {
            MailAddress from = new MailAddress("somemail@gmail.com", "Tom");
            MailAddress to = new MailAddress("somemail@yandex.ru");
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Тест";
            m.Body = "Письмо-тест 2 работы smtp-клиента";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("somemail@gmail.com", "mypassword");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            Console.WriteLine("Письмо отправлено");
        }
    }
}


