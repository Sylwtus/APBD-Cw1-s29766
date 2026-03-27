# APBD-Cw1-s29766

Prosty program konsolowy do wypożyczania sprzętu – laptopów, projektorów i kamer.  
Umożliwia dodawanie użytkowników, sprzętu, wypożyczenia, zwroty oraz podgląd raportów.

---

## Link do repozytorium
https://github.com/Sylwtus/APBD-Cw1-s29766

---

## Jak korzystać z programu

Po uruchomieniu pojawia się menu konsolowe, które pozwala na:
- dodawanie użytkowników i sprzętu  
- wypożyczanie i zwroty  
- podgląd sprzętu i wypożyczeń  
- wyświetlenie podsumowania  

---

## Struktura projektu

- Models – klasy danych (`User`, `Student`, `Employee`, `Equipment`, `Laptop`, `Camera`, `Projector`, `Rental`)  
- Enums – wyliczenia (`UserType`, `BorrowResults`)  
- Services – logika aplikacji (`UserService`, `EquipmentService`, `RentalService`, `FileService`)  
- Program – menu i obsługa aplikacji  

---

## Instrukcja uruchomienia

Sklonuj repozytorium:

```
git clone https://github.com/TWOJ_USER/APBD-Cw1-s29766.git
cd APBD-Cw1-s29766
```

Zbuduj projekt:

```
dotnet build
```

Uruchom aplikację:

```
dotnet run
```

Po uruchomieniu korzystasz z menu.  

---

## Decyzje projektowe

- Logika została podzielona na serwisy, żeby kod był czytelny  
- Zastosowane dziedziczenie (`User`, `Equipment`) ułatwia rozbudowę  
- Interfejs to proste menu konsolowe  

---
