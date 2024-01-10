using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditHelper3_1
{
    internal class Menu
    {
        public static string MainMenu()
        {
            Menu.MenuUI("Wybierz funkcję:;;1) Tworzenie konta BITAdmin;2) Instalacja oprogramowania;3) Zbieranie informacji;;4) Wyjście");

            do
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        // Tworzenie konta BITAdmin
                        User user = new();
                        break;
                    case "2":
                        // Instalacja oprogramowania
                        Install install = new();
                        break;
                    case "3":
                        // Zbieranie informacji
                        Menu.MenuUI("Funkcja w przygotowaniu.;;Naciśnij dowolny przycisk by kontynuować.");
                        break;
                    case "4":
                        // Wyjście
                        Environment.Exit(0);
                        break;
                    default:
                        Menu.MenuUI("Wybierz funkcję:;;1) Tworzenie konta BITAdmin;2) Instalacja oprogramowania;3) Zbieranie informacji;;4) Wyjście");
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

            for (int i = 0; i < 11 - textLines.Length; i++)
            {
                Console.WriteLine("║                                                                             ║");
            }

            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}
