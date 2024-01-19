using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuditHelper3_1
{
    internal class Data
    {
        private static string localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";

        private static string hostname = System.Environment.MachineName;
        private string password = GenerateRandomPassword();

        private static string anyDeskID = "";
        private static string deviceName = "";
        private static string userName = "";
        private static string comment = "";

        private static Dictionary<string, bool> stepsTaken = new Dictionary<string, bool>();

        public string LocalPath { get => localPath; }
        public string Password { get => password; }
        public string? DeviceName { get => deviceName; }
        public Dictionary<string, bool> StepsTaken { get => stepsTaken; }


        [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);

        public Data()
        {
            stepsTaken.Add("info", false);
            stepsTaken.Add("user", false);
            stepsTaken.Add("programs", false);
        }

        public static void GetInfo(bool returnToMenu = true, bool fullAudit = false)
        {
                anyDeskID = GetAnyDeskID();

                Menu.MenuUI("Podaj nazwę BetterIT:");
                deviceName = Console.ReadLine();

                Menu.MenuUI("Podaj użytkownika odpowiedzialnego:");
                userName = Console.ReadLine();

                Menu.MenuUI("Podaj opcjonalny komentarz:");
                comment = Console.ReadLine();

                Menu.MenuUI("Podsumowanie:;;Nazwa: " + deviceName + ";Użytkownik: " + userName + ";Komentarz: " + comment +
                    ";;Wpisz \"1\" by poprawić bądź naciśnij inny przycisk by kontynuować.");

                switch (Console.ReadLine())
                {
                    case "1":
                        GetInfo();
                        break;
                    default:
                        stepsTaken["info"] = true;
                        string[] columns = { "Hostname", "Nazwa BetterIT", "User", "Uwagi", "AnyDesk ID" };
                        string[] values = { hostname, deviceName ?? "", userName ?? "", comment ?? "", anyDeskID };

                        SaveFile("dane", columns, values);
                        if (returnToMenu)
                        {
                            if (fullAudit) return; else Menu.MainMenu();
                        }                       
                        break;
                }
        }

        private static string GetAnyDeskID()
        {
            string filePath = Path.Combine(localPath, "\\AnyDeskID.bat");

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("@echo off");
                sw.WriteLine("path=C:\\Program Files (x86)\\AnyDesk-c035baa3;%path%");
                sw.WriteLine("for /f \"delims=\" %%i in ('AnyDesk-c035baa3 --get-id') do set CID=%%i");
                sw.WriteLine("echo %CID%");
            }

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/c \"{filePath}\"";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string anyDeskID = process.StandardOutput.ReadToEnd();
            process.WaitForExit();

            File.Delete(filePath);

            return anyDeskID;
        }

        private static string GenerateRandomPassword()
        {
            const int passwordLength = 9;
            const string validChars =
                "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz123456789!@#$%&*?-";

            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] uintBuffer = new byte[sizeof(uint)];

            char[] chars = new char[passwordLength];
            int validCharsCount = validChars.Length;
            for (int i = 0; i < passwordLength; i++)
            {
                rng.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                chars[i] = validChars[(int)(num % (uint)validCharsCount)];
            }

            return new string(chars);
        }

        public static void SaveFile(string fileName, string[] columns, string[] values)
        {
            while (!stepsTaken["info"])
            {
                Menu.MenuUI("Przed zapisaniem danych do pliku należy wypełnić informacje o komputerze.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
                GetInfo(returnToMenu: false);
            }

            Menu.MenuUI("Trwa zapisywanie do pliku.;;Proszę czekać...");

            string filePath = Path.Combine(localPath, $"{(deviceName.Substring(0, 3))}_{(fileName)}.csv");

            bool fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (!fileExists)
                {
                    foreach (var item in columns) { csv.WriteField(item); }
                    csv.NextRecord();
                }
                foreach (var item in values) { csv.WriteField(item); }
                csv.NextRecord();
            }
            Menu.MenuUI("Zapisano dane do pliku.;;Naciśnij dowolny przycisk by kontynuować.");
            Console.ReadKey();
        }
    }
}
