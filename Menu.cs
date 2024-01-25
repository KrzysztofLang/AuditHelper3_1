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
                $"1) Instalacja oprogramowania         {(data.StepsTaken["programs"] ? "(gotowe)" : "")};" + 
                $"2) Zbieranie informacji              {(data.StepsTaken["info"] ? "(gotowe)" : "")};" + 
                $"3) Tworzenie kont administracyjnych  {(data.StepsTaken["user"] ? "(gotowe)" : "")};;" + 
                $"4) Wyjście;;" +
                $"Przed przeprowadzeniem audytu upewnij się, że przygotowano odpowiednie pakiety instalacyjne!");

            ConsoleKey input;
            do
            {
                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.Enter:
                        if (data.StepsTaken["programs"] || data.StepsTaken["info"] || data.StepsTaken["user"])
                        {
                            break;
                        }
                        else
                        {
                            Install.InstallPrograms(data, fullAudit: true);
                            Data.GetInfo(fullAudit: true);
                            User.NewUsers(data, fullAudit: true);
                            MenuUI("Audyt zakończony.;;Naciśnij dowolny klawisz by zamknąć.");
                            Console.ReadKey(true);
                            Environment.Exit(0);
                            break;
                        }

                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Install.InstallPrograms(data);
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Data.GetInfo();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        User.NewUsers(data);
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Environment.Exit(0);
                        break;

                    default: break;
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
