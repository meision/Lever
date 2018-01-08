# Lever
## Foundation

### Lever

## Extension
*Lever Visual Studio Extension ToolSuite* is a powerful visual studio extensions help developer improve velocity.  
Only support C# project currently.

**Singleton Class**  
Item Template. By clicking "", you could add a singleton class under "Code" category.
It create a singleton class with standard template.
1. Right click on project folder, and choose "Add -> New Item...".
1. 

**Languages Excel**  
Item Template. By clicking "Add -> New Item...", you could add a languages excel file under "General" category.  
Languages Excel is an approach which intend to replace the Resx file for internationalization.  
Typically, you use separated "resx" files(Strings.resx, Strings.zh-Hans.resx, Strings.en-US.resx) to provide languages strings.
However, it is difficult to "aligning" which means a key may miss in other resx file, as well as the interpreters could not easy to guess the meaning from string key.  
From languages excel, you could edit the Strings.xlsx file and "Run Custom Tool" (or "Generate Languages" if excel file is open.) to generate static string file.

**TestData Excel**  
Item Template. By clicking "Add -> New Item...", you could add a test data excel file under "Data" category.  
TestData Excel used for provide test data for XUnit.Net ```[Theory]``` test method.  
Open "TestData.xlsx" file create a worksheet and set the name match with class name, and then add test data line by line.
