﻿using System.Diagnostics;
using System.Management;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuditHelper3_1
{
    internal class Install
    {
        public static void InstallPrograms(Data data, bool fullAudit = false)
        {
            string path = data.LocalPath;

            var (foundAnyDesk, foundNvision) = CheckInstalled();

            InstallOpenAudit(path);

            if (foundAnyDesk && foundNvision)
            {
                Menu.MenuUI("AnyDesk oraz nVision są już zainstalowane.;;Naciśnij dowolny przycisk by kontynuować.");
            }
            else
            {
                if (!foundAnyDesk) {InstallProgram(path, "AnyDesk", "AnyDesk_BetterIT_ACL.msi");}
                if (!foundNvision) {InstallProgram(path, "nVision", "nVAgentInstall.msi");}

                Menu.MenuUI("Zakończono instalację programów.;;Naciśnij dowolny przycisk by kontynuować.");
            }

            data.StepsTaken["programs"] = true;
            Console.ReadKey();
            if (fullAudit) return; else Menu.MainMenu();
        }

        private static void InstallProgram(string path, string programName, string installerName)
        {
            try
            {
                Menu.MenuUI($"Trwa instalowanie {(programName)}.;;Proszę czekać...");
                string filePath = Path.Combine(path, $"\\Instalki\\{(installerName)}");

                Process process = new Process();
                process.StartInfo.FileName = "msiexec.exe";
                process.StartInfo.Arguments = $"/i \"{filePath}\"";
                process.Start();
                process.WaitForExit();
                Menu.MenuUI($"Zainstalowano {(programName)}.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
            catch
            {
                Menu.MenuUI($"Napotkano problem podczas instalacji {(programName)}. Spróbuj zainstalować ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
            }
        }

        private static void InstallOpenAudit(string path)
        {
            try
            {
                Menu.MenuUI("Trwa instalowanie OpenAudit.;;Prosze czekać...");
                
                string filePath = Path.Combine(path, "\\Instalki\\INSTALL.bat");

                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = $"/c \"{filePath}\"";
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
                Menu.MenuUI("Napotkano problem podczas instalacji OpenAudit. Spróbuj zainstalować ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
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
                string? name = mo["Name"]?.ToString();

                if (name == null) continue;

                installedAnyDesk |= name.Contains("AnyDesk BetterIT");
                installedNvision |= name.Contains("nVision Agent");

                if (installedAnyDesk && installedNvision) break;
            }

            return Tuple.Create(installedAnyDesk, installedNvision);
        }
    }
}
