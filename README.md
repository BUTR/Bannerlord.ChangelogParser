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
bannerlord_changelog_parser latestversion -f "$PWD/changelog.txt"

bannerlord_changelog_parser description -f "$PWD/changelog.txt"
bannerlord_changelog_parser description -v "3.1.0" -f "$PWD/changelog.txt"

bannerlord_changelog_parser fulldescription -f "$PWD/changelog.txt"
bannerlord_changelog_parser fulldescription -v "3.1.0" -f "$PWD/changelog.txt"

bannerlord_changelog_parser gameversion -f "$PWD/changelog.txt"
bannerlord_changelog_parser gameversion -v "3.1.0" -f "$PWD/changelog.txt"
```
  
Check [this](https://github.com/Aragas/Bannerlord.MBOptionScreen/blob/700eb1dd1aec531cfaac242f8273c7a5d58ca6e0/.github/workflows/test-and-publish.yml#L255-L276) for a workflow example.

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

### fulldescription command output
```txt
For e1.4.0/e1.4.1/e1.4.2
* Fixed Group translation bug
* Fixed Fluent API settings loading
* Fixed XML settings reading
```
