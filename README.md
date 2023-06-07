# [Twitter Clone](https://twitter-clone-lnu.azurewebsites.net)

A project as a university exam for "Web-tecnologies" class. Clone of popular social network. Project should has basic functions and features, algorithms which are similar or as an official Twitter has too. Current clone was developed only for practice interest.


## :hammer_and_wrench: Stack

- [.NET](https://dotnet.microsoft.com/) - free, open-source, cross-platform framework for building modern apps and powerful cloud services.
- [Azure](https://azure.microsoft.com/) - cloud computing platform and a set of services provided by Microsoft for building, deploying, and managing applications and services through Microsoft-managed data centers.
- [Entity Framework](https://learn.microsoft.com/ef) - object-relational mapping (ORM) framework for .NET developers that enables them to work with databases using .NET objects, simplifying the process of data access and manipulation.
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-2019) - relational database management system (RDBMS) developed by Microsoft that uses SQL (Structured Query Language) to manage and manipulate data, including storing, querying, and retrieving data.
- [App Service](https://azure.microsoft.com/products/app-service/) - set of cloud-based services provided by Microsoft that enable developers and businesses to build, deploy, and manage applications and services in the cloud, including computing, storage, networking, databases, analytics, and more.
- [MSTest](https://learn.microsoft.com/dotnet/core/testing/unit-testing-with-mstest) - unit testing framework for .NET developers that allows them to write and execute automated tests to ensure the correctness and reliability of their code.
- [NuGet packages](https://learn.microsoft.com/nuget/) - type of software package used in the Microsoft .NET ecosystem, containing compiled code and other resources, and are used by developers to easily add functionality to their projects and share code between teams.


## :computer: Installation

To download .NET visit [this link](https://dotnet.microsoft.com/download) and follow instructions for your current OS.

## :wrench: Configuration

Open your system terminal and run commands:

```bash
git clone https://github.com/plxgwalker/Suvorov.LNU.Twitter-Clone.git
cd Suvorov.LNU.Twitter-Clone
```

Add your already deployed database connection string to files:

In `Suvorov.LNU.TwitterClone.Database/NetworkDbContext.cs` method `OnConfiguring(DbContextOptionsBuilder options)` add line of code which below. Instead of `ConnectionString` add your database connection string. Line of code: `options.UseSqlServer("ConnectionString");`. Method should look like that:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		options.UseLazyLoadingProxies();
		options.UseSqlServer("ConnectionString");
	}
```

In `Suvorov.LNU.TwitterClone.Web/appsettings.json` in `"ConnectionStrings"` add line: `"NetworkConnection": "ConnectionString"`. Instead of `ConnectionString` add your database connection string. Code shluld look like this:

```json
  "ConnectionStrings": {
    "NetworkConnection": "ConnectionString"
  }
```

Than (if you have [App Services](https://azure.microsoft.com/products/app-service/)) add line of your connection string to application insights. Code in `Suvorov.LNU.TwitterClone.Web/appsettings.json` should look something like that, but also instead of `ConnectionString` should be yours.

```json
"ApplicationInsights": {
    "LogLevel": {
      "Default": "Information"
    },
    "ConnectionString": "ConnectionString"
  },
```

Also should be installed list of [NuGet packages](https://learn.microsoft.com/nuget/) to folder/project `Suvorov.LNU.TwitterClone.Database`.
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Proxies
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools


## :rocket: Usage

Than after installation of [.NET](https://dotnet.microsoft.com/) and all configuration below you can run code running command in your terminal or IDE:

```sh
dotnet run
```

## :pencil: Decomposition of tasks

1. **Azure deployment**
	- [x] Initial deploy ptoject.
	- [x] Database hide connection string.
	- [x] Logs and Application Insights.
	- [x] CI/CD.
	
2. **Registration Page**
	- [x] CreateUserRequest.cs
	- [x] RegisterUser - Razor Pages.
	- [x] Input validation(Check if user written correct regex of name, username, email, password and if confirm password is similar to password). 
	- [x] Input validation(Check if information already in use, like "User with current email is already signed up").
	- [ ] Sign up with Google.
	- [x] Link to Login Page("Already signed up?").

3. **Login Page**
	- [x] LoginUserRequest.
	- [x] LoginUser - Razor Pages.
	- [x] Input validation(Check user email and password from database).
	- [x] Password hashing.
	
4. **Home Page**
	- [x] List of following people's posts.
	- [ ] List of recomendation posts.
	- [ ] List of recomendation people.
	- [x] List of popular hashtags.
	- [ ] Searchbar.
	
5. **Profile page**
	- [x] Count of followers.
	- [x] Count of followings.
	- [ ] Dispaly list of whom user follows and also follows current person.
	- [ ] Display list of whom reads.
	- [x] List of posts.
	- [ ] List of posts which user liked.
	
6. **Post Page**
	- [x] Count of likes, ect.
	- [x] Post text info.
	- [ ] Post media info(mb).
	- [x] Hashtags.
	
7. **Features and algorithms**
	- [ ] Posts recomendations. Recomendations of posts with hashtag which you like and which posts likes people which liked similar to your post.
	- [ ] Friends recomendations. When someone follows your friend, probably you may know this 'someone'. 
	- [ ] Search of users, posts.

8. **Integration with OpenAI**
	- [x] Post generation.
	- [x] Hashtags generation.


## :iphone: Contact me

:email: [Email](mailto:olegsuv.ukr@gmail.com)

:calling: [Telegram](https://t.me/suph0mi3)

 ## Lab 3
**Task/s:**
- Change `Home` page. Changed home page apperreance.
- Change `Post button`. Redesigned post button.
- Change `Post area`. Made borders for post apperreance.
Kozariz Volodymyr
My project [Edelveis](https://github.com/odvova/Edelveis).
[Pull request](https://github.com/plxgwalker/Suvorov.LNU.Twitter-Clone/pull/3)

## Lab 4
- Reviewer: Voievoda Vladislav 
- My project [project](https://github.com/Vladislav43/WEB)
- [Pull request](https://github.com/Vladislav43/Suvorov.LNU.Twitter-Clone/pull/1)

## Lab 4
- Reviewer: Kozariz Volodymyr
- My project [Edelveis](https://github.com/odvova/Edelveis).
- [Pull request](https://github.com/odvova/Suvorov.LNU.Twitter-Clone/pull/1)
