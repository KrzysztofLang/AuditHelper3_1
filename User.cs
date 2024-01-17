using CsvHelper;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;



namespace AuditHelper3_1
{
    internal class User
    {
        public static void CreateUser(Data data)
        {
            string userName = "BITAdmin_test";
            string fullUserName = "Administrator lokalny BetterIT";
            string groupNamePL = "Administratorzy";
            string groupNameEN = "Administrators";

            Menu.MenuUI("Trwa tworzenie konta.;;Proszę czekać...");

            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine))
            {
                UserPrincipal existingUser = UserPrincipal.FindByIdentity(pc, userName);
                if (existingUser != null)
                {
                    Menu.MenuUI("Konto BITAdmin już istnieje w systemie.;;Naciśnij dowolny przycisk by kontynuować.");
                    Console.ReadKey();
                }
                else
                {
                    UserPrincipal user = new UserPrincipal(pc)
                    {
                        Name = userName,
                        PasswordNeverExpires = true,
                        UserCannotChangePassword = true,
                        Enabled = true,
                        DisplayName = fullUserName
                    };
                    user.SetPassword(data.Password);
                    user.Save();

                    try
                    {
                        GroupPrincipal groupPL = GroupPrincipal.FindByIdentity(pc, groupNamePL);
                        if (groupPL != null)
                        {
                            groupPL.Members.Add(user);
                            groupPL.Save();
                            Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;oraz nadano uprawnienia administratora.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                        }
                        else
                        {
                            GroupPrincipal groupEN = GroupPrincipal.FindByIdentity(pc, groupNameEN);
                            if (groupEN != null)
                            {
                                groupEN.Members.Add(user);
                                groupEN.Save();
                                Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;oraz nadano uprawnienia administratora.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                            }
                            else
                            {
                                Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;lecz nie udało się nadać uprawnień administratora.;Spróbuj nadać je ręcznie.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                            }
                        }
                    }
                    catch
                    {
                        Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;lecz nie udało się nadać uprawnień administratora.;Spróbuj nadać je ręcznie.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                    }
                    Console.ReadKey();
                    UserToFile(data);
                }
            }
            data.StepsTaken["user"] = true;
            
            Menu.MainMenu();
        }

        private static void UserToFile(Data data)
        {
            while (!data.StepsTaken["info"])
            {
                Menu.MenuUI("Przed zapisaniem danych do pliku należy wypełnić informacje o komputerze.;;Naciśnij dowolny przycisk by kontynuować.");
                Console.ReadKey();
                Data.GetInfo(returnToMenu: false);
            }

            Menu.MenuUI("Trwa zapisywanie pliku.;;Proszę czekać...");

            string path = data.LocalPath;

            string filePath = Path.Combine(path, "pwd_XXX.csv");

            bool fileExists = File.Exists(filePath);

            using (var writer = new StreamWriter(filePath, append: true))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                if (!fileExists)
                {
                    Console.WriteLine("Tworzenie pliku");
                    string[] columns = { "collections", "type", "name", "notes", "fields", "reprompt", "login_uri", "login_username", "login_password", "login_totp" };
                    foreach (var item in columns) { csv.WriteField(item); }
                    csv.NextRecord();                    
                }
                Console.WriteLine("Dopisywanie do pliku");
                string[] values = { data.DeviceName.Substring(0, 3) ?? "", "login", data.DeviceName ?? "", "", "", "", "", "BITAdmin", data.Password ?? "" };
                foreach (var item in values) { csv.WriteField(item); }
                csv.NextRecord();
            }
            Console.ReadKey();
            Menu.MenuUI("Zapisano dane do pliku.;;Naciśnij dowolny przycisk by kontynuować.");
            Console.ReadKey();
        }
    }
}
