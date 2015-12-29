# ZArc

This project is to provide a user interface to the System.IO.Compression.ZipArchive and System.IO.Compression.ZipFile classes

### Technical  notes:

  * Microsoft's .Net Framework 4.5 and later implement the System.IO.Compression.ZipArchive and System.IO.Compression.ZipFile classes. These objects appear to implement [PKWare APPNOTE 6.3.0](https://www.pkware.com/support/zip-app-note) or later. PKWare APPNOTE describes a public domain standard for the .zip file  format that is broadly accepted and implemented in the industry. The standard, and this particular Microsoft implementation, support what is described as the Zip64 extension, which supports zip archives and files in the order of 17 exabyte (10 ** 22 or 2 ** 64) in size   
  * There is no obvious user interface for these objects 
