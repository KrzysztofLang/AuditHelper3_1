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
                MenuUI($"Wybierz funkcję:;;" +
                    $"{(data.StepsTaken["programs"]||data.StepsTaken["info"]||data.StepsTaken["user"] ? "" : "a) Pełen audyt;;")}" +
                    $"1) Instalacja oprogramowania         {(data.StepsTaken["programs"] ? "(gotowe)" : "")};" + 
                    $"2) Zbieranie informacji              {(data.StepsTaken["info"] ? "(gotowe)" : "")};" + 
                    $"3) Tworzenie kont administracyjnych  {(data.StepsTaken["user"] ? "(gotowe)" : "")};;" +
                    $"4) Informacje o programie;;" + 
                    $"q) Wyjście;;;" +
                    $"Hostname urządzenia: {(data.Hostname)}");

                input = Console.ReadKey(true).Key;
                switch (input)
                {
                    case ConsoleKey.A:
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
                        MenuUI("Przed rozpoczęciem audytu upewnij się, że odpowiednie pliki instalacyjne znajdują się w folderze \"Instalki\", znajdującym sie w tej samej lokalizacji co program." +
                            "Te pliki to:;" +
                            "   - AnyDesk_BetterIT_ACL.msi;" +
                            "   - nVAgentInstall.msi;" +
                            "   - Odpowiednio przygotowane pliki do instalacji OpenAudit, w tym skrypt INSTALL.bat;;" +
                            "Zależnie od potrzeb, program może być użyty w jeden z 2 sposobów: pełen audyt lub ręcznie wybierając poszczególne funkcje. W przypadku pełnego audytu, funkcje wykonywane są w kolejności: " +
                            "instalacja oprogramowania, zbieranie informacji, tworzenie kont administracyjnych;;" +
                            "Opis funkcji:;" +
                            "   - Instalacja oprogramowania - Najpierw sprawdzane jest, czy na komputerze zainstalowane są AnyDesk i agent nVision. Następnie instalowane są te programy których na komputerze brakuje. " +
                            "Na koniec dodawany jest wpis do Harmonogramu Zadań uruchamiający komunikację z OpenAudit.;" +
                            "   - Zbieranie informacji - Automatycznie sprawdzane są: hostname, AnyDeskID. Następnie przeprowadzający audyt wprowadza: nazwę nadaną według standardu BetterIT, użytkownika odpowiedzialnego, " +
                            "opcjonalny komentarz. Dane są zapisywane do pliku o nazwie XYZ_dane.csv, gdzie XYZ to trzyliterowy skrót klienta.;" +
                            "   - Tworzenie kont administracyjnych - Po weryfikacji czy takie konto już nie istnieje w systemie, automatycznie tworzone jest konto administratora o nazwie BITAdmin, " +
                            "wraz z losowo generowanym bezpiecznym hasłem. Następnie przedstawiany jest wybór czy tworzyć konto administracyjne dla klienta. " +
                            "W przypadku zgody, tworzone jest konto o nazwie XYZAdmin, gdzie XYZ to trzyliterowy skrót klienta, wraz z nowo generowanym bezpiecznym hasłem. Dane utworzonych kont zapisywane sa w pliku " +
                            "o nazwie XYZ_pwd.csv, gdzie XYZ to trzyliterowy skrót klienta. Sam plik jest sformatowany w sposób pozwalający na import do bazy Bitwarden.;;" +
                            "Przogram przygotowany przez Krzysztof Lang, na potrzeby BetterIT/MH-info sp. z o.o.;;" +
                            "Naciśnij dowolny przycisk by wrócić do menu.");
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
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════╗");
            PrintColoredText(@"║                        _ _ _   _    _      _                  ____  __      ║");
            PrintColoredText(@"║         /\            | (_) | | |  | |    | |                |___ \/_ |     ║");
            PrintColoredText(@"║        /  \  _   _  __| |_| |_| |__| | ___| |_ __   ___ _ __   __) || |     ║");
            PrintColoredText(@"║       / /\ \| | | |/ _` | | __|  __  |/ _ \ | '_ \ / _ \ '__| |__ < | |     ║");
            PrintColoredText(@"║      / ____ \ |_| | (_| | | |_| |  | |  __/ | |_) |  __/ |    ___) || |     ║");
            PrintColoredText(@"║     /_/    \_\__,_|\__,_|_|\__|_|  |_|\___|_| .__/ \___|_|   |____(_)_|     ║");
            PrintColoredText(@"║                                             | |                             ║");
            PrintColoredText(@"║                                             |_|                             ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");

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

