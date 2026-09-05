# Deployment Checklist

## Fastest Way To Share With Brandon And Hope

For Brandon and Hope to preview before launch:

1. Run the site locally in Visual Studio.
2. Share screenshots or screen share the preview.
3. Confirm names, date, venue, story wording, RSVP fields, and colors.

## Public Launch Path

Use Azure App Service for the cleanest C# hosting path.

1. Create a GitHub repository.
2. Push this project to GitHub.
3. Create an Azure App Service using the .NET 9 runtime.
4. Download the Azure publish profile.
5. Add it to GitHub as `AZURE_WEBAPP_PUBLISH_PROFILE`.
6. Update `AZURE_WEBAPP_NAME` in `.github/workflows/azure-webapp.yml`.
7. Push to the `main` branch.

## Manual Publish Package

If you want to upload the app manually instead of using GitHub Actions, use the zip at:

```txt
artifacts/BrandonAndHope-WeddingSite-publish.zip
```

That zip is generated from `dotnet publish` and contains the compiled site.

## Before Sending The Link To Guests

- Replace the placeholder hero image with Brandon and Hope's photos.
- Add registry links if they want them public.
- Confirm the RSVP deadline: February 28, 2027.
- Decide whether simple CSV RSVP storage is enough or whether to move to Azure SQL.
- Test one RSVP after deployment and confirm the response is saved.
