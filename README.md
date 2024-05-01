## Project: GoodHambuger
 
## Description
 
>Project developed for tests purpose on a tech company
 
## Follow the instructions below to run the project

```console
 
# Clone the repository
 
$ git clone <repo>
 
# open the project path on terminal/cmd
 
$ cd <folder>

# Execute the following docker-compose command to run a MySql database
 
$ docker-compose up -d
  
# Execute the migrations - My database is hosted on Docker/WSL(Ubuntu)

$ dotnet ef database update -s .\WebApi\WebApi.csproj  -p .\Infra.Repository\ -v
 
# Run the API 
 
$  dotnet run --project .\WebApi\WebApi.csproj
 
# The application will be run on port 5000
 
 <http://localhost:5000>
 
# Swagger documentation
 
<http://localhost:5000/swagger/index.html>
