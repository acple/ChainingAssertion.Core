dotnet clean ../ChainingAssertion.Core.sln
dotnet restore ../ChainingAssertion.Core.sln
dotnet pack -c Release -o ../nuget ../ChainingAssertion.Core.MSTest/ChainingAssertion.Core.MSTest.csproj
dotnet pack -c Release -o ../nuget ../ChainingAssertion.Core.NUnit/ChainingAssertion.Core.NUnit.csproj
dotnet pack -c Release -o ../nuget ../ChainingAssertion.Core.Xunit/ChainingAssertion.Core.Xunit.csproj
