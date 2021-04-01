# XTrimCalculator

## Setup

```
dotnet new --install Microsoft.DotNet.Web.ProjectTemplates.3.1::3.1.13

git clone https://github.com/DevenKShah/XTrimCalculator.git

cd XTrimCalculator

dotnet new gitignore

md src

cd src

dotnet new sln -n XTrimCalculator  

dotnet new classlib -n XTrimCalculator.Application  --framework netcoreapp3.1

dotnet new classlib -n XTrimCalculator.Domain   --framework netcoreapp3.1

dotnet new classlib -n XTrimCalculator.Infrastructure  --framework netcoreapp3.1

dotnet new console -n XTrimCalculator.ConsoleApp  --framework netcoreapp3.1

dotnet new xunit -n XTrimCalculator.UnitTests  --framework netcoreapp3.1

dotnet sln add .\XTrimCalculator.Application

dotnet sln add .\XTrimCalculator.Domain

dotnet sln add .\XTrimCalculator.Infrastructure

dotnet sln add .\XTrimCalculator.ConsoleApp

dotnet sln add .\XTrimCalculator.UnitTests

dotnet add  .\XTrimCalculator.Application  reference  .\XTrimCalculator.Domain

dotnet add  .\XTrimCalculator.Infrastructure  reference  .\XTrimCalculator.Domain

dotnet add  .\XTrimCalculator.ConsoleApp  reference  .\XTrimCalculator.Domain

dotnet add  .\XTrimCalculator.UnitTests  reference  .\XTrimCalculator.Domain  .\XTrimCalculator.Application  .\XTrimCalculator.Infrastructure

```