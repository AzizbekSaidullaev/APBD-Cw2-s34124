# Jak uruchomić?
1. Pobrać repozytorium na dysk.
2. Otworzyć plik '.sln'.
3. Wcisnąć run.

# Architektura

* W gałęzi domain są klasy: User, Equipment i Rental
* W gałęzi services są klasy: 'RentalServices'


Klasy z "domain" to czyste obiekty. Nie zawierają one logiky wypożyczenia
Natomiast "services' zajmuje się koordynacją z 'program.cs'.