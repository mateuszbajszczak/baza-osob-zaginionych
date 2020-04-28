# Baza Osób Zaginionych

## struktura projektu
### Kontrolery
- OsobyZaginioneController - obsługa listy i dodawania osób zaginionych; logowanie i rejestracja
- AdminController - obsługa listy, dodawania, usuwania i edycji klientów
### Modele
- Uzytkownik - odzwierciedla klienta lub administratora systemu (id,email,haslo,aktywny,admin)
- OsobaZaginiona - odzwierciedla osobe zaginioną (id,imie,nazwisko,opis,zdjecie,plec)
### Widoki
#### OsobyZaginione
- Index 
- Create
- List
#### Admin
- Index
- Create
- Details
- Edit
- Delete

## na start
Razem z systemem dostarczone są dwie bazy danych. Dwie przykładowe osoby zaginione i jedno konto klienta - administrator.

Konto administatora:
login: admin
haslo: admin

Każdy klient może się zarejestrować, dostep do bazy zaginionych otrzymuje po akceptacji administratora.