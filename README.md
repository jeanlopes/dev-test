# My Open Baking Api Application

This project was built wing the stack: MongoDB + .Net5 + Angular

To run, entry on MyOpenBaking\MyOpenBaking directory and type

dotnet run

swagger only works outside docker (non-production env).

To publish:

dotnet publish -o ..\dist

https://localhost:5001/swagger/index.html

https://localhost:5001/home

To run this on Docker, entry on MyOpenBaking directory, where is dockerfile and type

docker build -t myopenbank .

docker run -d -p 5000:80 myopenbank


