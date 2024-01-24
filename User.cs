using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.DirectoryServices.AccountManagement;
using System.Security.Cryptography;



namespace AuditHelper3_1
{
    internal class User
    {
        public static void NewUsers(Data data, bool fullAudit = false)
        {
            CreateUser(data, false);

            ConsoleKey input;
            bool cont = false;
            do
            {
                input = Console.ReadKey(true).Key;
                Menu.MenuUI("Czy chcesz utworzyć konto administracyjne dla klienta?;;" +
                                    "1) Tak;2) Nie;;");
                switch (input)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        CreateUser(data, true);
                        cont = true;
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        cont = true;
                        break;
                    default: break;
                }
            }
            while (!cont);

            data.StepsTaken["user"] = true;
            if (fullAudit) return; else Menu.MainMenu();
        }
        public static void CreateUser(Data data, bool clientAdmin)
        {
            if (data.DeviceName == "")
            {
                Menu.MenuUI("Nie zebrano danych o komputerze, proszę wprowadzić nazwę wg standardu BetterIT:");
                data.DeviceName = Console.ReadLine().ToUpper() ?? "";
            }

            string userName = clientAdmin ? $"{(data.DeviceName.Substring(0, 3))}Admin_test" : "BITAdmin_test";
            string fullUserName = $"Administrator lokalny {(clientAdmin ? data.DeviceName.Substring(0, 3) : "BetterIT")}";
            string groupNamePL = "Administratorzy"; 
            string groupNameEN = "Administrators";

            Menu.MenuUI($"Trwa tworzenie konta {(userName)}.;;Proszę czekać...");

            string password = GenerateRandomPassword();

            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine))
            {
                UserPrincipal existingUser = UserPrincipal.FindByIdentity(pc, userName);
                if (existingUser != null)
                {
                    Menu.MenuUI($"Konto {(userName)} już istnieje w systemie.;;Naciśnij dowolny przycisk by kontynuować.");
                    Console.ReadKey(true);
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
                    user.SetPassword(password);
                    user.Save();

                    try
                    {
                        GroupPrincipal groupPL = GroupPrincipal.FindByIdentity(pc, groupNamePL);
                        if (groupPL != null)
                        {
                            groupPL.Members.Add(user);
                            groupPL.Save();
                            Menu.MenuUI($"Pomyślnie utworzono konto {(userName)} oraz nadano uprawnienia administratora.;;" +
                                $"Hasło: {(password)};;Naciśnij dowolny przycisk by kontynuować.");
                        }
                        else
                        {
                            GroupPrincipal groupEN = GroupPrincipal.FindByIdentity(pc, groupNameEN);
                            if (groupEN != null)
                            {
                                groupEN.Members.Add(user);
                                groupEN.Save();
                                Menu.MenuUI($"Pomyślnie utworzono konto {(userName)} oraz nadano uprawnienia administratora.;;" +
                                    $"Hasło: {(password)};;Naciśnij dowolny przycisk by kontynuować.");
                            }
                            else
                            {
                                Menu.MenuUI($"Pomyślnie utworzono konto {(userName)} lecz nie udało się nadać uprawnień administratora. Spróbuj nadać je ręcznie.;;" +
                                    $"Hasło: {(password)};;Naciśnij dowolny przycisk by kontynuować.");
                            }
                        }
                    }
                    catch
                    {
                        Menu.MenuUI($"Pomyślnie utworzono konto {(userName)} lecz nie udało się nadać uprawnień administratora. Spróbuj nadać je ręcznie.;;" +
                            $"Hasło: {(password)};;Naciśnij dowolny przycisk by kontynuować.");
                    }
                    Console.ReadKey(true);

                    string[] columns = { "collections", "type", "name", "notes", "fields", "reprompt", "login_uri", "login_username", "login_password", "login_totp" };
                    string[] values = { data.DeviceName.Substring(0, 3), "login", data.DeviceName ?? "", "", "", "", "", userName, password ?? "" };
                    Data.SaveFile("pwd", columns, values);
                }
            }
        }

        private static string GenerateRandomPassword()
        {
            const int passwordLength = 10;
            const string validChars =
                "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz123456789!@#$%&*?-_+=";

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
