# Opis ostatniej poprawki - role i specjalizacje

## Powod poprawki

W poprzedniej wersji baza miala nie do konca spojna logike rol i specjalizacji. Uzytkownik mogl miec role `Lekarz` w tabeli `UserRoles`, ale nie musial miec profilu w tabeli `Doctors`. Mogla tez wystapic odwrotna sytuacja: rekord w `Doctors` bez roli `Lekarz`.

Dodatkowo specjalizacja lekarza byla przechowywana bezposrednio w tabeli `Doctors`, co ograniczalo lekarza do jednej specjalizacji.

## Co zostalo zmienione

Dodano nowa tabele:

```sql
DoctorSpecializations
```

Tabela przechowuje specjalizacje przypisane do lekarza:

- `DoctorID`
- `SpecializationID`
- `IsPrimary`

Dzieki temu jeden lekarz moze miec wiele specjalizacji, a jedna z nich moze byc oznaczona jako glowna.

## Kompatybilnosc ze stara aplikacja

Kolumna:

```sql
Doctors.SpecializationID
```

zostala zostawiona. Stary kod aplikacji, ktory korzysta z tej kolumny, nadal powinien dzialac.

Nowa tabela `DoctorSpecializations` jest zalecanym miejscem do pobierania pelnej listy specjalizacji lekarza.

## Dodane triggery

Dodano automatyczna synchronizacje:

- `TR_Doctors_AssignLekarzRole` - po dodaniu lekarza automatycznie dodaje role `Lekarz`;
- `TR_Patients_AssignPacjentRole` - po dodaniu pacjenta automatycznie dodaje role `Pacjent`;
- `TR_Doctors_SyncPrimarySpecialization` - synchronizuje `Doctors.SpecializationID` z `DoctorSpecializations`;
- `TR_DoctorSpecializations_UpdateDoctorPrimary` - aktualizuje `Doctors.SpecializationID`, gdy zmieni sie glowna specjalizacja w `DoctorSpecializations`.

## Efekt koncowy

Logika bazy jest teraz bardziej spojna:

- role systemowe sa nadal w `UserRoles`;
- profil lekarza jest w `Doctors`;
- profil pacjenta jest w `Patients`;
- specjalizacje lekarza sa w `DoctorSpecializations`;
- rola `Lekarz` lub `Pacjent` dodaje sie automatycznie po utworzeniu odpowiedniego profilu.

Poprawka zostala wykonana tak, aby ograniczyc koniecznosc zmian w istniejacej aplikacji.
