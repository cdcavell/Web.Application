# Web.Application
## ASP.NET 8.0 MVC Web Development

<hr />

[![GitHub license](https://img.shields.io/github/license/cdcavell/web.application)](https://github.com/cdcavell/web.application/blob/main/LICENSE)
[![GitHub tag (latest by date)](https://img.shields.io/github/v/tag/cdcavell/web.application)](https://github.com/cdcavell/web.application/tags)
![GitHub top language](https://img.shields.io/github/languages/top/cdcavell/web.application)
![GitHub language count](https://img.shields.io/github/languages/count/cdcavell/web.application)
[![CodeQL Analysis](https://github.com/cdcavell/web.application/workflows/CodeQL%20Analysis/badge.svg)](https://github.com/cdcavell/web.application/actions?query=workflow%3A%22CodeQL+Analysis%22)

<hr />

Target Framework is [ASP.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0). Developed and built in a Windows environment utilizing [Visual Studio Community 2022](https://visualstudio.microsoft.com/vs/) source-code editor. Repository is [Git](https://git-scm.com/) utilizing [git-flow](https://github.com/nvie/gitflow) extension to provide high-level repository operations for [Vincent Driessen's branching model](https://nvie.com/posts/a-successful-git-branching-model/).

This work is [licensed](https://github.com/cdcavell/cdcavell.dev/blob/main/LICENSE) under the [MIT License](https://opensource.org/licenses/MIT).

Source Code documentation is found in repository [Wiki](https://github.com/cdcavell/cdcavell.dev/wiki) section. 

<hr />

_If you are cloning this repository, please enter commands as follows:_

```
$ git clone --recurse-submodules https://github.com/cdcavell/cdcavell.dev.git

$ cd cdcavell.dev

$ git flow init -d
```

<hr />

__Database Migrations CLI Instructions__
<br />
_Before you can use the CLI tools on project, you'll need to add the `Microsoft.EntityFrameworkCore.Design` package to it._
<br />
<br />_Install EF Core Tools:_ `dotnet tool install --global dotnet-ef`
<br />_Upgrade EF Core Tools:_ `dotnet tool update --global dotnet-ef`

_To Initialize:_

```
$ dotnet ef migrations add InitialCreate_ApplicationDb --context ApplicationDbContext --output-dir Data/Migrations/ApplicationDb
```

_To Update:_

```
$ dotnet ef migrations add Update_ApplicationDb_YYYYMMDD --context ApplicationDbContext --output-dir Data/Migrations/ApplicationDb
```

<hr />

__Windows Event Log Instructions__
<br />
_Use PowerShell to issue following commands to create/configure event log (only needed once)_

```
$ New-EventLog -LogName cdcavell.dev -Source cdcavell.dev

$ Limit-EventLog -OverflowAction OverWriteAsNeeded -MaximumSize 20480KB -LogName cdcavell.dev
```

_Use PowerShell to issue following commands to create event log sources (only needed once)_

```
$ New-EventLog -LogName cdcavell.dev -Source dis.cdcavell.dev
$ New-EventLog -LogName cdcavell.dev -Source usr.cdcavell.dev
```

<hr />

| Build | Date | Description |
|-------|------|-------------|
| 1.0.5.2 | 05/16/2023 | __Update Build Scripts__ |
| 1.0.5.1 | 05/16/2023 | __Update GitHub Actions__ |
| 1.0.5.0 | 05/15/2023 | __Game Development - Sudoku__ |
| 1.0.4.0 | 05/14/2023 | __User Role Claims Development__ |
| 1.0.3.3 | 12/14/2022 | __Migrate to universeodon.com__ |
| 1.0.3.2 | 12/14/2022 | __Update Node.js v16, CodeQL Action v2__ |
| 1.0.3.1 | 12/13/2022 | __User Registration Development__ |
| 1.0.3.0 | 12/12/2022 | __User Registration Development__ |
| 1.0.2.1 | 10/15/2022 | __Block Harassing IP Addresses__ |
| 1.0.2.0 | 10/08/2022 | __Duende IdentityServer Development__ |
| 1.0.1.0 | 09/05/2022 | __Data Access Layer Development__ |
| 1.0.0.0 | 09/05/2022 | __Initial Development__ |
