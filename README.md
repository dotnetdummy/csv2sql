# csv2sql

Helpful tool when you i.e want to create a bunch of SQL from lines in a CSV file.

## Pre-requirements

- .Net 6

## Known bugs

- When parsing lines with `","` it will not return the correct number of columns and data.

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
dotnet publish -c Release -r osx-arm64 /p:PublishSingleFile=true /p:PublishTrimmed=true
```
