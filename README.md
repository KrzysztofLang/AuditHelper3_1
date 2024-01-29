# AuditHelper3.1

## Za�o�enie

Program ma za zadanie u�atwi� przeprowadzanie audytu lokalnego poprzez automatyzacj� element�w audytu:

- Instalacja oprogramowania
- Zebranie informacji o komputerze
- Tworzenie administraora lokalnego

## Spos�b dzia�ania

Zale�nie od potrzeb, program mo�e by� u�yty na jeden z 2 sposob�w: pe�en audyt lub r�cznie wybieraj�c poszczeg�lne funkcje.
W przypadku pe�nego audytu, funkcje wykonywane s� w kolejno�ci: instalacja oprogramowania, zbieranie informacji, tworzenie kont administracyjnych.
W przypadku r�cznego wybierania funkcji, po ich wykonaniu program wraca do menu wyboru funkcji i oznacza przeprowadzone dzia�ania.

Opis funkcji:
1. Instalacja oprogramowania - Najpierw sprawdzane jest, czy na komputerze zainstalowane s� AnyDesk i agent nVision. Nast�pnie instalowane s� te programy kt�rych na komputerze brakuje. Na koniec dodawany jest wpis do Harmonogramu Zada� uruchamiaj�cy komunikacj� z OpenAudit.
1. Zbieranie informacji - Automatycznie sprawdzane s�:
	- hostname
	- AnyDeskID

	Nast�pnie przeprowadzaj�cy audyt wprowadza:
	- Nazw� nadan� wed�ug standardu BetterIT
	- U�ytkownika odpowiedzialnego
	- Opcjonalny komentarz

	Dane s� zapisywane do pliku o nazwie XYZ_dane.csv, gdzie XYZ to trzyliterowy skr�t klienta.
1. Tworzenie kont administracyjnych - Po weryfikacji czy takie konto ju� nie istnieje w systemie, automatycznie tworzone jest konto administratora o nazwie BITAdmin, wraz z losowo generowanym bezpiecznym has�em. Nast�pnie przedstawiany jest wyb�r czy tworzy� konto administracyjne dla klienta. W przypadku zgody, tworzone jest konto o nazwie XYZAdmin, gdzie XYZ to trzyliterowy skr�t klienta, wraz z nowo generowanym bezpiecznym has�em. Dane utworzonych kont zapisywane sa w pliku o nazwie XYZ_pwd.csv, gdzie XYZ to trzyliterowy skr�t klienta. Sam plik jest sformatowany w spos�b pozwalaj�cy na import do bazy Bitwarden.