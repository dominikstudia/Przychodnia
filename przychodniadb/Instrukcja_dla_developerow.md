# PrzychodniaDB - instrukcja dla developerow

## Cel poprawki

Poprawka porzadkuje logike rol oraz specjalizacji lekarzy bez usuwania dotychczasowych tabel i kolumn uzywanych przez aplikacje. Najwazniejsza zasada po zmianach:

- `UserRoles` sluzy do uprawnien/logowania w systemie.
- `Doctors` i `Patients` sa profilami domenowymi uzytkownika.
- `DoctorSpecializations` przechowuje specjalizacje lekarza.

## Zmiany w bazie

Dodano tabele:

```sql
DoctorSpecializations
- DoctorID
- SpecializationID
- IsPrimary
```

Tabela pozwala przypisac lekarzowi wiecej niz jedna specjalizacje. Jedna specjalizacja moze byc oznaczona jako glowna przez `IsPrimary = 1`.

Zachowano kolumne:

```sql
Doctors.SpecializationID
```

Ta kolumna zostala zostawiona dla kompatybilnosci ze starym kodem aplikacji. Nowy kod powinien preferowac `DoctorSpecializations`, ale stare zapytania korzystajace z `Doctors.SpecializationID` nadal beda dzialac.

## Synchronizacja rol

Dodano triggery:

- `TR_Doctors_AssignLekarzRole`
- `TR_Patients_AssignPacjentRole`

Po dodaniu rekordu do `Doctors` baza automatycznie doda uzytkownikowi role `Lekarz`, jezeli jeszcze jej nie ma.

Po dodaniu rekordu do `Patients` baza automatycznie doda uzytkownikowi role `Pacjent`, jezeli jeszcze jej nie ma.

Dzieki temu aplikacja nie musi osobno pamietac o dopisaniu roli po utworzeniu profilu lekarza lub pacjenta.

## Synchronizacja specjalizacji

Dodano triggery:

- `TR_Doctors_SyncPrimarySpecialization`
- `TR_DoctorSpecializations_UpdateDoctorPrimary`

Jesli stary kod zmieni `Doctors.SpecializationID`, tabela `DoctorSpecializations` zostanie zaktualizowana automatycznie.

Jesli nowy kod ustawi specjalizacje w `DoctorSpecializations` z `IsPrimary = 1`, kolumna `Doctors.SpecializationID` zostanie zaktualizowana automatycznie.

## Jak dodac lekarza

Minimalnie, w starym stylu:

```sql
INSERT INTO Doctors (UserID, SpecializationID)
VALUES (@UserID, @SpecializationID);
```

Baza automatycznie:

- doda role `Lekarz` w `UserRoles`,
- doda glowna specjalizacje do `DoctorSpecializations`.

## Jak dodac pacjenta

```sql
INSERT INTO Patients (UserID)
VALUES (@UserID);
```

Baza automatycznie doda role `Pacjent` w `UserRoles`.

## Jak dodac kolejna specjalizacje lekarzowi

```sql
INSERT INTO DoctorSpecializations (DoctorID, SpecializationID, IsPrimary)
VALUES (@DoctorID, @SpecializationID, 0);
```

## Jak zmienic glowna specjalizacje lekarza

Nowy zalecany sposob:

```sql
UPDATE DoctorSpecializations
SET IsPrimary = CASE WHEN SpecializationID = @SpecializationID THEN 1 ELSE 0 END
WHERE DoctorID = @DoctorID;
```

Stary sposob nadal dziala:

```sql
UPDATE Doctors
SET SpecializationID = @SpecializationID
WHERE DoctorID = @DoctorID;
```

## Co developerzy powinni wiedziec

- Nie trzeba usuwac starego kodu korzystajacego z `Doctors.SpecializationID`.
- Nowy kod powinien uzywac `DoctorSpecializations`, gdy potrzebna jest lista wszystkich specjalizacji lekarza.
- `UserRoles` nie powinno byc traktowane jako jedyne zrodlo informacji, czy ktos jest lekarzem lub pacjentem. Dla logiki medycznej nalezy sprawdzac tabele `Doctors` i `Patients`.
- Role `Lekarz` i `Pacjent` sa synchronizowane automatycznie po dodaniu profilu lekarza lub pacjenta.
- Tabela `PasswordHistory` nadal przechowuje maksymalnie 3 ostatnie hashe hasel uzytkownika.
