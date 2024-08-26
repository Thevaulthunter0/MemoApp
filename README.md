<h1>MemoApp</h1>
<h2>OverView</h2>
<p>Small web application that let user create and assigned small message, reminder and memo to work group.</p>

- Home page by user

- Moderator page
  
- Signing page

- Different listing

<h2>Technologie</h2>
<p>A complete project using microsoft ASP.NET core MVC that used C# and a SQL microsoft server for the database. The UI is made in Razor(.cshtml), a mix of C# and web base technologie.(Html, css, bootstrap)</p>
<p>ASP.NET core propose a librairy name EntityFramework that easily link your Models with the database. Using EntityFramework will save alot of time bypassing the SQL request process and making modification to your model is easy with the usage of migration.</p>

<h2>How does it work</h2>
<p>Its based on the basic MVC model and use the URL as a routing mechanism. Eatch controller as differents views, the basic routing would look like this. If you have a controller name PizzaController you would make a 'Pizza' folder inside of the 'View' folder. Inside of the controller you would have different function call that have the same name of the views. For exemple : LocalHost1234/Pizza/Index, LocalHsot1234/Pizza/List. Inside the controller you tell witch model will be used inside of the view.</p>
<p>EntityFramework connect to your BD using a connectionString and base on the model that you assign inside of the DbContext, it will create a migration that can be easily upload to your database when modification need to be done. Its basicly drop the whole db and remake it with the modification that you have done.</p>
<p>I created a service layer so that the controllers dont directly called the DB.</p>

<h2>DataBase</h2>
![MemoAppData drawio](https://github.com/user-attachments/assets/c3ffa6bc-9ec9-4d7f-a331-bf0a278b76b6)
