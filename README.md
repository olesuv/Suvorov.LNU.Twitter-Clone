# Twitter Clone. Decomposition of tasks

### Status of tasks:

:white_check_mark: - Done.
<br />:sparkle: - In progress.
<br />:negative_squared_cross_mark: - Not started.

### Tasks:

1. :sparkle: **Azure deployment**
	- Initial deploy ptoject.
	<br /> Status: :white_check_mark:
	- Database hide connection string.
	<br /> Status: :white_check_mark:
	- Logs and Application Insights.
	<br /> Status: :white_check_mark:
	
2. :sparkle: **Registration Page**
	- CreateUserRequest.cs
	<br /> Status: :white_check_mark:
	- RegisterUser - Razor Pages.
	<br /> Status: :white_check_mark:
	- Input validation(Check if user written correct regex of name, username, email, password and if confirm password is similar to password). 
	<br /> Status: :white_check_mark:
	- Input validation(Check if information already in use, like "User with current email is already signed up").
	<br /> Status: :white_check_mark:
	- Sign up with Google.
<br /> Status: :negative_squared_cross_mark:
	- Link to Login Page("Already signed up?").
	<br /> Status: :negative_squared_cross_mark:

3. :negative_squared_cross_mark: **Login Page**
	- LoginUserRequest.
	- LoginUser - Razor Pages.
	- Input validation(Check user email and password from database).
	
4. :negative_squared_cross_mark: **Home Page & Features**
	- Posts recomendations. Recomendations of posts with hashtag which you like and which posts likes people which liked similar to you post.
	- Friends recomendations.
	- Popular hashtags.
	- Search of users, posts.
	- Post count of likes, ect.

5. :negative_squared_cross_mark: **Profile page**
	- List of followers.
	- List of whom user follows and also follows current person.
	- List of whom reads.
	- List of posts.
	- List of posts which user liked.
