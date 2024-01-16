using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace AuditHelper3_1
{
    internal class Menu
    {
        public static Data data = new Data();

        public Menu()
        {
            MainMenu();
        }

        public static void MainMenu()
        {
            MenuUI("Wybierz funkcję:;;1) Zbieranie informacji;2) Tworzenie konta BITAdmin;3) Instalacja oprogramowania;;4) Wyjście");
            Console.WriteLine(data.LocalPath);
            
            foreach (var item in data.StepsTaken)
            {
                Console.WriteLine("W menu:");
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }

            do
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        Data.GetInfo();
                        break;
                    case "2":
                        User.CreateUser(data);
                        break;
                    case "3":
                        Install.InstallPrograms(data);
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Menu.MenuUI("Wybierz funkcję:;;1) Zbieranie informacji;2) Tworzenie konta BITAdmin;3) Instalacja oprogramowania;;4) Wyjście");
                        break;
                }
            }
            while (true);
        }

        public static void MenuUI(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                         _ _ _   _    _      _                ____  __       ║");
            Console.WriteLine("║          /\\            | (_) | | |  | |    | |              |___ \\/_ |      ║");
            Console.WriteLine("║         /  \\  _   _  __| |_| |_| |__| | ___| |_ __   ___ _ __ __) || |      ║");
            Console.WriteLine("║        / /\\ \\| | | |/ _` | | __|  __  |/ _ \\ | '_ \\ / _ \\ '__|__ < | |      ║");
            Console.WriteLine("║       / ____ \\ |_| | (_| | | |_| |  | |  __/ | |_) |  __/ |  ___) || |      ║");
            Console.WriteLine("║      /_/    \\_\\__,_|\\__,_|_|\\__|_|  |_|\\___|_| .__/ \\___|_| |____(_)_|      ║");
            Console.WriteLine("║                                              | |                            ║");
            Console.WriteLine("║                                              |_|     by Krzysztof Lang, 2024║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");

            string[] textLines = text.Split(';');
            foreach (string line in textLines)
            {
                string spaces = new string(' ', 73 - line.Length);
                Console.WriteLine("║    " + line + spaces + '║');
            }

            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}
