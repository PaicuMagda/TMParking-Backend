## Pasul 1. Se instalează Git de pe site-ul https://git-scm.com/downloads

## Pasul 2

Se descarcă Visual Studio de la adresa https://visualstudio.microsoft.com/downloads/

## Pasul 3 

Se descarcă și se instalează .NET SDK 

## Pasul 4.

Se navighează folosind comanda "cd/path-ul-către-director" către folderul unde se se dorește atașarea proiectului.
Există varianta de a naviga în acel director , iar in bara unde se află ierarhia de fișiere se șterge tot și se introduce "cmd + Enter" . Imediat se va deschide Command Prompt cu calea către acel fișier.
         
## Pasul 5

Se rulează comanda `git clone https://github.com/PaicuMagda/TMParking-Backend.git`

## Pasul 6

După ce s-a descărcat proiectul se face dublu click pe fișierul TMParking-Backend.sln sau se deschide direct proiectul în Visual Studio.
         
## Pasul 7

În terminal se rulează comanda `dotnet build`

## Pasul 8

Se rulează comanda `dotnet run` și se va deschide automat pagina de Swagger unde se pot testa endpoint-urile.

## Pentru legarea la baza de date:

## P1

Se descarcă SQL Server de la adresa https://www.microsoft.com/en-us/sql-server/sql-server-downloads.

## P2 

Se deschide SQL Server și se copiază "Server name" care apare într-o fereastră la deschidere. 

## P3

În proiectul Backend se deschide fișierul appsettings.json și se înlocuiește în următoarea bucată de cod : 

  "ConnectionStrings": {
    "TMParkingConnectionString": "server= < aici se înlocuiește Server name >;Trusted_Connection=true;TrustServerCertificate=true;Initial Catalog=TMParking-Backend;Integrated Security=true"
  },

## P4

Se navighează din nou în proiectul din Visual Studio și se rulează următoarele comenzi în Package Manager Console 

             dotnet ef migrations add InitialCreate --project TMParking-Backend (sau denumirea proiectului așa cum e salvat) 
             
             dotnet ef database update  --project TMParking-Backend (sau denumirea proiectului așa cum e salvat)
