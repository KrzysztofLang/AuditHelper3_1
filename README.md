# AuditHelper3.1

## Za³o¿enie

Program ma za zadanie u³atwiæ przeprowadzanie audytu lokalnego poprzez automatyzacjê elementów audytu:

- Instalacja oprogramowania
- Zebranie informacji o komputerze
- Tworzenie administraora lokalnego

## Sposób dzia³ania

Zale¿nie od potrzeb, program mo¿e byæ u¿yty na jeden z 2 sposobów: pe³en audyt lub rêcznie wybieraj¹c poszczególne funkcje.
W przypadku pe³nego audytu, funkcje wykonywane s¹ w kolejnoœci: instalacja oprogramowania, zbieranie informacji, tworzenie kont administracyjnych.
W przypadku rêcznego wybierania funkcji, po ich wykonaniu program wraca do menu wyboru funkcji i oznacza przeprowadzone dzia³ania.

Opis funkcji:
1. Instalacja oprogramowania - Najpierw sprawdzane jest, czy na komputerze zainstalowane s¹ AnyDesk i agent nVision. Nastêpnie instalowane s¹ te programy których na komputerze brakuje. Na koniec dodawany jest wpis do Harmonogramu Zadañ uruchamiaj¹cy komunikacjê z OpenAudit.
1. Zbieranie informacji - Automatycznie sprawdzane s¹:
	- hostname
	- AnyDeskID

	Nastêpnie przeprowadzaj¹cy audyt wprowadza:
	- Nazwê nadan¹ wed³ug standardu BetterIT
	- U¿ytkownika odpowiedzialnego
	- Opcjonalny komentarz

	Dane s¹ zapisywane do pliku o nazwie XYZ_dane.csv, gdzie XYZ to trzyliterowy skrót klienta.
1. Tworzenie kont administracyjnych - Po weryfikacji czy takie konto ju¿ nie istnieje w systemie, automatycznie tworzone jest konto administratora o nazwie BITAdmin, wraz z losowo generowanym bezpiecznym has³em. Nastêpnie przedstawiany jest wybór czy tworzyæ konto administracyjne dla klienta. W przypadku zgody, tworzone jest konto o nazwie XYZAdmin, gdzie XYZ to trzyliterowy skrót klienta, wraz z nowo generowanym bezpiecznym has³em. Dane utworzonych kont zapisywane sa w pliku o nazwie XYZ_pwd.csv, gdzie XYZ to trzyliterowy skrót klienta. Sam plik jest sformatowany w sposób pozwalaj¹cy na import do bazy Bitwarden.