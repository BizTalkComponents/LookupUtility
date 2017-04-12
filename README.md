# LookupUtility
> Generic utility for translating values in a BizTalk transform.

As with any other type of programming, hard coding configuration data in a BizTalk solution should be avoided. This helper library aims to enable integrations to read configuration data in a consistent manner.

The lookup utlity is not dependant on any specific configuration store but makes certain assumptions about the structure of how the configuration data can be accessed.

Configuration data should be stored in table format as key/value pairs and there should be a column called Key where a unique identifier used for the lookup is stored, and a column called Value with value to be looked up. Any other column should be ignored by the utility. 

The utility can either be used in a BizTalk map or a BizTalk pipeline component.


| Key | Value | Description                |
|-----|-------|----------------------------|
| 123 | ABC   | Human readable description |
| 456 | DEF   | Human readable description |
| 789 | GHI   | Human readable description |

These configuration tables are referred to as lists in the utility. A list can be stored as a SharePoint list or a SQL Server table for example.

## Using the utility

### Pipeline component
Install the [Nuget package](https://github.com/BizTalkComponents/LookupUtility/releases) in your pipeline component project.

The following code gets the configuration value from the MyList list with the key ConfigKey.

```cs
var lookupService = new LookupUtilityService();
var val = lookupService.GetValue("MyList", "ConfigKey");
```

### BizTalk map
Make sure that the utility is [installed](#Installation) in the GAC.
The easiest way to to this is to download and install the [MSI](https://github.com/BizTalkComponents/LookupUtility/releases) 

You also need to [reference](https://blog.sandro-pereira.com/2012/07/29/biztalk-mapper-patterns-calling-an-external-assembly-from-custom-xslt-in-biztalk-server-2010/) the utility dll from your BizTalk map.

The extension object xml should look like this.
```xml
<ExtensionObjects>
  <ExtensionObject Namespace="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    AssemblyName="BizTalkComponents.Utilities.LookupUtility, Version=1.0.0.0, Culture=neutral, 
        PublicKeyToken=2a501ae5622b3926" 
    ClassName="BizTalkComponents.Utilities.LookupUtility.LookupUtilityService" />
</ExtensionObjects>
````
In Xslt you can then call the utility using the GetValue method with a parameter for list and key.

```xml
<xsl:variable name="result" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    select="ScriptNS0:GetValue(list, key))" />
```

The utility will return null for any non existing key and throw an exception if the specified list does not exist.

If you want to throw an exception if the specified key does not exist you can use the following call.

```xml
<xsl:variable name="result" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    select="ScriptNS0:GetValue(list, key, true))" />
```

## Installation

The utility needs to be installed in the GAC on all BizTalk Servers where the utility is used.
The easiest way to to this is to download and install the [MSI](https://github.com/BizTalkComponents/LookupUtility/releases) 

Connection information to the storage should typically be placed in BizTalks configuration file.
The Sharepoint repository looks for the config key SharePointSite


## Sharepoint
LookupUtility is shipped with a lookup repository for reading configuration data from Sharepoint lists.