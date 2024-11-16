# Web.Application
## ASP.NET Core 9.0 MVC Web Development

<hr />

[![GitHub license](https://img.shields.io/github/license/cdcavell/Web.Application)](https://github.com/cdcavell/Web.Application/blob/main/LICENSE)
![GitHub top language](https://img.shields.io/github/languages/top/cdcavell/Web.Application)
![GitHub language count](https://img.shields.io/github/languages/count/cdcavell/Web.Application)

<hr />

Target Framework is [ASP.NET Core 9.0](https://dotnet.microsoft.com/download/dotnet/9.0). 
Developed and built in a Windows environment utilizing 
[Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/) source-code editor. 
Target Database is [SQLite](https://www.sqlite.org/).

**_This work is [licensed](https://github.com/cdcavell/Web.Application/blob/main/LICENSE) under the
[MIT License](https://opensource.org/licenses/MIT). Assets are licensed under their respective
[licensing](https://github.com/cdcavell/Web.Application/blob/main/ASSETS-LICENSES.md)._**

<hr />

__Database Migrations CLI Instructions__
<br />
_Before you can use the CLI tools on project, you'll need to add the `Microsoft.EntityFrameworkCore.Design` package to it._
<br />
<br />_Install EF Core Tools:_ `dotnet tool install --global dotnet-ef`
<br />_Upgrade EF Core Tools:_ `dotnet tool update --global dotnet-ef`

_To Initialize:_

```
$ dotnet ef migrations add Initial --context ApplicationDbContext --output-dir Migrations
```

_To Update:_

```
$ dotnet ef migrations add Update_YYYYMMDD --context ApplicationDbContext --output-dir Migrations
```

<hr />