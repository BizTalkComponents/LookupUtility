# LookupUtility
> Generic utility for translating values in a BizTalk transform.

Utility to handle lookup of configuration values from BizTalk maps and BizTalk pipeline components.

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
Install the [Nuget package](https://www.nuget.org/packages/BizTalkComponents.Utilities.LookupUtility/) in your pipeline component project.

The configuration properties are accessed through the _LookupUtilityService_.
The  _LookupUtilityService_ should be initialized with a repository for the specific configuration store in used. For Sharepoint use:

```cs
var lookupService = new LookupUtilityService(new SharepointLookupRepository());
```

The following code gets the configuration value from the _MyList_ list with the key _ConfigKey_.

```cs
var val = lookupService.GetValue("MyList", "ConfigKey");
```

If the list _MyList_ does not exist a ArgumentException will be thrown. If _ConfigKey_ does not exist, val will be set to null.
If an exception should be thrown if the key does not exist that is possible through:

```cs
var val = lookupService.GetValue("MyList", "ConfigKey", true);
```

### BizTalk map
Make sure that the utility is [installed](#Installation) in the GAC.
The easiest way to to this is to download and install the [MSI](https://github.com/BizTalkComponents/LookupUtility/releases) 

You also need to [reference](https://blog.sandro-pereira.com/2012/07/29/biztalk-mapper-patterns-calling-an-external-assembly-from-custom-xslt-in-biztalk-server-2010/) the utility dll from your BizTalk map.

Since we are not able to initialize the LookupService with the constructor that takes the repository as a parameter we need to use an ApplicationService specific to our configuration store.

The extension object xml for Sharepoint would look like this.
```xml
<ExtensionObjects>
  <ExtensionObject Namespace="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    AssemblyName="BizTalkComponents.Utilities.LookupUtility, Version=1.0.0.0, Culture=neutral, 
        PublicKeyToken=2a501ae5622b3926" 
    ClassName="BizTalkComponents.Utilities.LookupUtility.Application.SharePointApplicationService" />
</ExtensionObjects>
````


In Xslt you can then call the utility using the GetValue method with a parameter for list and key.

```xsl
<xsl:variable name="result" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    select="ScriptNS0:GetValue('list', 'key'))" />
```

The utility will return null for any non existing key and throw an exception if the specified list does not exist.

If you want to throw an exception if the specified key does not exist you can use the following call.

```xsl
<xsl:variable name="result" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    select="ScriptNS0:GetValue('list', 'key', true))" />
```

## Default values
Besides returning null or throwing an exception when a requested key does not exist the utility can return default values.
A default value can be specified at the configuration store by creating a key called _default_ with the default value as value.
The utility will look for the default key if the specified key does not exists and if checking for default values are enabled in the map/pipeline component. This is accomplished by setting allowDefaults = true in the GetValue method.

```xsl
<xsl:variable name="result" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    select="ScriptNS0:GetValue('list', 'key', false, true))" />
```

Default values can also be defined directly in the map/pipline component by using an overloaded version of GetValue

```xsl
<xsl:variable name="result" xmlns:ScriptNS0="http://schemas.microsoft.com/BizTalk/2003/ScriptNS0" 
    select="ScriptNS0:GetValue('list', 'key', 'default value'))" />
```

## Installation

The utility needs to be installed in the GAC on all BizTalk Servers where the utility is used.
The easiest way to to this is to download and install the [MSI](https://github.com/BizTalkComponents/LookupUtility/releases) 

Connection information to the storage should typically be placed in BizTalks configuration file.
The Sharepoint repository looks for the config key _SharePointSite_
```xml
<appSettings>
    <add key="SharePointSite" value="http://server/listdirectory"/>
</appSettings>
```

## Sharepoint
LookupUtility is shipped with a lookup repository for reading configuration data from Sharepoint lists. It is important that the configuration data is stored in a list with a unique name and that it contains a field called key and a field called value. The values in the key field must be unique to the list.

## Composite keys
In some scenarios multiple keys are needed to make a unique row. The utility is agnostic of how keys are structured but the suggested way is to combine keys with a separator between them, i.e.

| Key | Value | Description                      |
|-----|-------|----------------------------------|
| Key1:Key2 | ABC   | Human readable description |
| Key1:Key3 | DEF   | Human readable description |