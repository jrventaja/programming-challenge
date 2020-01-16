# Programming Challenge Media Catalog

Steps to run/test

## Step 1

Unzip the zip/gz files with the compressor of your choice (I use 7zip). This will result in 3 .tsv files.

## Step 2

Set the variables at DataLoaderTool\appsettings.json:
Section "Paths":
"PeopleData" should refer to the "name.basics" file;
"MediaData" should refer to the "title.basics" file;
"RatingData" should refer to the "title.ratings" file.\

e.g:"PeopleData": "C:\\Users\\joser\\Documents\\Projects\\programming-challenge\\dataset\\name.basics.tsv\\data.tsv",
    "MediaData": "C:\\Users\\joser\\Documents\\Projects\\programming-challenge\\dataset\\title.basics.tsv\\data.tsv",
    "RatingData": "C:\\Users\\joser\\Documents\\Projects\\programming-challenge\\dataset\\title.ratings.tsv\\data.tsv"


## Step 3

(Install SQL SERVER EXPRESS if you don't have it, and then) run the scripts contained at "DDL Scripts.sql" individually.

## Step 4

Run "dotnet build" and then "dotnet run" at cmd while at the solution folder. (You need .NET FRAMEWORK CORE 2.1 installed)
It will take some time to load, because of the data size. After that, all tables are loaded with the data that was provided.

## Step 5

Time to run the service application, so we can receive requests through our API. Edit the file at "MediaCatalog\MediaCatalog\appsettings.json" setting the ConnectionStrings/Storage/Server with the address your SQL Server is listening (usually LOCALHOST\SQLEXPRESS if you are not using a VM to run the application - e.g Docker).
Use "dotnet build" and then "dotnet run" at MediaCatalog solution.

## Step 6

Your application will be listening on port 44304 and is able to be tested.


# API Documentation

## List media categories

URL: api/categories

REQUEST TYPE: GET

No parameters supported

Response:

200(OK) + Content

Content example:


[{"id":1,"name":"Documentary"},{"id":2,"name":"Short"},{"id":3,"name":"Animation"},{"id":4,"name":"Comedy"},{"id":5,"name":"Romance"},{"id":6,"name":"Sport"},{"id":7,"name":"Action"},{"id":8,"name":"News"},{"id":9,"name":"Drama"},{"id":10,"name":"Fantasy"},{"id":11,"name":"Horror"},{"id":12,"name":"Biography"},{"id":13,"name":"Music"},{"id":14,"name":"War"},{"id":15,"name":"Crime"},{"id":16,"name":"Western"},{"id":17,"name":"Family"},{"id":18,"name":"Adventure"},{"id":19,"name":"History"},{"id":20,"name":"Mystery"},{"id":22,"name":"Sci-Fi"},{"id":23,"name":"Thriller"},{"id":24,"name":"Musical"},{"id":25,"name":"Film-Noir"},{"id":26,"name":"Game-Show"},{"id":27,"name":"Talk-Show"},{"id":28,"name":"Reality-TV"},{"id":29,"name":"Adult"}]

## List top rated movies
URL: api/movies/top

REQUEST TYPE: GET

Supported query string parameters: year (number), page (number 1 to n - default: 1), pageSize (number 1 to n - default: 10).

e.g: api/movies/top?year=2019, api/movies/top?page=3&pageSize=5

Response:

200(OK) + Content

400(Bad Request)

Content example:
{"pageNumber":1,"pageSize":2,"content":[{"id":6735740,"title":"Love in Kilnerry","year":2019,"runtimeMinutes":100,"rating":10.00,"isAdult":false,"director":"Kimberly Graham","actors":["Adam McNulty","Addison LeMay","Anne Pearna","Bari Hyman","Bradley A. Boyd","Brandon Brumm","Brian Donovon","Courtney Bissonette","Dana Marisa Schoenfeld","Daniel Keith","Debargo Sanyal","Denise Coutre","Dominic Paolillo","Emilee Dupre","Franklin Kavaler","George Olsen","Isabella Romero","James McWilliams","James Patrick Nelson","Jeremy Fernandez","JohnMichael Mitchell","Juan David Pinzon","Kate Quisumbing","Kathy Searle","Kellen Ryan","Lawrence R. Leritz","Leon Morgan","Leona Ross","Maaike Snoep","Meghan Locwin","Nicholas Franks","Nick Sanchez","Odell Cureton","Reiko Johnson","Roger Hendricks Simon","Ross Matthei","Sam Ellis","Scott Watson","Sheila Stasack","Steven Scott","Sybil Lines","Todd Butera","Tony Triano","Virginia Johnson"],"genres":["Comedy"]},{"id":7535666,"title":"Retrocausality","year":2019,"runtimeMinutes":null,"rating":9.60,"isAdult":false,"director":"Kristian Denver Diaz","actors":["Aliser Ramos","Elisabeth Rioux","Kristian Denver Diaz","Logan Gervais"],"genres":["Drama","Romance"]}]}

## List movies by category
URL: api/movies

REQUEST TYPE: GET

Supported query string parameters: category (number - Must be informed, at this current version)

e.g.: api/movies?category=10

Response:

200(OK) + Content

400(BadRequest)

Content example:

{"pageNumber":1,"pageSize":2,"content":[{"id":3419,"title":"The Student of Prague","year":1913,"runtimeMinutes":85,"rating":6.50,"isAdult":false,"director":"Guido Seeber","actors":["Fritz Weidemann","Grete Berger","Hanns Heinz Ewers","John Gottowt","Lothar Körner","Lyda Salmonova","Paul Wegener"],"genres":["Drama","Fantasy","Horror"]},{"id":3643,"title":"The Avenging Conscience: or 'Thou Shalt Not Kill'","year":1914,"runtimeMinutes":78,"rating":6.50,"isAdult":false,"director":null,"actors":[],"genres":["Crime","Drama","Horror"]}]}
