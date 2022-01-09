To start the project open demoProject.csproj and run.

To sort the grid, click on Post Title or Author
To mark as favorite click on the star icon.
To delete a post click on the delete icon and confirm.
To see a post detail view, click on any row with a post from 
the grid view.

In post detail view.
To go Back press the back button at the top
To delete a comment click on the delete icon and confirm.
To add a comment use the form on the left side
	* Message is required
	* Email is required and must be a valid email.




The URLs to jsonplaceholder.typicode.com APIs are set in the 
appsettings.json under "ResourceUrls"

Each time when the application starts it re-creates the datalayer
Meaning each time the project starts requests to 
jsonplaceholder.typicode.com are made and the local .json files
are re-created.

The backend project already contains a production build of the client app.
To do a new client app production build do the following:
1. install ng
2. go to demoProject\ClientApp\buildscripts and run build.bat
This will trigger a production build of ClientApp and place it
on inside wwwroot/cliet-app of the Razor Pages.