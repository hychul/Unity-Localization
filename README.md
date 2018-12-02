# Localization
![](https://img.shields.io/badge/Unity-2018.3-blue.svg?style=flat-square) ![](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)

Unity plugin to show proper language string for locale

# Getting Start

Just add this plugin to your unity project. That all :)

# Usage

Create 'string_[locale].xml' file in 'Assets/Resources/Localization' folder.
``` xml
<?xml version="1.0" encoding="UTF-8" ?>

<resource>
    <string name="hello">Hello Locale!</string>
    <string name="ok">OK</string>
    <string name="cancel">Cancel</string>
</resource>

```

To load the xml file, use `LocalizationSystem` class.
``` cs
...
LocalizationSystem.Load("en");
...
```
