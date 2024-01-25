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


            ConsoleKey input;
            do
            {            
                MenuUI($"{(data.StepsTaken["programs"]||data.StepsTaken["info"]||data.StepsTaken["user"] ? "Wybierz kolejną funkcję:;;" : "Wciśnij enter aby przprowadzić pełny audyt, lub wybierz funkcję:;;")}" +
                $"1) Instalacja oprogramowania         {(data.StepsTaken["programs"] ? "(gotowe)" : "")};" + 
                $"2) Zbieranie informacji              {(data.StepsTaken["info"] ? "(gotowe)" : "")};" + 
                $"3) Tworzenie kont administracyjnych  {(data.StepsTaken["user"] ? "(gotowe)" : "")};;" +
                $"4) Informacje o programie;;" + 
                $"q) Wyjście;;");

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
                        MenuUI("Nie interesuj sie bo kotem w morde dostaniesz >:(");
                        Console.ReadKey(true);
                        break;

                    case ConsoleKey.Q:
                        Environment.Exit(0);
                        break;
                    
                    case ConsoleKey.C:
                    case ConsoleKey.K:
                        MenuUI(@"|\---/|    /\_/\;| o_o |   ( o.o ); \_^_/     > ^ <");
                        Console.ReadKey(true);
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
            PrintColoredText(@"║                        _ _ _   _    _      _                  ____  __      ║");
            PrintColoredText(@"║         /\            | (_) | | |  | |    | |                |___ \/_ |     ║");
            PrintColoredText(@"║        /  \  _   _  __| |_| |_| |__| | ___| |_ __   ___ _ __   __) || |     ║");
            PrintColoredText(@"║       / /\ \| | | |/ _` | | __|  __  |/ _ \ | '_ \ / _ \ '__| |__ < | |     ║");
            PrintColoredText(@"║      / ____ \ |_| | (_| | | |_| |  | |  __/ | |_) |  __/ |    ___) || |     ║");
            PrintColoredText(@"║     /_/    \_\__,_|\__,_|_|\__|_|  |_|\___|_| .__/ \___|_|   |____(_)_|     ║");
            PrintColoredText(@"║                                             | |                             ║");
            PrintColoredText(@"║                                             |_|                             ║");
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

        private static void PrintColoredText(string text)
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[0]);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text.Substring(1, 31));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(text.Substring(32, 31));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(text.Substring(63, 15));

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text[78]);
            Console.Write('\n');
        }
    }
}

