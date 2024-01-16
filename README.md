# AuditHelper3.1

## Za�o�enie

Program ma za zadanie u�atwi� przeprowadzanie audytu lokalnego poprzez automatyzacj� element�w audytu:

- Tworzenie administraora lokalnego
- Instalacja oprogramowania
- Zebranie informacji o komputerze

## Spos�b dzia�ania

1. Tworzenie lokalnego konta administratora z bezpiecznym has�em
1. Wykrycie instalacji AnyDesk i agenta nVision
1. Je�li nie zosta�y wykryte, instalacja program�w
1. Dodanie do harmonogramu systemu zadania audytuj�cego do OpenAudit
1. Automatyczne zebranie informacji:
    1. Hostname
    1. ~~Mapowane udzia�y sieciowe~~
    1. AnyDesk ID
1. Wprowadzenie informacji przez personel:
    1. Nadana nazwa urz�dzenia
    1. U�ytkownik odpowiedzialny
    1. Dodatkowe uwagi
1. Utworzenie plik�w z danymi:
    1. dane_XXX.csv - zebrane informacje o urz�dzeniu
    2. pwd_XXX.csv - plik z danymi administratora gotowy do importu do mened�era hase�

