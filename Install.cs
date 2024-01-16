using System.Diagnostics;
using System.Management;

namespace AuditHelper3_1
{
    internal class Install
    {
        public static void InstallPrograms(Data data)
        {
            string path = data.LocalPath;

            var (foundAnyDesk, foundNvision) = CheckInstalled();

            InstallOpenAudit(path);

            if (foundAnyDesk && foundNvision)
            {
                Menu.MenuUI("AnyDesk oraz nVision są już zainstalowane.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
                Menu.MainMenu();
            }
            else
            {
                if (!foundAnyDesk) {InstallAnyDesk(path);}
                if (!foundNvision) {InstallNvision(path);}

                Menu.MenuUI("Zakończono instalację programów.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
                Menu.MainMenu();
            }
        }

        private static void InstallAnyDesk(string path)
        {

            try
            {
                Menu.MenuUI("Trwa instalowanie AnyDesk.;;Prosze czekać...");
                string installerPath = Path.Combine(path, "\\Instalki\\AnyDesk_BetterIT_ACL.msi");

                Process process = new Process();
                process.StartInfo.FileName = "msiexec.exe";
                process.StartInfo.Arguments = $"/i \"{installerPath}\"";
                process.Start();
                process.WaitForExit();
                Menu.MenuUI("Zainstalowano AnyDesk.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
            catch
            {
                Menu.MenuUI("Napotkano problem podczas instalacji AnyDesk.;Spróbuj zainstalować ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
        }

        private static void InstallNvision(string path)
        {
            try
            {
                Menu.MenuUI("Trwa instalowanie nVision.;;Prosze czekać...");
                string installerPath = Path.Combine(path, "\\Instalki\\nVAgentInstall.msi");
                Process process = new Process();
                process.StartInfo.FileName = "msiexec.exe";
                process.StartInfo.Arguments = $"/i \"{installerPath}\"";
                process.Start();
                process.WaitForExit();

                Menu.MenuUI("Zainstalowano nVision.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
            catch
            {
                Menu.MenuUI("Napotkano problem podczas instalacji nVision.;Spróbuj zainstalować ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
        }

        private static void InstallOpenAudit(string path)
        {
            try
            {
                Menu.MenuUI("Trwa instalowanie OpenAudit.;;Prosze czekać...");
                
                string installerPath = Path.Combine(path, "\\Instalki\\INSTALL.bat");

                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/c \"{installerPath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                process.WaitForExit();

                Menu.MenuUI("Zainstalowano OpenAudit.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
            catch
            {
                Menu.MenuUI("Napotkano problem podczas instalacji OpenAudit.;Spróbuj zainstalować ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
        }

        private static Tuple<bool, bool> CheckInstalled()
        {
            Menu.MenuUI("Trwa weryfikacja zainstalowanych programów.;;Proszę czekać...");

            bool installedAnyDesk = false;
            bool installedNvision = false;

            ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_Product");

            foreach (ManagementObject mo in mos.Get())
            {
                if (mo["Name"] != null)
                {
                    if (mo["Name"].ToString().Contains("AnyDesk BetterIT"))
                    {
                        installedAnyDesk = true;
                    }

                    if (mo["Name"].ToString().Contains("nVision Agent"))
                    {
                        installedNvision = true;
                    }
                }
            }

            return Tuple.Create(installedAnyDesk, installedNvision);
        }
    }
}
