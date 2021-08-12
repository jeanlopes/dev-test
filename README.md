# My Open Baking Api Application

This project was built wing the stack: MongoDB + .Net5 + Angular

To run, ensure that you have the .Net 5 sdk installed. 
It can be found here: https://dotnet.microsoft.com/download/dotnet/5.0
Download and install nodejs as wel: https://nodejs.org/en/
Entry on MyOpenBaking\MyOpenBaking directory and type

dotnet restore

dotnet build

dotnet run

swagger only works outside docker (non-production env).

To publish:

dotnet publish -o ..\dist

https://localhost:5001/swagger/index.html

https://localhost:5001/home

To run this on Docker, entry on MyOpenBaking directory, where is dockerfile and type

docker build -t myopenbank .

docker run -d -p 5000:80 myopenbank

To run tests, entry on MyOpenBaking\MyOpenBaking.Tests and type

dotnet test
