# csv2sql

Helpful tool when you i.e want to create a bunch of SQL from lines in a CSV file.

## Pre-requirements

- .Net 6

## Build

Run the following to build it as self-contained (without need to install .Net on the target machine)

### Windows

```
dotnet publish -c Release -r win-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```

### Linux

```
dotnet publish -c Release -r linux-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```

### OSX

```
dotnet publish -c Release -r osx.12.6-x64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```
