# ZArc

This project is to provide a user interface to the System.IO.Compression.ZipArchive and System.IO.Compression.ZipFile classes

Currently only a CLI interface is provided 

### Current version:

The current version is v0.7.2, refer the VesionHistory.md file. Compiled code for .Net Framework 4.6 can be downloaded from [here](https://www.dropbox.com/sh/s2j1ee6sg3b200t/AABHFKU4i1ERwfVdzzwZya_5a?dl=0)  

### CLI syntax:

The command displays help text. Use the following syntax as an initial guide to implemented functions

```
ZArcCLI help syntax: 
  ZArcCLI -Help -<verb> 
 
    <verb> -DeleteEntry  Help on deleting an archive entry 
           -Help         Help 
           -List         Help on listing the files in an archive 
           -ListEntry    Help on listing a specific archive entry   
           -UnzipDir     Help on unzipping an archive into a directory
           -UnzipFile    Help on unzipping a specfic archive entry into 
                         a file  
           -ZipDir       Help on zipping a directory into an archive
           -ZipFile      Help on zipping a fiile into a specific archive
                         entry
```     

### CLI examples:

The following code provides examples of execution

```
rem List the original directory
DIR "C:\Software\ZarcTest\*.*" /s /a  

rem Zip a directory into an archive, without overwrite
ZArcCLI -zipdir -a C:\Software\ZarcTest.zip -s "C:\Software\ZarcTest" -cl Fastest -nib -noa -st

rem Zip a directory into the same archive with overwrite
ZArcCLI -zipdir -a C:\Software\ZarcTest.zip -s "C:\Software\ZarcTest" -cl Fastest -nib -oa -st

rem List archive entries
ZArcCLI -list -a C:\Software\ZarcTest.zip -nsh -st 

rem Add a file to an archive, allowing duplicates  
ZArcCLI -zipfile -a C:\Software\ZarcTest.zip -s "C:\Software\VP.20121127.log" -e "test\test1.txt" -st

rem Add the same file to an archive, not allowing duplicates
ZArcCLI -zipfile -a C:\Software\ZarcTest.zip -s "C:\Software\VP.20121127.log" -e "test\test1.txt" -nad -st

rem Add a second file to an archive 
ZArcCLI -zipfile -a C:\Software\ZarcTest.zip -s "C:\Software\VP.20121204.log" -e "test\test2.txt" -st

rem List the most recently added archive entry
ZArcCLI -listentry -a C:\Software\ZarcTest.zip -e "test\test2.txt" -nsh -st 

rem Delete the most recently added archive entry
ZArcCLI -deleteentry -a C:\Software\ZarcTest.zip -e "test\test2.txt" 

rem List archive entries
ZArcCLI -list -a C:\Software\ZarcTest.zip -nsh -st

rem List a previously added archive entry 
ZArcCLI -listentry -a C:\Software\ZarcTest.zip -e "test\test1.txt" -nsh -st 

rem Unzip an archive into a target directory, without overwrite
ZArcCLI -unzipdir -a C:\Software\ZarcTest.zip -t "C:\Software\_ZarcTest" -not -st 

rem Unzip an archive into a target directory, with overwrite
ZArcCLI -unzipdir -a C:\Software\ZarcTest.zip -t "C:\Software\_ZarcTest" -ot -st 

rem Unzip a file  
ZArcCLI -unzipfile -a C:\Software\ZarcTest.zip -e "test\test1.txt" -t "C:\Software\_ZarcTest\_test\test1.txt" -st 

rem List the target directory for comparison
DIR "C:\Software\_ZarcTest\*.*" /s /a   
```

### System requirements:

  + The code was tested under .Net Framework 4.6
  + The underlying classes were only introduced in .Net Framework 4.5
 
### Technical  notes:

  + Microsoft's .Net Framework 4.5 and later implement the System.IO.Compression.ZipArchive and System.IO.Compression.ZipFile classes. These classes appear to implement [PKWare APPNOTE 6.3.0](https://www.pkware.com/support/zip-app-note) or later. [PKWare APPNOTE](https://en.wikipedia.org/wiki/Zip_(file_format)#Standardization) describes a public domain standard for the .zip file  format that is broadly accepted and implemented in the industry. The standard, and this particular Microsoft implementation, support what is described as the Zip64 extension, which supports zip archives and files in the order of 17 exabyte (10\*\*22 or 2\*\*64) in size
  + The implication is that most *industry standard* zip programs will unzip archives created by these classes; these classes will only unzip archives created by some *industry standard* zip programs, and then only if zipped using specific parameters. Specifically the classes only support compression as provided by the [zlib](https://en.wikipedia.org/wiki/Zlib) library    
  + There does not appear to be a generic user interface using these classes 
