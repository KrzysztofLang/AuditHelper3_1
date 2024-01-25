using CsvHelper;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace AuditHelper3_1
{
    internal class Data
    {
        private static string hostname = System.Environment.MachineName;

        private static string anyDeskID = "";
        private static string deviceName = "";
        private static string userName = "";
        private static string comment = "";

        private static Dictionary<string, bool> stepsTaken = new Dictionary<string, bool>();
        public Dictionary<string, bool> StepsTaken { get => stepsTaken; }
        public string DeviceName { get => deviceName; set => deviceName = value; }

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
            anyDeskID = anyDeskID.TrimEnd('\r', '\n');

            string deviceNamePattern = @"^[A-Z]{3}-[A-Z]{3}[0-9]{2}$";
            Regex rgx = new Regex(deviceNamePattern);


            Menu.MenuUI("Podaj nazwę BetterIT w formacie \"XYZ-ABC00\":");
            while (true)
            {
                deviceName = Console.ReadLine() ?? "";
                deviceName = deviceName.ToUpper() ?? "";

                if (rgx.IsMatch(deviceName))
                {
                    break;
                }
                else
                {
                    Menu.MenuUI("Wpisano niepoprawną nazwę. Podaj nazwę BetterIT w formacie \"XYZ-ABC00\":");
                }
            }

            Menu.MenuUI("Podaj użytkownika odpowiedzialnego (imię i nazwisko), lub wpisz \"brak\" by pominąć:");
            while (true)
            {
                userName = Console.ReadLine() ?? "";

                if (userName == "brak")
                {
                    break;
                }
                else if (userName != null && userName.Split(' ').Length == 2)
                {
                    userName = string.Join(" ", userName.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
                    break;
                }
                else
                {
                    Menu.MenuUI("Niepoprawne dane. Podaj użytkownika odpowiedzialnego (imię i nazwisko), lub wpisz \"brak\" by pominąć:");
                }
            }

            Menu.MenuUI("Podaj opcjonalny komentarz:");
            comment = Console.ReadLine() ?? "";

            Menu.MenuUI($"Podsumowanie:;;Nazwa: {(deviceName)};Użytkownik: {(userName)};Komentarz: {(comment)};;" +
                $"Hostname: {(hostname)};AnyDesk ID: {(anyDeskID)};;" +
                "1) Kontynuuj;2) Popraw");

            ConsoleKey input;
            do
            {
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        stepsTaken["info"] = true;

                        string[] columns = { "Hostname", "Nazwa BetterIT", "User", "Uwagi", "AnyDesk ID" };
                        string[] values = { hostname, deviceName ?? "", userName ?? "", comment ?? "", anyDeskID };

                        SaveFile("dane", columns, values);

                        if (returnToMenu)
                        {
                            if (fullAudit) return; else Menu.MainMenu();
                        }
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        GetInfo();
                        break;
                    default:
                        break;
                }
            }
            while (true);

        }

        private static string GetAnyDeskID()
        {
            string filePath = Path.GetFullPath($"AnyDeskID.bat");

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

            if (!int.TryParse(anyDeskID, out int number))
            {
                Menu.MenuUI("Nie wykryto zainstalowanego AnyDesk.;;" +
                    "1) Instaluj AnyDesk i spróbuj ponownie;2) Spróbuj ponownie bez instalacji;3) Kontynuuj;4) Zamknij program");

                ConsoleKey input;
                do
                {
                    input = Console.ReadKey(true).Key;
                    switch (input)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Install.InstallProgram("AnyDesk", "AnyDesk_BetterIT_ACL.msi");
                            GetAnyDeskID();
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            GetAnyDeskID();
                            break;
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            Environment.Exit(0);
                            break;
                        default:
                            break;
                    }
                }
                while (input != ConsoleKey.D3 || input != ConsoleKey.NumPad3);
            }

            return anyDeskID;
        }

        public static void SaveFile(string fileName, string[] columns, string[] values)
        {
            Menu.MenuUI("Trwa zapisywanie do pliku.;;Proszę czekać...");

            string filePath = Path.GetFullPath($@"{(deviceName.Substring(0, 3))}_{(fileName)}.csv");

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
            Console.ReadKey(true);
        }
    }
}
