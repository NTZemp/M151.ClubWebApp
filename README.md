# M151.ClubWebApp
## Projektbeschrieb
### Idee
Es soll eine Webseite geben, auf welcher jemand einen Verein anmelden kann. Es kann eine
Mitgliederliste für einen Verein erstellt werden. Man kann sich also anmelden um die
Mitgliederliste für die Vereine, in welchen man mit diesem Account ein Mitglied ist, anschauen
zu können. Auf der Startseite soll ein Tutorial angezeigt werden, in welchem beschrieben wird
wie man einen Verein und die dazugehörende Mitgliederliste erstellt.
Admins können Leute dem Verein hinzufügen oder entfernen. Hinzugefügt wird man per
Benutzername. Der hinzugefügte Benutzer bekommt nach dem anmelden eine
Benachrichtigung, ob er die Einladung annehmen will.

### Personengruppen
Die Vereinsapp hat nur eine Art von Benutzer. Doch innerhalb eines Vereines wird zwischen Administratoren und Mitgliedern unterschieden.  

### User Stories
* Als eingeloggter Benutzer möchte ich einen Verein erstellen können und diesem
Benutzer hinzufügen, um einen Verein zu initialisieren.
* Als eingeloggter Administrator eines Vereins, soll es möglich sein Benutzer als
Mitglieder dem Verein, von welchem der eingeloggte Benutzer Admin ist,
hinzuzufügen, um neue Vereinsmitglieder zu erfassen. 
* Als eingeloggter Administrator eines Vereins, soll es möglich Mitglieder aus dem
Verein, von welchem der eingeloggte Benutzer Admin ist, zu löschen, um Mitglieder,
die den Verein verlassen, zu entfernen.
* Als eingeloggter Benutzer, soll es möglich sein Vereinseinladung anzunehmen oder
abzulehnen, um nicht willkürlichen Vereinen zugefügt zu werden.

### Technologien
* Backend - [Asp.Net Core Web Api](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1)
* Frontend - [Angular](https://angular.io)
* ORM - [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* Datenbank - [MSSQL Server Community](https://www.microsoft.com/de-de/sql-server/sql-server-2019)

