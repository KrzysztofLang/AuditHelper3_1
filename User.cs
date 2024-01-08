
using System.DirectoryServices.AccountManagement;
using System.Security.Cryptography;



namespace AuditHelper3_1
{
    internal class User
    {
        // Nazwa użytkownika
        private const string userName = "BITAdmin_test";
        private const string fullUserName = "Administrator lokalny BetterIT";
        private const string groupNamePL = "Administratorzy";
        private const string groupNameEN = "Administrators";
        private bool userCreated;
        private string password = "";

        public User()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Machine))
            {
                // Check if a user with the specified username already exists
                UserPrincipal existingUser = UserPrincipal.FindByIdentity(pc, userName);
                if (existingUser != null)
                {
                    MenuUI.UserErrorUI();
                }

                // Create the new user
                UserPrincipal user = new UserPrincipal(pc)
                {
                    Name = userName,
                    PasswordNeverExpires = true,
                    UserCannotChangePassword = true,
                    Enabled = true,
                    DisplayName = fullUserName
                };
                password = GenerateRandomPassword;
                user.SetPassword(password);
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
                        MenuUI.UserCreatedUI();
                    }
                    else
                    {
                        GroupPrincipal groupEN = GroupPrincipal.FindByIdentity(pc, groupNameEN);
                        if (groupEN != null)
                        {
                            // If the group exists, add the new user to the group
                            groupEN.Members.Add(user);
                            groupEN.Save();
                            MenuUI.UserCreatedUI();
                        }
                        else
                        {
                            MenuUI.UserGroupErrorUI();
                        }
                    }
                }
                catch
                {
                    MenuUI.UserGroupErrorUI();
                }
            }
        }

        private static string GenerateRandomPassword
        {
            get
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

        public static string UserName => userName;
        public static string FullUserName => fullUserName;
        public string Password { get => password; }
        public bool UserCreated { get => userCreated; set => userCreated = value; }
    }
}
