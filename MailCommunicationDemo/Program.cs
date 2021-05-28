using MailCommunicationDemo.Classes;
using MailCommunicationUtils.Classes;
using MailCommunicationUtils.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace MailCommunicationDemo
{
    class Program
    {
        private static Deserialize _deserialize = new Deserialize();
        private static SMTPMailMessage sMTPMailMessage = new SMTPMailMessage("apikey",
            "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",
            "smtp.sendgrid.net");
        //private static AppSettingsService<DemoAppSettings> _appSettingsService = AppSettingsService<DemoAppSettings>.Instance;

        static void Main(string[] args)
        {
            PrintMenu();
        }

        static void PrintMenu(bool clearScreen = false)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (clearScreen) Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Stuur mail zonder bijlagen");
            Console.WriteLine("2) Stuur mail met bijlagen");
            Console.WriteLine("3) Stuur subscription mail");
            Console.WriteLine("4) Stuur subscription mail to multiple people");
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
                        // SendSubscriptionConfirmation<Person>("Jelle", "jelle.dispersyn@student.vives.be", _appSettingsService.AppSettings.html.htmlFolderPath, "Info.html", "", "", "", false);
                        SendSubscriptionConfirmation<Person>("Jelle", "jelle.dispersyn@student.vives.be", @"D:\Vives\Kwartaal 4\Programming Applictaion software\Taken\Taak_3_Jelle_Dispersyn\SMTP\MailCommunicationDemo\Files\Html\Info.html", "", "", "", "", false);
                        PrintMenu();
                        break;
                    }
                case "4":
                    {
                        //  SendSubscriptionConfirmation<Person>("", "", _appSettingsService.AppSettings.html.htmlFolderPath, "Info.html", "geadresseerden.json", _appSettingsService.AppSettings.json.jsonFolderPath, "json", true);
                        SendSubscriptionConfirmation<Person>("", "", @"D:\Vives\Kwartaal 4\Programming Applictaion software\Taken\Taak_3_Jelle_Dispersyn\SMTP\MailCommunicationDemo\Files\Html\Info.html", "", "geadresseerden.json", @"D:\Vives\Kwartaal 4\Programming Applictaion software\Taken\Taak_3_Jelle_Dispersyn\SMTP\MailCommunicationDemo\Files\Json", "json", true);
                        PrintMenu();
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

        static void SendSubscriptionConfirmation<T>(string personName, string personEmail, string htmlPath, string htmlFileName, string fileName, string jsonPath, string fileType, bool multiplePeople)
        {
            string html = File.ReadAllText(htmlPath);
            if (multiplePeople == true)
            {
                var persons = _deserialize.DeserializeObjectFromFile<List<Person>>(jsonPath, fileName, fileType);
                foreach (var person in persons.ReturnValue)
                {
                    string body = String.Format(html, person.Name, person.Email);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine($"Sending mail via Sendgrif SMTP Relay {person.Email}");

                    var msg = sMTPMailMessage.CreateMail(person.Email, body, "Subscription");

                    var result = sMTPMailMessage.sendMessage(msg);

                    if (result.Status == MailSendingStatus.OK)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"Message send to {person.Email}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine(result.Message);
                    }
                    
                }
                return;
            }
            else
            {
                string body = String.Format(html, personName, personEmail);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Sending mail via Sendgrif SMTP Relay {personEmail}");

                var msg = sMTPMailMessage.CreateMail(personEmail, body, "Subscription");

                var result = sMTPMailMessage.sendMessage(msg);

                if (result.Status == MailSendingStatus.OK)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine($"Message send to {personEmail}");
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine(result.Message);
                    return;
                }
            }
            

        }
    }
}
