# Semesterprosjekt for Kartverket av Gruppe 17

## Applikasjonens oppsett
Teknologier brukt i applikasjonen er som følger:

* Docker: For å bygge opp og kjøre applikasjonen og databasen.
* MariaDB: Relasjonsdatabase for lagring av data.
* Applikasjonen er utviklet som en ASP.NET MVC-app i versjon 8.0 av .NET-rammeverket.
* En IDE som støtter .NET. E.g. Visual Studio eller Rider.

## Hvordan applikasjonen kjøres
For å kjøre applikasjonen må følgende gjøres:

### Windows
  1. Installer Docker på systemet som skal kjøre applikasjonen
  2. Klone koden fra repositorien til systemet som skal kjøre applikasjonen
  3. Åpne applikasjonen i Visual Studio eller Rider
  4. Kjøre applikasjonen med Docker Compose for å starte opp både applikasjonen og databasen i egne containere.

 ### Mac
Grunnet en ukjent feil får en ikke til å kjøre applikasjonen i en Docker container med Mac. For at applikasjonen skal kunne kjøres må den kjøres lokalt med en kobling til MariaDB-container som må settes opp manuelt. 
  1. Følg samme steg frem til og med steg 3 for Windows.
  2. Endre connection string på linje 28 i Program.cs til "MariaDbConnection".
  3. Opprett en MariaDB Docker container med kommandoen under:
`
          docker run --name mariadb \
        -e MYSQL_ROOT_PASSWORD=kartverket \  
        -e MYSQL_DATABASE=KartverketDb \
        -p 3307:3306 \
        -v mariadb_data:/var/lib/mysql \
        -d mariadb:latest`

  4. Start applikasjonen lokalt fra IDEen med HTTPS. 

## Funksjonaliteter i applikasjonen

### Brukerregistrering
Brukere kan registrere egne brukere. Epost benyttes som brukernavn.

### Innlogging/Autentisering
For å få tilgang til å bruke applikasjonen må brukere logge inn. 
For test er det seeded to testbrukere inn i databasen:

Testbruker saksbehandler:
Email: casehandler@test.com,
Passord: CaseHandler@123

Testbruker innmelder:
Email: submitter@test.com,
Passord: Submitter@123

### Kartinnmelding

Kartet bruker nettleseren sin innebygde geolokasjons API for å vise startposisjon i kartet.
Hvis dette ikke aktiveres er default startposisjon koordinatene til Kristiansand.


Alle brukertyper kan opprette innmeldinger for feil i kartlag.

Innmeldinger kan slettes av brukere med innmelderrollen hvis innmeldingen er i status Pending og det er de selv som har opprettet innmeldingen.

Alle saksbehandlere kan slette alle innmeldinger som er i status Pending, men hvis en saksbehandler har startet behandling, er det KUN saksbehandleren som er knyttet til innmeldingen som kan slette.

Saksbehandlere kan knytte seg selv til innmeldinger i status Pending ved å starte behandling. Har de startet behandling kan de fullføre behandling, tildele saken til en annen saksbehandler eller slette saken. Saksbehandlere som ikke er knyttet til innmeldingen kan se innmeldingen, men ikke gjøre noe med den. 


### Innmeldingsoversikt 
Innmeldingsoversikten består av to varianter. En mer standard tabellvisning og en mer visuell kartvisning.

* Tabellvisning
  - Innmeldere: Får opp en oversikt over alle innmeldinger de har gjort. De kan gå inn på hver individuell innmelding.
  - Saksbehandlere: Kan se alle innmeldinger samt gå inn på hver individuell innmelding.
  - Hver kolonne i tabellvisningen kan sortere, og det er mulig å filtrere innmeldinger basert på status og      kartlag. Det finnes også en søkefunksjon som kan benyttes for å søke etter nøkkelord blant sakene.
 
* Kartvisning
  - Fungerer likt for alle brukere, men innmeldere (ikke saksbehandlere) kan bare se sine egne innmeldinger.
  - Kartvisningen viser alle innmeldinger som merker basert på senter koordinat fra GeoJson-objektet. Disse grupperes automatisk etter zoom-nivå. 
  


 
