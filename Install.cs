using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuditHelper3_1
{
    internal class Install
    {
        private bool installedAnyDesk = false;
        private bool installedNvision = false;

        public Install()
        {
            CheckInstalled();
            InstallOpenAudit();

            if (installedAnyDesk && installedNvision)
            {
                Menu.MenuUI("AnyDesk oraz nVision są już zainstalowane.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
                Menu.MainMenu();
            }
            else
            {
                //if (!installedAnyDesk) {InstallAnyDesk();}
                //if (!installedNvision) {InstallNvision();}

                Menu.MenuUI("Zakończono instalację programów.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
                Menu.MainMenu();
            }
        }

        private void InstallAnyDesk()
        {

            try
            {
                Menu.MenuUI("Trwa instalowanie AnyDesk.;;Prosze czekać...");
                string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        private void InstallNvision()
        {
            try
            {
                Menu.MenuUI("Trwa instalowanie nVision.;;Prosze czekać...");
                string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        private void InstallOpenAudit()
        {
            try
            {
                Menu.MenuUI("Trwa instalowanie OpenAudit.;;Prosze czekać...");
                string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        private void CheckInstalled()
        {
            Menu.MenuUI("Trwa weryfikacja zainstalowanych programów.;;Proszę czekać...");

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
        }
    }
}
