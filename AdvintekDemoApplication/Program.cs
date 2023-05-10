using System;
using System.IO;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace AdvintekDemoApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string folderPath = "E:\\MessageFiles";
                FileSystemWatcher watcher = new FileSystemWatcher(folderPath);
                watcher.Created += OnFileCreated;
                watcher.EnableRaisingEvents = true;

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines(e.FullPath);

                // Extract the values of To and Body from the string array
                string to = lines[0].Substring("Message for:".Length);
                string body = lines[1].Substring("text:".Length);

                const string accountSid = "ACc6ee9ddda3e0774ea4f0429cff507adc";
                const string authToken = "851b31edd5157a681cf2762b9fefc5e9";
                TwilioClient.Init(accountSid, authToken);

                var message = MessageResource.Create(
                    body: body,
                    from: new Twilio.Types.PhoneNumber("+12706068054"),
                    to: new Twilio.Types.PhoneNumber(to)
                );
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
