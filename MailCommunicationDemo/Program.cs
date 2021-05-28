using MailCommunicationUtils.Classes;
using MailCommunicationUtils.Enums;
using System;
using System.IO;

namespace MailCommunicationDemo
{
    class Program
    {
        private static Deserialize _deserialize = new Deserialize();
        private static SMTPMailMessage sMTPMailMessage = new SMTPMailMessage("apikey",
            "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",
            "smtp.sendgrid.net");
        static void Main(string[] args)
        {
            PrintMenu();
        }

        static void PrintMenu(bool clearScreen = false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (clearScreen) Console.Clear();
            Console.Write("Choose an option:");
            Console.WriteLine("1) Stuur mail zonder bijlagen");
            Console.WriteLine("2) Stuur mail met bijlagen");
            Console.WriteLine("3) Stuur subscription mail");
            Console.Write("Select an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    {
                        SendMail(false);
                        PrintMenu();
                        break;
                    }
                case "2":
                    {
                        SendMail(true);
                        PrintMenu();
                        break;
                    }
                case "3":
                    {
                        SendSubscriptionConfirmation(@"D:\Vives\Kwartaal 4\Programming Applictaion software\Taken\Taak_3_Jelle_Dispersyn\SMTP\bin\Debug\net5.0\Info.html", "Jelle", "jelle.dispersyn@student.vives.be");
                        break;
                    }
                case "4":
                    {
                        SendSubscriptionConfirmation<Person>("geadresseerden.json", @"D:\Vives\Kwartaal 4\Programming Applictaion software\Taken\Taak_3_Jelle_Dispersyn\SMTP\MailCommunicationDemo\bin\Debug\net5.0", "json");
                        break;
                    }
                default:
                    break;
            }
        }
        static void SendMail(bool withContents)
        {
            var receiverAddress = "jelle.dispersyn@student.vives.be";

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Sending mail via Sendgrif SMTP Relay {receiverAddress}");
            var msg = sMTPMailMessage.CreateMail(receiverAddress, "Dit is een test", "test mail");
            if (withContents == true) { msg = sMTPMailMessage.CreateMail(receiverAddress, "Dit is een test", "test mail", "Attach1.txt", "Attach2.txt"); }

            var result = sMTPMailMessage.sendMessage(msg);

            if (result.Status == MailSendingStatus.OK)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Message send to {receiverAddress}");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(result.Message);
                return;
            }
        }

        static void SendSubscriptionConfirmation(string path, string name, string email)
        {
            string html = File.ReadAllText(path);
            string body = String.Format(html, name, email);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Sending mail via Sendgrif SMTP Relay {email}");

            var msg = sMTPMailMessage.CreateMail(email, body, "Subscription");

            var result = sMTPMailMessage.sendMessage(msg);

            if (result.Status == MailSendingStatus.OK)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Message send to {email}");
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(result.Message);
                return;
            }

        }

        private static void SendSubscriptionConfirmation<T>(string fileName, string absolutefolderPath, string fileType)
        {

            var result = _deserialize.DeserializeObjectFromFile<PersonList>(absolutefolderPath, fileName, fileType);


        }
    }
}
