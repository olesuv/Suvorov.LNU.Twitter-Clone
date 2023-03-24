# Twitter Clone

A project as a university exam for "Web-tecnologies" class. Clone of popular social network. Project should has basic functions and features, algorithms which are similar or as an official Twitter has too. Current clone was developed only for practice interest.

## Installation

To download .NET visit [this link](https://dotnet.microsoft.com/download) and follow instructions for your current OS.

## Usage

```bash
git clone https://github.com/plxgwalker/Suvorov.LNU.Twitter-Clone.git
cd Suvorov.LNU.Twitter-Clone
dotnet run
```

## Stack

- [.NET](https://dotnet.microsoft.com/) - free, open-source, cross-platform framework for building modern apps and powerful cloud services.
- [Azure](https://azure.microsoft.com/) - cloud computing platform and a set of services provided by Microsoft for building, deploying, and managing applications and services through Microsoft-managed data centers.
- [Entity Framework](https://learn.microsoft.com/ef) - object-relational mapping (ORM) framework for .NET developers that enables them to work with databases using .NET objects, simplifying the process of data access and manipulation.
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-2019) - relational database management system (RDBMS) developed by Microsoft that uses SQL (Structured Query Language) to manage and manipulate data, including storing, querying, and retrieving data.
- [App Service](https://azure.microsoft.com/products/app-service/) - set of cloud-based services provided by Microsoft that enable developers and businesses to build, deploy, and manage applications and services in the cloud, including computing, storage, networking, databases, analytics, and more.
- [MSTest](https://learn.microsoft.com/dotnet/core/testing/unit-testing-with-mstest) - unit testing framework for .NET developers that allows them to write and execute automated tests to ensure the correctness and reliability of their code.

## Decomposition of tasks and their current status

1. **Azure deployment**
    - [x] Initial deploy ptoject.
	- [x] Database hide connection string.
	- [x] Logs and Application Insights.
	- [ ] CI/CD.
	
2. **Registration Page**
    - [x] CreateUserRequest.cs
	- [x] RegisterUser - Razor Pages.
    - [x] Input validation(Check if user written correct regex of name, username, email, password and if confirm password is similar to password). 
	- [x] Input validation(Check if information already in use, like "User with current email is already signed up").
	- [ ] Sign up with Google.
	- [ ] Link to Login Page("Already signed up?").

3. **Login Page**
	- [ ] LoginUserRequest.
	- [ ] LoginUser - Razor Pages.
	- [ ] Input validation(Check user email and password from database).
	
4. **Home Page & Features**
	- [ ] Posts recomendations. Recomendations of posts with hashtag which you like and which posts likes people which liked similar to you post.
	- [ ] Friends recomendations.
	- [ ] Popular hashtags.
	- [ ] Search of users, posts.
	- [ ] Post count of likes, ect.

5. **Profile page**
	- [ ] List of followers.
	- [ ] List of whom user follows and also follows current person.
	- [ ] List of whom reads.
	- [ ] List of posts.
	- [ ] List of posts which user liked.
