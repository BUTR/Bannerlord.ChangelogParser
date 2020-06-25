# Bannerlord.ChangelogParser
Simple tool that parses a custom changelog format inspired by Factorio.

### Requirements
Requires BUTR GitHub Package Registry to be added.
```shell
nuget sources add -name "BUTR" -Source https://nuget.pkg.github.com/BUTR/index.json -Username YOURGITHUBUSERNAME -Password YOURGITHUBTOKEN
```

### Installation
```shell
dotnet tool install -g Bannerlord.ChangelogParser
```

### Example
When installed as a global tool:
```shell
bannerlord_changelog_parser getversion -f "$PWD/changelog.txt")
bannerlord_changelog_parser getdescription -f "$PWD/changelog.txt"
bannerlord_changelog_parser getdescription -v "3.1.0" -f "$PWD/changelog.txt"
```
  
Check [this](https://github.com/Aragas/Bannerlord.MBOptionScreen/blob/ff921182721919055cb74d19d12ddb6eda74d679/.github/workflows/test-and-publish.yml#L255-L276) for a workflow example.

### Changelog Format
```txt
---------------------------------------------------------------------------------------------------
Version: 3.1.10
Game Versions: e1.4.0,e1.4.1,e1.4.2
* Fixed GroupToggle in Fluent API
* Better Prefab loading
* Fixed Ctrl+V in Edit Box
* Fixed 'random' crash in Edit Box
---------------------------------------------------------------------------------------------------
Version: 3.1.9
Game Versions: e1.4.0,e1.4.1,e1.4.2
* Fixed Group translation bug
* Fixed Fluent API settings loading
* Fixed XML settings reading
---------------------------------------------------------------------------------------------------
```

### Description command output
```txt
For e1.4.0/e1.4.1/e1.4.2
* Fixed Group translation bug
* Fixed Fluent API settings loading
* Fixed XML settings reading
```
