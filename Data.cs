using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace AuditHelper3_1
{
    internal class Data
    {
        private static string localPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "";

        private string hostname = System.Environment.MachineName;
        private string anyDeskID = GetAnyDeskID();
        private string password = GenerateRandomPassword();

        private static string deviceName = "";
        private static string? userName;
        private static string? comment;

        private static Dictionary<string, bool> stepsTaken = new Dictionary<string, bool>();

        public string LocalPath { get => localPath; }
        public string Password { get => password; }
        public string? DeviceName { get => deviceName; set => deviceName = value; }
        public Dictionary<string, bool> StepsTaken { get => stepsTaken; set => stepsTaken = value; }


        [DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);

        public Data()
        {
            stepsTaken.Add("info", false);
            stepsTaken.Add("user", false);
            stepsTaken.Add("programs", false);
        }

        public static void GetInfo()
        {
            while(true)
            {
                Menu.MenuUI("Podaj nazwę BetterIT:");
                deviceName = Console.ReadLine();
                Menu.MenuUI("Podaj użytkownika odpowiedzialnego:");
                userName = Console.ReadLine();
                Menu.MenuUI("Podaj opcjonalny komentarz:");
                comment = Console.ReadLine();
                Menu.MenuUI("Podsumowanie:;;Nazwa: " + deviceName + ";Użytkownik: " + userName + ";Komentarz: " + comment + ";;Wpisz \"1\" by poprawić bądź naciśnij inny przycisk by kontynuować.");

                switch (Console.ReadLine())
                {
                    case "1":
                        break;
                    default:
                        stepsTaken["info"] = true;
                        Menu.MainMenu();
                        break;
                }
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
            const int passwordLength = 12;
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
    }
}
