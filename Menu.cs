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
            MenuUI($"{(data.StepsTaken["programs"]||data.StepsTaken["info"]||data.StepsTaken["user"] ? "Wybierz kolejną funkcję:;;" : "Wciśnij enter aby przprowadzić pełny audyt, lub wybierz funkcję:;;")}" +
                $"1) Instalacja oprogramowania {(data.StepsTaken["programs"] ? "(gotowe)" : "")};" + 
                $"2) Zbieranie informacji      {(data.StepsTaken["info"] ? "(gotowe)" : "")};" + 
                $"3) Tworzenie konta BITAdmin  {(data.StepsTaken["user"] ? "(gotowe)" : "")};;" + 
                $"4) Wyjście");

            do
            {
                switch (Console.ReadLine())
                {
                    case "":
                        Install.InstallPrograms(data, fullAudit: true);
                        Data.GetInfo(fullAudit: true);
                        User.CreateUser(data, fullAudit: true);
                        MenuUI("Audyt zakończony.;;Naciśnij dowolny klawisz by zamknąć.");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    case "1":
                        Install.InstallPrograms(data);
                        break;
                    case "2":
                        Data.GetInfo();
                        break;
                    case "3":
                        User.CreateUser(data);
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        MainMenu();
                        break;
                }
            }
            while (true);
        }

        public static void MenuUI(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"╔═════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine(@"║                         _ _ _   _    _      _                ____  __       ║");
            Console.WriteLine(@"║          /\            | (_) | | |  | |    | |              |___ \/_ |      ║");
            Console.WriteLine(@"║         /  \  _   _  __| |_| |_| |__| | ___| |_ __   ___ _ __ __) || |      ║");
            Console.WriteLine(@"║        / /\ \| | | |/ _` | | __|  __  |/ _ \ | '_ \ / _ \ '__|__ < | |      ║");
            Console.WriteLine(@"║       / ____ \ |_| | (_| | | |_| |  | |  __/ | |_) |  __/ |  ___) || |      ║");
            Console.WriteLine(@"║      /_/    \_\__,_|\__,_|_|\__|_|  |_|\___|_| .__/ \___|_| |____(_)_|      ║");
            Console.WriteLine(@"║                                              | |                            ║");
            Console.WriteLine(@"║                                              |_|     by Krzysztof Lang, 2024║");
            Console.WriteLine(@"╠═════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine(@"║                                                                             ║");
            Console.WriteLine(@"║                                                                             ║");

            string[] textLines = text.Split(';');
            foreach (string line in textLines)
            {
                //string spaces = new string(' ', 73 - line.Length);
                //Console.WriteLine("║    " + line + spaces + '║');

                if (line.Length <= 69)
                {
                    string spaces = new string(' ', 73 - line.Length);
                    Console.WriteLine("║    " + line + spaces + '║');
                }
                else
                {
                    int startIndex = 0;
                    while (startIndex < line.Length)
                    {
                        int length = Math.Min(69, line.Length - startIndex);
                        if (length < line.Length - startIndex && line[startIndex + length] != ' ')
                        {
                            int lastSpace = line.LastIndexOf(' ', startIndex + length, length);
                            if (lastSpace > startIndex)
                            {
                                length = lastSpace - startIndex;
                            }
                        }
                        string subLine = line.Substring(startIndex, length);
                        string spaces = new string(' ', 73 - subLine.Length);
                        Console.WriteLine("║    " + subLine + spaces + '║');
                        startIndex += length + 1;
                    }
                }
            }

            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}
