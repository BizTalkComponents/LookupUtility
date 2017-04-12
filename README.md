# LookupUtility
> Generic utility for translating values in a BizTalk transform.

As with any other type of programming, hard coding configuration data in a BizTalk solution should be avoided. This helper library aims to enable integrations to read configuration data in a consistent manner.

The lookup utlity is not dependant on any specific configuration store but makes certain assumptions about the structure of how the configuration data can be accessed.
Configuration data should be stored in table format as key/value pairs and there should be a column called Key where a unique identifier used for the lookup is stored, and a column called Value with value to be looked up. Any other column should be ignored by the utility. The utility can either be used in a BizTalk map or a BizTalk pipeline component.


| Key | Value | Description                |
|-----|-------|----------------------------|
| 123 | ABC   | Human readable description |
| 456 | DEF   | Human readable description |
| 789 | GHI   | Human readable description |

These configuration tables are referred to as lists in the utility. A list can be stored as a SharePoint list or a SQL Server table for example.



## Sharepoint
LookupUtility is shipped with a lookup repository for reading configuration data from Sharepoint lists.