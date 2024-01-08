using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditHelper3_1
{
    internal class MenuUI
    {
        public static void Title()
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
        }

        public static void MainMenuUI()
        {
            Title();
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        1) Tworzenie konta BITAdmin                          ║");
            Console.WriteLine("║                        2) Instalacja oprogramowania                         ║");
            Console.WriteLine("║                        3) Zbieranie informacji                              ║");
            Console.WriteLine("║                        4) Wyjście                                           ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
        }

        public static void UserProgressUI()
        {
            Title();
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Trwa tworzenie konta.                                ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Proszę czekać...                                     ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
        }

        public static void UserCreatedUI()
        {
            Title();
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Pomyślnie utworzono                                  ║");
            Console.WriteLine("║                        konto administratora                                 ║");
            Console.WriteLine("║                        BITAdmin wraz z hasłem.                              ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Naciśnij dowolny przycisk                            ║");
            Console.WriteLine("║                        by wrócić do menu.                                   ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.ReadKey();
        }

        public static void UserErrorUI()
        {
            Title();
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Konto BITAdmin                                       ║");
            Console.WriteLine("║                        już istnieje w systemie.                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Naciśnij dowolny przycisk                            ║");
            Console.WriteLine("║                        by wrócić do menu.                                   ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.ReadKey();
            Menu.MainMenu();
        }

        public static void UserGroupErrorUI()
        {
            Title();
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Utworzono konto BITAdmin,                            ║");
            Console.WriteLine("║                        Lecz napotkano problem                               ║");
            Console.WriteLine("║                        podczas dodawania                                    ║");
            Console.WriteLine("║                        do grupy Administratorów.                            ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Naciśnij dowolny przycisk                            ║");
            Console.WriteLine("║                        by kontynuować.                                      ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.ReadKey();
        }

        public static void NotImplementedUI()
        {
            Title();
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Funkcja w przygotowaniu                              ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                        Naciśnij dowolny przycisk                            ║");
            Console.WriteLine("║                        by wrócić do menu.                                   ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("║                                                                             ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════╝");
            Console.ReadKey();
            Menu.MainMenu();
        }
    }
}
