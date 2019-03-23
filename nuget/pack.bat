@echo
cd /d %~dp0
dotnet clean ../ChainingAssertion.Core.sln
dotnet pack -c Release -o ../nuget ../ChainingAssertion.Core.sln
