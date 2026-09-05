# Brandon & Hope Wedding Website

An ASP.NET Core Razor Pages wedding website for Brandon Davis-Barkus and Hope Leyva.

## What is included

- One-page wedding site with sections for the hero, love story, wedding details, schedule, travel, gallery, registry, RSVP, and FAQ.
- Real wedding details from Brandon and Hope's intake response.
- Modern romantic styling with papel pink, marigold orange, Talavera blue, and agave green accents.
- Built-in RSVP form for guest name, email, attendance, plus-one name, song request, and a note to the couple.
- Private RSVP CSV storage in `App_Data/Rsvps.csv`.
- GitHub Actions workflow starter for Azure App Service deployment.

## Open in Visual Studio

1. Open `WeddingSite.sln`.
2. Press F5 or choose the HTTPS launch profile.
3. Edit the public wedding content in `Pages/Index.cshtml.cs`.

## Private Files

Do not commit private response data to GitHub. The `.gitignore` excludes:

- `App_Data/Rsvps.csv`
- `*Responses*.xlsx`
- local build and publish folders

## Local Run

```powershell
dotnet restore WeddingSite.csproj --configfile NuGet.Config
dotnet run --project WeddingSite.csproj
```

## Publish Package

Create a hosting package with:

```powershell
dotnet publish WeddingSite.csproj --configuration Release --output publish
Compress-Archive -Path .\publish\* -DestinationPath .\artifacts\BrandonAndHope-WeddingSite-publish.zip -Force
```

## GitHub

Create a new GitHub repository, then push this folder:

```powershell
git init
git add .
git commit -m "Initial Brandon and Hope wedding site"
git branch -M main
git remote add origin https://github.com/YOUR-USER/YOUR-REPO.git
git push -u origin main
```

## Azure App Service Deployment

1. Create an Azure App Service for .NET 9.
2. In GitHub, add the Azure publish profile as the repository secret `AZURE_WEBAPP_PUBLISH_PROFILE`.
3. Update `AZURE_WEBAPP_NAME` in `.github/workflows/azure-webapp.yml`.
4. Push to `main`.

## RSVP Storage Note

The current RSVP storage is intentionally simple and writes to `App_Data/Rsvps.csv`. That is fine for a small single-instance site. For a more production-style setup, move RSVP storage to Azure SQL or another managed database before sending the public RSVP link widely.
