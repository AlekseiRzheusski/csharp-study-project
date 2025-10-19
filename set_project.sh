dotnet new sln -n MySolution
dotnet new classlib -n MyLibrary
dotnet new console -n MyApp
# add library and app to the sln
dotnet sln MySolution.sln add MyLibrary/MyLibrary.csproj
dotnet sln MySolution.sln add MyApp/MyApp.csproj
# add library to the app
dotnet add MyApp/MyApp.csproj reference MyLibrary/MyLibrary.csproj

dotnet build MySolution.sln
dotnet run --project MyApp