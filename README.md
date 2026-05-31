# E-Bankarstvo-Projekat
Zavrsni projekat iz programiranja. Aplikacija za e-bankarstvo.
Uroš Šojić, Jovan Lazić, Aleksandar Čolaković, Andreja Utović

Projekat obuhvata aplikaciju za e-bankarstvo sa sledećim mogućnostima:
1. Uplata na dinarski račun korisnika
2. Kupovina i prodaja stranih valuta (USD, EUR, GBP, CHF i CAD)
3. Prenos sredstava
4. Pregled stanja dinarskog i deviznog računa

# Uputstvo za korišćenje:
Kada se aplikacija pokrene, korisniku je predstavljen meni sa sledećim opcijama:
1. Pregled stanja
2. Uplata sredstava
3. Menjačnica
4. Prenos sredstava
5. Izlaz

## Pregled stanja
Korisniku se ispisuje stanje na dinarskom i deviznom računu.
Stanje deviznog računa je prikazano po valutama, npr:

0 USD
100 EUR
50.5 GBP
0 CHF
300 CAD

## Uplata sredstava
Korisniku se daje mogućnost da uplati određen iznos na dinarski račun
Nakon transakcije se ispisuje stanje na dinarskom računu i korisnik može da ponovo uplati sredstva na dinarski račun

## Menjačnica
Kada se odabere opcija menjačnica, korisniku se prvo daje izbor valute, gde pritom mora da se unese ime jedne od 5 ponudjenih valuta
Nakon toga, korisnik bira da li želi da proda ili kupi datu valutu i mora da unese 1 za prodaju ili 2 za kupovinu.
1. Prodaja - 
   Korisnik unosi iznos izabrane valute koju želi da proda.
   U slučaju da korisnik nema dovoljan iznos izabrane valute, ispisuje se greška, i ako korisnik nema uopšte izabrane valute može da unese 0 da izađe iz prodaje
2. Kupovina - 
   Korisnik unosi iznos izabrane valute koju želi da kupi.
   U slučaju da korisnik nema dovoljno dinara da kupi izabranu valutu, ispisuje se greška i može da unese 0 ako je dinarski račun prazan da izađe iz kupovine
Nakon transakcije se ispisuje stanje na deviznom i dinarskom računu i korisnik može da obavi ponovo transakciju u menjacnici

## Prenos sredstava
Korisnik unosi Naziv primaoca, mesto primaoca, broj računa primaoca, šifru plaćanja, iznos, model plaćanja i poziv na broj. 

Moguće šifre plaćanja su 253 i 289. Šifra plaćanja 253 zahteva da model plaćanja bude 97. 

Vrši se provera poziva na broj prema modelu plaćanja gde ako je model plaćanja 97 poziv na broj mora da bude 3 ili više cifara.

Banka uzima proviziju za svaki prenos sredstava. Provizija iznosi 20 dinara, odnosno 0,5% iznosa prenosa u slučaju da je iznos veći od 50.000,00 dinara.

Da bi izašao iz programa, korisnik na glavnom meniju može da izabere opciju 5.
