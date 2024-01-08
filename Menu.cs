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
            MenuUI.MainMenuUI();
            bool isCorrectInput = false;
            string choice = "";

            do
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        // Tworzenie konta BITAdmin
                        MenuUI.UserProgressUI();
                        User user = new();
                        break;
                    case "2":
                        // Instalacja oprogramowania
                        MenuUI.NotImplementedUI();
                        break;
                    case "3":
                        // Zbieranie informacji
                        MenuUI.NotImplementedUI();
                        break;
                    case "4":
                        // Wyjście
                        Environment.Exit(0);
                        break;
                    default:
                        MenuUI.MainMenuUI();
                        break;
                }
            }
            while (!isCorrectInput);

            return choice;
        }
    }
}
