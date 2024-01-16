using System.DirectoryServices.AccountManagement;



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
            bool userCreated;

            Menu.MenuUI("Trwa tworzenie konta.;;Proszę czekać...");

            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine))
            {
                // Check if a user with the specified username already exists
                UserPrincipal existingUser = UserPrincipal.FindByIdentity(pc, userName);
                if (existingUser != null)
                {
                    Menu.MenuUI("Konto BITAdmin już istnieje w systemie.;;Naciśnij dowolny przycisk by kontynuować.");
                    Console.ReadKey();
                    Menu.MainMenu();
                }
                else
                {
                    // Create the new user
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

                    userCreated = true;

                    try
                    {
                        // Check if the group exists
                        GroupPrincipal groupPL = GroupPrincipal.FindByIdentity(pc, groupNamePL);
                        if (groupPL != null)
                        {
                            // If the group exists, add the new user to the group
                            groupPL.Members.Add(user);
                            groupPL.Save();
                            Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;oraz nadano uprawnienia administratora.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                            Console.ReadKey();
                            Menu.MainMenu();
                        }
                        else
                        {
                            GroupPrincipal groupEN = GroupPrincipal.FindByIdentity(pc, groupNameEN);
                            if (groupEN != null)
                            {
                                // If the group exists, add the new user to the group
                                groupEN.Members.Add(user);
                                groupEN.Save();
                                Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;oraz nadano uprawnienia administratora.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                                Console.ReadKey();
                                Menu.MainMenu();
                            }
                            else
                            {
                                Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;lecz nie udało się nadać uprawnień administratora.;Spróbuj nadać je ręcznie.;;Naciśnij dowolny przycisk by kontynuować.");
                                Console.ReadKey();
                                Menu.MainMenu();
                            }
                        }
                    }
                    catch
                    {
                        Menu.MenuUI("Pomyślnie utworzono konto BITAdmin;lecz nie udało się nadać uprawnień administratora.;Spróbuj nadać je ręcznie.;;Hasło: " + data.Password + ";;Naciśnij dowolny przycisk by kontynuować.");
                        Console.ReadKey();
                        Menu.MainMenu();
                    }
                }
            }
        }

        private void UserToFile()
        {
            return;
        }
    }
}
