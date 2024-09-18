<h1>MemoApp</h1>
<h2>OverView</h2>
<p>Small web application that let user create and assigned memo to work groups.</p>

- Home page by user

- Moderator page(CRUD operation on the DB)

- Different listing

- Adding a custom pdf file to the memo

- Creating reports

<h2>Technologie</h2>
<p>A complete project using microsoft ASP.NET core MVC that used C# and a SQL microsoft server for the database. The UI is made in Razor(.cshtml), a mix of C# and web base technologie.(html, css, bootstrap, javascrip)</p>
<p>ASP.NET core propose a librairy name EntityFramework that easily link your Models with the database. Using EntityFramework will save alot of time bypassing the SQL request process. Also, making modification to your model is easy with the usage of migration.</p>

<h2>How does it work</h2>
<p>Its based on the basic MVC model and use the URL as a routing mechanism. Eatch controller as differents views, the basic routing would look like this. If you have a controller name PizzaController you would make a 'Pizza' folder inside of the 'View' folder. Inside of the controller you would have different function call that have the same name of the views. For exemple : LocalHost1234/Pizza/Index, LocalHsot1234/Pizza/List. Inside the controller you tell witch model will be used inside of the view.</p>
<p>EntityFramework connect to your BD using a connectionString and base on the model that you assign inside of the DbContext, it will create a migration that can be easily upload to your database when modification need to be done. Its basicly drop the whole db and remake it with the modification that you have done.</p>
<p>I created a service layer so that the controllers dont directly called the DB.</p>

<h2>Model</h2>
<img src="https://github.com/user-attachments/assets/c3ffa6bc-9ec9-4d7f-a331-bf0a278b76b6" alt="bd diagram">

<h2>Documentation</h2>

- ASP.NET CORE https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0

- Bootstrap https://getbootstrap.com/docs/5.3/getting-started/introduction/

- MicrosoftSql https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16

- EntityFrameWork https://learn.microsoft.com/en-us/ef/

<h2>Migration commands</h2>

<code>dotnet ef migrations add MigrationName</code>

<code>dotnet ef database update</code>

<code>dotnet ef migrations remove</code>

<code>dotnet ef migrations list<</code>


