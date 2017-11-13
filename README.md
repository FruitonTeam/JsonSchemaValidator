# CLI wrapper for [Newtonsoft.Json.Schema](https://github.com/JamesNK/Newtonsoft.Json.Schema)

## Getting started

Install [dotnet core 2](https://www.microsoft.com/net/learn/get-started/windows)

### Using the app

To use the app just grab a zip from Releases folder and run `dotnet JsonSchemaValidator.dll [pathToSchemaFile] [pathToJsonToValidate]`

Validation errors will be output to the command line.

You can test the app by running it on `FruitDb.schema.json` and `FruitDb.json` from this repo.

### Deployment to Bamboo

1. Open in Visual studio 2017 (or vim?)
1. Change what you want
1. Publish the app (from VS or run `dotnet publish -c Release`)
1. Copy all files in the publish folder to `/var/lib/json-validator`
1. Run `chmod o+x /var/lib/json-validator/*.dll`
