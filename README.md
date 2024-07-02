The idea behind "PostWall" is to be able to support a 2010s style page serving "memes" where the user just needs a Email, username and password to share funny content.

of course this project does not take in account many problems that could show if someone ever wanted to create and support such a website (Security, Legality of the content uploaded)

This small project showcases the use of asp.net APIs and Entity Framework Core as well as ASP.NET Identity to expose the APIs requiring a cookie to have access to resources like post creation or deletion

It's written in .NET 8 and uses Libraries such as: 
-EntityFramework Core accessing a SQLite Database
-Automapper for ease of transformation of data from DTOs to the EF models
-ASPNetCore Identity for a simple showcase of authentication using Cookies which would be the best approach (in my opinion) in case you wanted to consume this APIs from the actual WebSite that shows the "memes", if it was ever required to use a authorization protocol such 
 a OAuthV2 this package would just require configuration.


If I was to implement this project in real life these are some of the changes or additions I would do to this project:
-Use a Cloud Based DB (MySQL or postgresql)
-Containarize the solution for ease of deployment in different cloud providers
-Generate roles of "Admin", "Mod" and "User"
-Add functionality for banning Users and deleting Posts as well as Admin controls.
-The actual Media Uploading would be managed a BlazorAPP(which would expose the actual Web Page) or a Function (Ex. deployed in Azure Functions) in case I wanted to Develop the webpage with a Client side technology like Angular or React (or JQuery even)

As I mentioned before this project would not be viable as it would try to compete with the many social media platform that already exist (Instagram, Twitter, Discord, etc)
Also it would need protection against BOT (Captcha) and I would explore the posibility to incorporate an Image recognition software in attempts to filter not desired Media such as NSFW

There are many things still being overlook, such as scallability of the different projects but perhaps thinking about this is too much for a simple portfolio project which aim is to showcases skills in C# and the latest .NET also i don't want this README to become a wall of text.
