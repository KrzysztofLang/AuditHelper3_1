# AuditHelper3.1

## Za³o¿enie

Program ma za zadanie u³atwiæ przeprowadzanie audytu lokalnego poprzez automatyzacjê elementów audytu:

- Tworzenie administraora lokalnego
- Instalacja oprogramowania
- Zebranie informacji o komputerze

## Sposób dzia³ania

1. Tworzenie lokalnego konta administratora z bezpiecznym has³em
1. Wykrycie instalacji AnyDesk i agenta nVision
1. Jeœli nie zosta³y wykryte, instalacja programów
1. Dodanie do harmonogramu systemu zadania audytuj¹cego do OpenAudit
1. Automatyczne zebranie informacji:
    1. Hostname
    1. ~~Mapowane udzia³y sieciowe~~
    1. AnyDesk ID
1. Wprowadzenie informacji przez personel:
    1. Nadana nazwa urz¹dzenia
    1. U¿ytkownik odpowiedzialny
    1. Dodatkowe uwagi
1. Utworzenie plików z danymi:
    1. dane_XXX.csv - zebrane informacje o urz¹dzeniu
    2. pwd_XXX.csv - plik z danymi administratora gotowy do importu do mened¿era hase³

