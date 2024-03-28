using System.Diagnostics;
using System.Management;

namespace AuditHelper3_1
{
    internal class Install
    {
        public static void InstallPrograms(Data data, bool fullAudit = false)
        {
            Dictionary<string, string> programs = new Dictionary<string, string>();
            programs.Add("AnyDesk", "AnyDesk BetterIT");
            programs.Add("nVision", "nVision Agent");
            programs.Add("ESET Agent", "ESET Management Agent");

            Dictionary<string, bool> installed = CheckInstalled(programs);

            Installer("OpenAudIT", "INSTALL.bat");

            if (installed.Values.All(value => value == true))
            {
                Menu.MenuUI("AnyDesk oraz agenty nVision i Eset są już zainstalowane.");
                Thread.Sleep(3000);
            }
            else
            {
                if (!installed["AnyDesk"]) {Installer("AnyDesk", "AnyDesk_BetterIT_ACL.msi");}
                if (!installed["nVision"]) {Installer("nVision", "nVAgentInstall.msi");}
                if (!installed["ESET Agent"]) {Installer("ESET Agent", "PROTECTAgentInstaller.bat");}

                Menu.MenuUI("Zakończono instalację programów.");
                Thread.Sleep(3000);
            }

            data.StepsTaken["programs"] = true;
            if (fullAudit) return; else Menu.MainMenu();
        }

        public static void Installer(string programName, string installerName)
        {
            try
            {
                Menu.MenuUI($"Trwa instalowanie {(programName)}.;;Proszę czekać...");
                string filePath = Path.GetFullPath($@"Instalki\{(installerName)}");

                if (installerName.Substring(installerName.Length - 3) == "bat")
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = $"/c \"{filePath}\"";
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                    process.WaitForExit();
                }
                else if (installerName.Substring(installerName.Length - 3) == "msi")
                {
                    Process process = new Process();
                    process.StartInfo.FileName = "msiexec.exe";
                    process.StartInfo.Arguments = $"/i \"{(filePath)}\" /passive";
                    process.Start();
                    process.WaitForExit();
                }

                Menu.MenuUI($"Zainstalowano {(programName)}.");
                Thread.Sleep(3000);
            }
            catch
            {
                Menu.MenuUI($"Napotkano problem podczas instalacji {(programName)}. Spróbuj zainstalować ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
        }

        private static Dictionary<string, bool> CheckInstalled(Dictionary<string, string> programs)
        {
            Menu.MenuUI("Trwa weryfikacja zainstalowanych programów.;;Proszę czekać...");

            Dictionary<string, bool> installed = new Dictionary<string, bool>();

            foreach (var key in programs.Keys)
            {
                installed[key] = false;
            }

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Product");

            foreach (ManagementObject mo in mos.Get())
            {
                string? name = mo["Name"]?.ToString();

                if (name == null) continue;

                foreach(var key in installed.Keys)
                {
                    installed[key] |= name.Contains(programs[key]);
                }

                if (installed.Values.All(value => value == true)) break;
            }

            return installed;
        }
    }
}
