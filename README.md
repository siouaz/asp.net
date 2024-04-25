<p align="center">
  HanaGroup - siwar 
</p>

<br/><br/>

<p align="center">
  
</p>

---

# Getting Started

## Prerequisites

- .NET 8
- SQL Server 2014+
- Node.js : Latest / LTS
- Yarn 🧶
- Angular CLI 🔋 `yarn global add @angular/cli`

## Clone Repository

`git clone git@gitlab.upper-link.com:hanagroup/siwar.git`

OR

`https://gitlab.upper-link.com/hanagroup/siwar.gitt`

Or with any Git GUI of your choice (Sourcetree, Tower, GitKraken, ...)

🚨 ⚡ Right after the cloning of the repository, please set your account information in the Git configuration inside of your repository directory.

``` bash
git config set user.name "Your GitHub user name"
git config set user.email "Your GitHub primary email"
```

## User Secrets

The solution will only work with the following secrets :

``` bash
ConnectionStrings:Storage              # Azure Storage Account
ConnectionStrings:siwar            # Database Connection String
ConnectionStrings:siwar-Jobs       # Jobs Database Connection String
```

> Please contact your project manager in order to get the secrets corresponding to the keys above.

## Debugging

In a terminal, please run `yarn start` inside the `src/siwar` directory.

Next, start the ASP.NET Core Web App in your editor (in Debug mode preferably) or by running `dotnet run` or `dotnet watch run` for watch mode inside `src/siwar`.

## Database Migrations

Coming Soon ...

# Contributing

## Branching

Any new development should be made on another branch created from the `master`. The branch should have a semantic name related to the issue being addressed.

The branch name should be in kebab case and can be prefixed with the issue number.

- ✅ `my-awesome-feature`
- ✅ `7-my-second-awesome-feature`
- ❌ `MyBug`
- ❌ `my_bug`
- ❌ `11_MY_BUG`

## Checklist

### C#

Always have an eye on your build output for warnings. We don't want a lot of those knowing we already ignore the annoying ones 😉.

Also, please make sure that all the tests pass either by running them inside your IDE or by running `dotnet test` inside of your repository directory.

### Angular

The same applies here, tests should pass, so please `yarn test` or `ng test` for watch mode inside `src/siwar` before submitting your pull request.

In addition, please make sure that the linter passes by running `yarn lint` inside `src/siwar`.

### PR

Once everything is checked, your PR is the cherry 🍒 on top. It will be reviewed and if everything is ✅, it will be merged on the `master` branch and deployed to the staging environment.

> Please note that if you're working on a complex feature that requires specific functional or technical details (database migration, new configuration key value pair, etc.), you should add a description or a comment describing them.

**Work In Progress** PRs are also welcome as long as they're prefixed with *WIP:* so that they don't get reviewed.

### Post Success

Once your PR is merged, you can go back and checkout the `master` branch and pull the changes.
