namespace PracTec.ZArc
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics;
  using System.IO;
  using System.IO.Compression;
  using System.Reflection;

  public static class Program
  {
    public enum Verb
    {
      DeleteEntry,
      Help,
      List,
      ListEntry,
      UnzipDir,
      UnzipFile,
      ZipDir,
      ZipFile
    };

    public enum DeleteEntryKey
    {
      help,
      archive,
      entry,
      stats, noStats
    };

    public enum HelpKey
    {
      Help,
      DeleteEntry,
      List,
      ListEntry,
      UnzipDir,
      UnzipFile,
      ZipDir,
      ZipFile
    };

    public enum ListKey
    {
      help,
      archive,
      @short, noShort,
      stats, noStats
    };

    public enum ListEntryKey
    {
      help,
      archive,
      entry,
      @short, noShort,
      stats, noStats
    };

    public enum UnzipDirKey
    {
      help,
      archive,
      target,
      overwriteTarget, noOverwriteTarget,
      stats, noStats
    };

    public enum UnzipFileKey
    {
      help,
      archive,
      entry,
      target,
      overwriteTarget, noOverwriteTarget,
      stats, noStats
    };

    public enum ZipDirKey
    {
      help,
      archive,
      source,
      compressionLevel,
      includeBase, noIncludeBase,
      overwriteArchive, noOverwriteArchive,
      stats, noStats
    };

    public enum ZipFileKey
    {
      help,
      archive,
      entry,
      source,
      compressionLevel,
      allowDuplicate, noAllowDuplicate,
      stats, noStats
    };

    public static Dictionary<string, Verb> VerbList = new Dictionary<string, Verb> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.VerbDeleteEntry, Verb.DeleteEntry },
        { Properties.Resources.VerbHelp, Verb.Help }, { Properties.Resources.VerbHelp_, Verb.Help },
        { Properties.Resources.VerbList, Verb.List },
        { Properties.Resources.VerbListEntry, Verb.ListEntry },
        { Properties.Resources.VerbUnzipDir, Verb.UnzipDir },
        { Properties.Resources.VerbUnzipFile, Verb.UnzipFile },
        { Properties.Resources.VerbZipDir, Verb.ZipDir },
        { Properties.Resources.VerbZipFile, Verb.ZipFile } };

    public static Dictionary<string, DeleteEntryKey> DeleteEntryList = new Dictionary<string, DeleteEntryKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, DeleteEntryKey.help }, { Properties.Resources.KeyHelp_, DeleteEntryKey.help },
        { Properties.Resources.KeyArchive, DeleteEntryKey.archive }, { Properties.Resources.KeyArchive_, DeleteEntryKey.archive },
        { Properties.Resources.KeyEntry, DeleteEntryKey.entry }, { Properties.Resources.KeyEntry_, DeleteEntryKey.entry },
        { Properties.Resources.KeyStats, DeleteEntryKey.stats }, { Properties.Resources.KeyStats_, DeleteEntryKey.stats }, { Properties.Resources.KeyNoStats, DeleteEntryKey.noStats }, { Properties.Resources.KeyNoStats_, DeleteEntryKey.noStats } };

    public static Dictionary<string, HelpKey> HelpList = new Dictionary<string, HelpKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.VerbHelp, HelpKey.Help }, { Properties.Resources.VerbHelp_, HelpKey.Help },
        { Properties.Resources.VerbList, HelpKey.List },
        { Properties.Resources.VerbUnzipDir, HelpKey.UnzipDir },
        { Properties.Resources.VerbZipDir, HelpKey.ZipDir } };

    public static Dictionary<string, ListKey> ListList = new Dictionary<string, ListKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, ListKey.help }, { Properties.Resources.KeyHelp_, ListKey.help },
        { Properties.Resources.KeyArchive, ListKey.archive }, { Properties.Resources.KeyArchive_, ListKey.archive },
        { Properties.Resources.KeyShort, ListKey.@short }, { Properties.Resources.KeyShort_, ListKey.@short }, { Properties.Resources.KeyNoShort, ListKey.noShort }, { Properties.Resources.KeyNoShort_, ListKey.noShort },
        { Properties.Resources.KeyStats, ListKey.stats }, { Properties.Resources.KeyStats_, ListKey.stats }, { Properties.Resources.KeyNoStats, ListKey.noStats }, { Properties.Resources.KeyNoStats_, ListKey.noStats } };

    public static Dictionary<string, ListEntryKey> ListEntryList = new Dictionary<string, ListEntryKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, ListEntryKey.help }, { Properties.Resources.KeyHelp_, ListEntryKey.help },
        { Properties.Resources.KeyArchive, ListEntryKey.archive }, { Properties.Resources.KeyArchive_, ListEntryKey.archive },
        { Properties.Resources.KeyEntry, ListEntryKey.entry }, { Properties.Resources.KeyEntry_, ListEntryKey.entry },
        { Properties.Resources.KeyShort, ListEntryKey.@short }, { Properties.Resources.KeyShort_, ListEntryKey.@short }, { Properties.Resources.KeyNoShort, ListEntryKey.noShort }, { Properties.Resources.KeyNoShort_, ListEntryKey.noShort },
        { Properties.Resources.KeyStats, ListEntryKey.stats }, { Properties.Resources.KeyStats_, ListEntryKey.stats }, { Properties.Resources.KeyNoStats, ListEntryKey.noStats }, { Properties.Resources.KeyNoStats_, ListEntryKey.noStats } };

    public static Dictionary<string, UnzipDirKey> UnzipDirList = new Dictionary<string, UnzipDirKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, UnzipDirKey.help }, { Properties.Resources.KeyHelp_, UnzipDirKey.help },
        { Properties.Resources.KeyArchive, UnzipDirKey.archive }, { Properties.Resources.KeyArchive_, UnzipDirKey.archive },
        { Properties.Resources.KeyTarget, UnzipDirKey.target }, { Properties.Resources.KeyTarget_, UnzipDirKey.target },
        { Properties.Resources.KeyOverwriteTarget, UnzipDirKey.overwriteTarget }, { Properties.Resources.KeyOverwriteTarget_, UnzipDirKey.overwriteTarget }, { Properties.Resources.KeyNoOverwriteTarget, UnzipDirKey.noOverwriteTarget }, { Properties.Resources.KeyNoOverwriteTarget_, UnzipDirKey.noOverwriteTarget },
        { Properties.Resources.KeyStats, UnzipDirKey.stats }, { Properties.Resources.KeyStats_, UnzipDirKey.stats }, { Properties.Resources.KeyNoStats, UnzipDirKey.noStats }, { Properties.Resources.KeyNoStats_, UnzipDirKey.noStats } };

    public static Dictionary<string, UnzipFileKey> UnzipFileList = new Dictionary<string, UnzipFileKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, UnzipFileKey.help }, { Properties.Resources.KeyHelp_, UnzipFileKey.help },
        { Properties.Resources.KeyArchive, UnzipFileKey.archive }, { Properties.Resources.KeyArchive_, UnzipFileKey.archive },
        { Properties.Resources.KeyEntry, UnzipFileKey.entry }, { Properties.Resources.KeyEntry_, UnzipFileKey.entry },
        { Properties.Resources.KeyTarget, UnzipFileKey.target }, { Properties.Resources.KeyTarget_, UnzipFileKey.target },
        { Properties.Resources.KeyOverwriteTarget, UnzipFileKey.overwriteTarget }, { Properties.Resources.KeyOverwriteTarget_, UnzipFileKey.overwriteTarget }, { Properties.Resources.KeyNoOverwriteTarget, UnzipFileKey.noOverwriteTarget }, { Properties.Resources.KeyNoOverwriteTarget_, UnzipFileKey.noOverwriteTarget },
        { Properties.Resources.KeyStats, UnzipFileKey.stats }, { Properties.Resources.KeyStats_, UnzipFileKey.stats }, { Properties.Resources.KeyNoStats, UnzipFileKey.noStats }, { Properties.Resources.KeyNoStats_, UnzipFileKey.noStats } };

    public static Dictionary<string, ZipDirKey> ZipDirList = new Dictionary<string, ZipDirKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, ZipDirKey.help }, { Properties.Resources.KeyHelp_, ZipDirKey.help },
        { Properties.Resources.KeyArchive, ZipDirKey.archive }, { Properties.Resources.KeyArchive_, ZipDirKey.archive },
        { Properties.Resources.KeySource, ZipDirKey.source }, { Properties.Resources.KeySource_, ZipDirKey.source },
        { Properties.Resources.KeyCompressionLevel, ZipDirKey.compressionLevel }, { Properties.Resources.KeyCompressionLevel_, ZipDirKey.compressionLevel },
        { Properties.Resources.KeyIncludeBase, ZipDirKey.includeBase }, { Properties.Resources.KeyIncludeBase_, ZipDirKey.includeBase }, { Properties.Resources.KeyNoIncludeBase, ZipDirKey.noIncludeBase }, { Properties.Resources.KeyNoIncludeBase_, ZipDirKey.noIncludeBase },
        { Properties.Resources.KeyOverwriteArchive, ZipDirKey.overwriteArchive }, { Properties.Resources.KeyOverwriteArchive_, ZipDirKey.overwriteArchive }, { Properties.Resources.KeyNoOverwriteArchive, ZipDirKey.noOverwriteArchive }, { Properties.Resources.KeyNoOverwriteArchive_, ZipDirKey.noOverwriteArchive },
        { Properties.Resources.KeyStats, ZipDirKey.stats }, { Properties.Resources.KeyStats_, ZipDirKey.stats }, { Properties.Resources.KeyNoStats, ZipDirKey.noStats }, { Properties.Resources.KeyNoStats_, ZipDirKey.noStats } };

    public static Dictionary<string, ZipFileKey> ZipFileList = new Dictionary<string, ZipFileKey> ( StringComparer.CurrentCultureIgnoreCase ) {
        { Properties.Resources.KeyHelp, ZipFileKey.help }, { Properties.Resources.KeyHelp_, ZipFileKey.help },
        { Properties.Resources.KeyArchive, ZipFileKey.archive }, { Properties.Resources.KeyArchive_, ZipFileKey.archive },
        { Properties.Resources.KeyEntry, ZipFileKey.entry }, { Properties.Resources.KeyEntry_, ZipFileKey.entry },
        { Properties.Resources.KeySource, ZipFileKey.source }, { Properties.Resources.KeySource_, ZipFileKey.source },
        { Properties.Resources.KeyCompressionLevel, ZipFileKey.compressionLevel }, { Properties.Resources.KeyCompressionLevel_, ZipFileKey.compressionLevel },
        { Properties.Resources.KeyAllowDuplicate, ZipFileKey.allowDuplicate }, { Properties.Resources.KeyAllowDuplicate_, ZipFileKey.allowDuplicate }, { Properties.Resources.KeyNoAllowDuplicate, ZipFileKey.noAllowDuplicate }, { Properties.Resources.KeyNoAllowDuplicate_, ZipFileKey.noAllowDuplicate },
        { Properties.Resources.KeyStats, ZipFileKey.stats }, { Properties.Resources.KeyStats_, ZipFileKey.stats }, { Properties.Resources.KeyNoStats, ZipFileKey.noStats }, { Properties.Resources.KeyNoStats_, ZipFileKey.noStats } };

    private static int rc = int.MinValue;

    private static int Main ( string [] args )
    {
      ConsoleColor windowBackground = Console.BackgroundColor;
      ConsoleColor windowForeground = Console.ForegroundColor;
      ConsoleColor windowError = Console.ForegroundColor;
      string windowTitle = Console.Title;
      int bufferHeight = Console.BufferHeight;

      try
      {
        Console.Title = string.Format ( "{0} v{1}", Assembly.GetExecutingAssembly ( ).GetName ( ).Name, Assembly.GetExecutingAssembly ( ).GetName ( ).Version );
        if ( Properties.Settings.Default.OverrideColor )
        {
          Console.BackgroundColor = Properties.Settings.Default.ColorBackground;
          Console.ForegroundColor = Properties.Settings.Default.ColorForeground;
        }
        if ( args.Length >= 1 )
        {
          if ( VerbList.ContainsKey ( args [ 0 ] ) )
          {
            switch ( VerbList [ args [ 0 ] ] )
            {
              case Verb.DeleteEntry:
                {
                  rc = _DeleteEntry ( args );
                }
                break;

              case Verb.Help:
                {
                  rc = _Help ( args );
                }
                break;

              case Verb.List:
                {
                  rc = _List ( args );
                }
                break;

              case Verb.ListEntry:
                {
                  rc = _ListEntry ( args );
                }
                break;

              case Verb.UnzipDir:
                {
                  rc = _UnzipDir ( args );
                }
                break;

              case Verb.UnzipFile:
                {
                  rc = _UnzipFile ( args );
                }
                break;

              case Verb.ZipDir:
                {
                  rc = _ZipDir ( args );
                }
                break;

              case Verb.ZipFile:
                {
                  rc = _ZipFile ( args );
                }
                break;
            }
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidVerb, args [ 0 ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpVerb );
          }
        }
        else
        {
          rc = -1;
          ConsoleColor foregroundColor = Console.ForegroundColor;
          try
          {
            if ( Properties.Settings.Default.OverrideColor )
              Console.ForegroundColor = Properties.Settings.Default.ColorError;
            Console.Error.WriteLine ( Properties.Resources.ErrorInvalidVerb, "" );
          }
          finally
          {
            Console.ForegroundColor = foregroundColor;
          }
          Console.Error.WriteLine ( Properties.Resources.HelpVerb );
        }
      }
      catch ( ArgumentNullException argumentNullexception )
      {
        rc = argumentNullexception.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionArgumentNull, argumentNullexception );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( ArgumentException argumentException )
      {
        rc = argumentException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionArgument, argumentException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( FileNotFoundException fileNotFoundException )
      {
        rc = fileNotFoundException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionFileNotFound, fileNotFoundException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( DirectoryNotFoundException directoryNotFoundException )
      {
        rc = directoryNotFoundException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionDirectoryNotFound, directoryNotFoundException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( InvalidDataException invalidDataException )
      {
        rc = invalidDataException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionInvalidData, invalidDataException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( PathTooLongException pathTooLongException )
      {
        rc = pathTooLongException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionPathTooLong, pathTooLongException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( IOException ioException )
      {
        rc = ioException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionIO, ioException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( UnauthorizedAccessException unauthorizedAccessException )
      {
        rc = unauthorizedAccessException.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionUnauthorizedAccess, unauthorizedAccessException );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      catch ( Exception exception )
      {
        rc = exception.HResult;
        ConsoleColor foregroundColor = Console.ForegroundColor;
        try
        {
          if ( Properties.Settings.Default.OverrideColor )
            Console.ForegroundColor = Properties.Settings.Default.ColorError;
          Console.Error.WriteLine ( Properties.Resources.ExceptionGeneral, exception );
        }
        finally
        {
          Console.ForegroundColor = foregroundColor;
        }
      }
      finally
      {
        Console.BackgroundColor = windowBackground;
        Console.ForegroundColor = windowForeground;
        Console.Title = windowTitle;
      }
      return rc;
    }

    private static int _DeleteEntry ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      string entryPath = string.Empty;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          DeleteEntryKey value = 0;
          if ( DeleteEntryList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case DeleteEntryKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case DeleteEntryKey.entry:
                {
                  if ( ix < args.Length )
                  {
                    entryPath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case DeleteEntryKey.stats:
                {
                  stats = true;
                }
                break;

              case DeleteEntryKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpZipFile );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          Stopwatch elapsed = new Stopwatch ( );
          long tc = 0L;
          float tl = 0.0F;
          float tcl = 0.0F;
          if ( File.Exists ( archivePath ) )
          {
            using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None ) )
            {
              using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Update ) )
              {
                ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry ( entryPath );
                if ( zipArchiveEntry != null )
                {
                  elapsed.Start ( );
                  zipArchiveEntry.Delete ( );
                  rc = 0;
                  elapsed.Stop ( );

                  tc++;
                  tl += (float)zipArchiveEntry.Length;
                  tcl += (float)zipArchiveEntry.CompressedLength;

                  if ( stats )
                  {
                    string c = tc.ToString ( "N0" );
                    string l = "***.** ??";
                    if ( tl > 999999999 )
                    {
                      l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                    }
                    else
                    {
                      if ( tl > 999999 )
                      {
                        l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                      }
                      else
                      {
                        l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                      }
                    }
                    string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
                    string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
                    Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
                  }
                }
                else
                {
                  rc = 1;
                  ConsoleColor foregroundColor = Console.ForegroundColor;
                  try
                  {
                    if ( Properties.Settings.Default.OverrideColor )
                      Console.ForegroundColor = Properties.Settings.Default.ColorError;
                    Console.Error.WriteLine ( Properties.Resources.ErrorEntryNotFound, entryPath );
                  }
                  finally
                  {
                    Console.ForegroundColor = foregroundColor;
                  }
                }

                zipArchive.Dispose ( );
              }
              stream.Dispose ( );
            }
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveNotFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        Console.Error.WriteLine ( Properties.Resources.HelpUnzipDir );
        rc = -1;
      }
      return rc;
    }

    private static int _Help ( string [] args )
    {
      int rc = int.MinValue;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          HelpKey value = 0;
          if ( HelpList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case HelpKey.DeleteEntry:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpDeleteEntry );
                }
                break;

              case HelpKey.Help:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpHelp );
                }
                break;

              case HelpKey.List:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpList );
                }
                break;

              case HelpKey.ListEntry:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpListEntry );
                }
                break;

              case HelpKey.UnzipDir:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpUnzipDir );
                }
                break;

              case HelpKey.UnzipFile:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpUnzipFile );
                }
                break;

              case HelpKey.ZipDir:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpZipDir );
                }
                break;

              case HelpKey.ZipFile:
                {
                  rc = 0;
                  Console.Error.WriteLine ( Properties.Resources.HelpZipFile );
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpHelp );
            break;
          }
        }
        rc = 0;
      }
      else
      {
        rc = -1;
        Console.Error.WriteLine ( Properties.Resources.HelpVerb );
      }
      return rc;
    }

    private static int _List ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      bool @short = false;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          ListKey value = 0;
          if ( ListList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case ListKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ListKey.@short:
                {
                  @short = true;
                }
                break;

              case ListKey.noShort:
                {
                  @short = false;
                }
                break;

              case ListKey.stats:
                {
                  stats = true;
                }
                break;

              case ListKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpList );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          if ( File.Exists ( archivePath ) )
          {
            Stopwatch elapsed = new Stopwatch ( );
            elapsed.Start ( );
            long tc = 0L;
            float tl = 0.0F;
            float tcl = 0.0F;
            using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            {
              using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Read ) )
              {
                foreach ( ZipArchiveEntry zipArchiveEntry in zipArchive.Entries )
                {
                  Console.Out.WriteLine ( zipArchiveEntry.FullName );
                  tc++;
                  tl += (float)zipArchiveEntry.Length;
                  tcl += (float)zipArchiveEntry.CompressedLength;
                  if ( !@short )
                  {
                    string l = "***.** ??";
                    if ( zipArchiveEntry.Length > 999999999 )
                    {
                      l = ( (float)zipArchiveEntry.Length / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                    }
                    else
                    {
                      if ( zipArchiveEntry.Length > 999999 )
                      {
                        l = ( (float)zipArchiveEntry.Length / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                      }
                      else
                      {
                        l = ( (float)zipArchiveEntry.Length ).ToString ( "N0" ) + "";
                      }
                    }
                    l = l.PadLeft ( 10 );
                    string p = ( ( zipArchiveEntry.Length != 0 ) ? ( ( (float)zipArchiveEntry.Length - (float)zipArchiveEntry.CompressedLength ) / (float)zipArchiveEntry.Length ) : 0.0F ).ToString ( "P" ).PadLeft ( 6 );
                    string d = zipArchiveEntry.LastWriteTime.ToString ( "d" ).PadLeft ( 10 );
                    Console.Out.WriteLine ( "{0} {1} {2}", l, p, d );
                  }
                }
                zipArchive.Dispose ( );
              }
              stream.Dispose ( );
              rc = 0;
            }
            elapsed.Stop ( );
            if ( stats )
            {
              string c = tc.ToString ( "N0" );
              string l = "***.** ??";
              if ( tl > 999999999 )
              {
                l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
              }
              else
              {
                if ( tl > 999999 )
                {
                  l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                }
                else
                {
                  l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                }
              }
              string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
              string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
              Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
            }
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveNotFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        rc = -1;
        Console.Error.WriteLine ( Properties.Resources.HelpList );
      }
      return rc;
    }

    private static int _ListEntry ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      string entryPath = string.Empty;
      bool @short = false;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          ListEntryKey value = 0;
          if ( ListEntryList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case ListEntryKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ListEntryKey.entry:
                {
                  if ( ix < args.Length )
                  {
                    entryPath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ListEntryKey.@short:
                {
                  @short = true;
                }
                break;

              case ListEntryKey.noShort:
                {
                  @short = false;
                }
                break;

              case ListEntryKey.stats:
                {
                  stats = true;
                }
                break;

              case ListEntryKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpListEntry );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          if ( File.Exists ( archivePath ) )
          {
            Stopwatch elapsed = new Stopwatch ( );
            elapsed.Start ( );
            long tc = 0L;
            float tl = 0.0F;
            float tcl = 0.0F;
            using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
            {
              using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Read ) )
              {
                ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry ( entryPath );
                if ( zipArchiveEntry != null )
                {
                  rc = 0;
                  Console.Out.WriteLine ( zipArchiveEntry.FullName );
                  tc++;
                  tl += (float)zipArchiveEntry.Length;
                  tcl += (float)zipArchiveEntry.CompressedLength;
                  if ( !@short )
                  {
                    string l = "***.** ??";
                    if ( zipArchiveEntry.Length > 999999999 )
                    {
                      l = ( (float)zipArchiveEntry.Length / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                    }
                    else
                    {
                      if ( zipArchiveEntry.Length > 999999 )
                      {
                        l = ( (float)zipArchiveEntry.Length / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                      }
                      else
                      {
                        l = ( (float)zipArchiveEntry.Length ).ToString ( "N0" ) + "";
                      }
                    }
                    l = l.PadLeft ( 10 );
                    string p = ( ( zipArchiveEntry.Length != 0 ) ? ( ( (float)zipArchiveEntry.Length - (float)zipArchiveEntry.CompressedLength ) / (float)zipArchiveEntry.Length ) : 0.0F ).ToString ( "P" ).PadLeft ( 6 );
                    string d = zipArchiveEntry.LastWriteTime.ToString ( "d" ).PadLeft ( 10 );
                    Console.Out.WriteLine ( "{0} {1} {2}", l, p, d );
                  }
                }
                else
                {
                  rc = 1;
                  ConsoleColor foregroundColor = Console.ForegroundColor;
                  try
                  {
                    if ( Properties.Settings.Default.OverrideColor )
                      Console.ForegroundColor = Properties.Settings.Default.ColorError;
                    Console.Error.WriteLine ( Properties.Resources.ErrorEntryNotFound, entryPath );
                  }
                  finally
                  {
                    Console.ForegroundColor = foregroundColor;
                  }
                }
                zipArchive.Dispose ( );
              }
              stream.Dispose ( );
            }
            elapsed.Stop ( );
            if ( stats )
            {
              string c = tc.ToString ( "N0" );
              string l = "***.** ??";
              if ( tl > 999999999 )
              {
                l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
              }
              else
              {
                if ( tl > 999999 )
                {
                  l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                }
                else
                {
                  l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                }
              }
              string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
              string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
              Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
            }
          }
          else
          {
            rc = 1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveNotFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        rc = -1;
        Console.Error.WriteLine ( Properties.Resources.HelpListEntry );
      }
      return rc;
    }

    private static int _UnzipDir ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      string targetPath = string.Empty;
      bool overwriteTarget = false;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          UnzipDirKey value = 0;
          if ( UnzipDirList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case UnzipDirKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case UnzipDirKey.target:
                {
                  if ( ix < args.Length )
                  {
                    targetPath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case UnzipDirKey.overwriteTarget:
                {
                  overwriteTarget = true;
                }
                break;

              case UnzipDirKey.noOverwriteTarget:
                {
                  overwriteTarget = false;
                }
                break;

              case UnzipDirKey.stats:
                {
                  stats = true;
                }
                break;

              case UnzipDirKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpUnzipDir );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          if ( File.Exists ( archivePath ) )
          {
            if ( ( !( Directory.Exists ( targetPath ) ) ) || ( ( Directory.Exists ( targetPath ) ) && overwriteTarget ) )
            {
              if ( Directory.Exists ( targetPath ) )
              {
                Directory.Delete ( targetPath, true );
              }
              Stopwatch elapsed = new Stopwatch ( );
              elapsed.Start ( );
              ZipFile.ExtractToDirectory ( archivePath, targetPath );
              rc = 0;
              elapsed.Stop ( );
              long tc = 0L;
              float tl = 0.0F;
              float tcl = 0.0F;
              using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
              {
                using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Read ) )
                {
                  foreach ( ZipArchiveEntry zipArchiveEntry in zipArchive.Entries )
                  {
                    tc++;
                    tl += (float)zipArchiveEntry.Length;
                    tcl += (float)zipArchiveEntry.CompressedLength;
                  }
                  zipArchive.Dispose ( );
                }
                stream.Dispose ( );
              }
              if ( stats )
              {
                string c = tc.ToString ( "N0" );
                string l = "***.** ??";
                if ( tl > 999999999 )
                {
                  l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                }
                else
                {
                  if ( tl > 999999 )
                  {
                    l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                  }
                  else
                  {
                    l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                  }
                }
                string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
                string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
                Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
              }
            }
            else
            {
              rc = 1;
              ConsoleColor foregroundColor = Console.ForegroundColor;
              try
              {
                if ( Properties.Settings.Default.OverrideColor )
                  Console.ForegroundColor = Properties.Settings.Default.ColorError;
                Console.Error.WriteLine ( Properties.Resources.ErrorTargetFound, targetPath );
              }
              finally
              {
                Console.ForegroundColor = foregroundColor;
              }
            }
          }
          else
          {
            rc = 1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveNotFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        rc = -1;
        Console.Error.WriteLine ( Properties.Resources.HelpUnzipDir );
      }
      return rc;
    }

    private static int _UnzipFile ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      string entryPath = string.Empty;
      string targetPath = string.Empty;
      bool overwriteTarget = false;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          UnzipFileKey value = 0;
          if ( UnzipFileList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case UnzipFileKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case UnzipFileKey.entry:
                {
                  if ( ix < args.Length )
                  {
                    entryPath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case UnzipFileKey.target:
                {
                  if ( ix < args.Length )
                  {
                    targetPath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case UnzipFileKey.overwriteTarget:
                {
                  overwriteTarget = true;
                }
                break;

              case UnzipFileKey.noOverwriteTarget:
                {
                  overwriteTarget = false;
                }
                break;

              case UnzipFileKey.stats:
                {
                  stats = true;
                }
                break;

              case UnzipFileKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpZipFile );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          Stopwatch elapsed = new Stopwatch ( );
          long tc = 0L;
          float tl = 0.0F;
          float tcl = 0.0F;
          if ( File.Exists ( archivePath ) )
          {
            using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None ) )
            {
              using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Update ) )
              {
                if ( !( string.IsNullOrWhiteSpace ( Path.GetDirectoryName ( targetPath ) ) ) )
                {
                  if ( !( Directory.Exists ( Path.GetFullPath ( Path.GetDirectoryName ( targetPath ) ) ) ) )
                  {
                    Directory.CreateDirectory ( Path.GetFullPath ( Path.GetDirectoryName ( targetPath ) ) );
                  }
                }
                if ( ( !( File.Exists ( targetPath ) ) ) || ( ( File.Exists ( targetPath ) ) && overwriteTarget ) )
                {
                  ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry ( entryPath );
                  if ( zipArchiveEntry != null )
                  {
                    tc++;
                    tl += (float)zipArchiveEntry.Length;
                    tcl += (float)zipArchiveEntry.CompressedLength;

                    elapsed.Start ( );
                    zipArchiveEntry.ExtractToFile ( targetPath, overwriteTarget );
                    rc = 0;
                    elapsed.Stop ( );

                    if ( stats )
                    {
                      string c = tc.ToString ( "N0" );
                      string l = "***.** ??";
                      if ( tl > 999999999 )
                      {
                        l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                      }
                      else
                      {
                        if ( tl > 999999 )
                        {
                          l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                        }
                        else
                        {
                          l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                        }
                      }
                      string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
                      string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
                      Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
                    }
                  }
                  else
                  {
                    rc = 1;
                    ConsoleColor foregroundColor = Console.ForegroundColor;
                    try
                    {
                      if ( Properties.Settings.Default.OverrideColor )
                        Console.ForegroundColor = Properties.Settings.Default.ColorError;
                      Console.Error.WriteLine ( Properties.Resources.ErrorEntryNotFound, entryPath );
                    }
                    finally
                    {
                      Console.ForegroundColor = foregroundColor;
                    }
                  }
                }
                else
                {
                  rc = -1;
                  ConsoleColor foregroundColor = Console.ForegroundColor;
                  try
                  {
                    if ( Properties.Settings.Default.OverrideColor )
                      Console.ForegroundColor = Properties.Settings.Default.ColorError;
                    Console.Error.WriteLine ( Properties.Resources.ErrorTargetFound, targetPath );
                  }
                  finally
                  {
                    Console.ForegroundColor = foregroundColor;
                  }
                }
                zipArchive.Dispose ( );
              }
              stream.Dispose ( );
            }
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveNotFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        Console.Error.WriteLine ( Properties.Resources.HelpUnzipDir );
        rc = -1;
      }
      return rc;
    }

    private static int _ZipDir ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      string sourcePath = string.Empty;
      CompressionLevel compressionLevel = CompressionLevel.Optimal;
      bool includeBase = true;
      bool overwriteArchive = false;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          ZipDirKey value = 0;
          if ( ZipDirList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case ZipDirKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ZipDirKey.source:
                {
                  if ( ix < args.Length )
                  {
                    sourcePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ZipDirKey.compressionLevel:
                {
                  if ( ix < args.Length )
                  {
                    if ( Enum.TryParse<CompressionLevel> ( args [ ix ], out compressionLevel ) )
                    {
                    }
                    else
                    {
                      rc = -1;
                      ConsoleColor foregroundColor = Console.ForegroundColor;
                      try
                      {
                        if ( Properties.Settings.Default.OverrideColor )
                          Console.ForegroundColor = Properties.Settings.Default.ColorError;
                        Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeywordValue, args [ iy ], args [ ix ] );
                      }
                      finally
                      {
                        Console.ForegroundColor = foregroundColor;
                      }
                      Console.Error.WriteLine ( Properties.Resources.HelpZipDir );
                    }
                    ix++;
                  }
                }
                break;

              case ZipDirKey.includeBase:
                {
                  includeBase = true;
                }
                break;

              case ZipDirKey.noIncludeBase:
                {
                  includeBase = false;
                }
                break;

              case ZipDirKey.overwriteArchive:
                {
                  overwriteArchive = true;
                }
                break;

              case ZipDirKey.noOverwriteArchive:
                {
                  overwriteArchive = false;
                }
                break;

              case ZipDirKey.stats:
                {
                  stats = true;
                }
                break;

              case ZipDirKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpZipDir );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          if ( ( !( File.Exists ( archivePath ) ) ) || ( ( File.Exists ( archivePath ) ) && overwriteArchive ) )
          {
            if ( Directory.Exists ( sourcePath ) )
            {
              if ( File.Exists ( archivePath ) )
              {
                File.Delete ( archivePath );
              }
              Stopwatch elapsed = new Stopwatch ( );
              elapsed.Start ( );
              ZipFile.CreateFromDirectory ( sourcePath, archivePath, compressionLevel, includeBase );
              rc = 0;
              elapsed.Stop ( );
              long tc = 0L;
              float tl = 0.0F;
              float tcl = 0.0F;
              using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.Read, FileShare.Read ) )
              {
                using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Read ) )
                {
                  foreach ( ZipArchiveEntry zipArchiveEntry in zipArchive.Entries )
                  {
                    tc++;
                    tl += (float)zipArchiveEntry.Length;
                    tcl += (float)zipArchiveEntry.CompressedLength;
                  }
                  zipArchive.Dispose ( );
                }
                stream.Dispose ( );
              }
              if ( stats )
              {
                string c = tc.ToString ( "N0" );
                string l = "***.** ??";
                if ( tl > 999999999 )
                {
                  l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                }
                else
                {
                  if ( tl > 999999 )
                  {
                    l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                  }
                  else
                  {
                    l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                  }
                }
                string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
                string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
                Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
              }
            }
            else
            {
              rc = 1;
              ConsoleColor foregroundColor = Console.ForegroundColor;
              try
              {
                if ( Properties.Settings.Default.OverrideColor )
                  Console.ForegroundColor = Properties.Settings.Default.ColorError;
                Console.Error.WriteLine ( Properties.Resources.ErrorSourceNotFound, sourcePath );
              }
              finally
              {
                Console.ForegroundColor = foregroundColor;
              }
            }
          }
          else
          {
            rc = 1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        rc = -1;
        Console.Error.WriteLine ( Properties.Resources.HelpZipDir );
      }
      return rc;
    }

    private static int _ZipFile ( string [] args )
    {
      int rc = int.MinValue;
      string archivePath = string.Empty;
      string entryPath = string.Empty;
      string sourcePath = string.Empty;
      CompressionLevel compressionLevel = CompressionLevel.Optimal;
      bool allowDuplicate = false;
      bool stats = true;

      if ( args.Length >= 2 )
      {
        int ix = 1;
        int iy = ix;
        while ( ix < args.Length )
        {
          iy = ix;
          ix++;
          ZipFileKey value = 0;
          if ( ZipFileList.TryGetValue ( args [ iy ], out value ) )
          {
            switch ( value )
            {
              case ZipFileKey.archive:
                {
                  if ( ix < args.Length )
                  {
                    archivePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ZipFileKey.entry:
                {
                  if ( ix < args.Length )
                  {
                    entryPath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ZipFileKey.source:
                {
                  if ( ix < args.Length )
                  {
                    sourcePath = args [ ix ];
                    ix++;
                  }
                }
                break;

              case ZipFileKey.compressionLevel:
                {
                  if ( ix < args.Length )
                  {
                    if ( Enum.TryParse<CompressionLevel> ( args [ ix ], out compressionLevel ) )
                    {
                    }
                    else
                    {
                      rc = -1;
                      ConsoleColor foregroundColor = Console.ForegroundColor;
                      try
                      {
                        if ( Properties.Settings.Default.OverrideColor )
                          Console.ForegroundColor = Properties.Settings.Default.ColorError;
                        Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeywordValue, args [ iy ], args [ ix ] );
                      }
                      finally
                      {
                        Console.ForegroundColor = foregroundColor;
                      }
                      Console.Error.WriteLine ( Properties.Resources.HelpZipDir );
                    }
                    ix++;
                  }
                }
                break;

              case ZipFileKey.allowDuplicate:
                {
                  allowDuplicate = true;
                }
                break;

              case ZipFileKey.noAllowDuplicate:
                {
                  allowDuplicate = false;
                }
                break;

              case ZipFileKey.stats:
                {
                  stats = true;
                }
                break;

              case ZipFileKey.noStats:
                {
                  stats = false;
                }
                break;
            }
            continue;
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorInvalidKeyword, args [ iy ] );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
            Console.Error.WriteLine ( Properties.Resources.HelpZipFile );
            break;
          }
        }
        if ( rc == int.MinValue )
        {
          Stopwatch elapsed = new Stopwatch ( );
          long tc = 0L;
          float tl = 0.0F;
          float tcl = 0.0F;
          if ( File.Exists ( archivePath ) )
          {
            using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None ) )
            {
              using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Update ) )
              {
                if ( File.Exists ( sourcePath ) )
                {
                  ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry ( entryPath );
                  if ( ( zipArchiveEntry == null ) || ( ( zipArchive != null ) && allowDuplicate ) )
                  {
                    elapsed.Start ( );
                    zipArchive.CreateEntryFromFile ( sourcePath, entryPath, compressionLevel );
                    rc = 0;
                    elapsed.Stop ( );
                  }
                  else
                  {
                    rc = 1;
                    ConsoleColor foregroundColor = Console.ForegroundColor;
                    try
                    {
                      if ( Properties.Settings.Default.OverrideColor )
                        Console.ForegroundColor = Properties.Settings.Default.ColorError;
                      Console.Error.WriteLine ( Properties.Resources.ErrorEntryFound, entryPath );
                    }
                    finally
                    {
                      Console.ForegroundColor = foregroundColor;
                    }
                  }
                }
                else
                {
                  rc = -1;
                  ConsoleColor foregroundColor = Console.ForegroundColor;
                  try
                  {
                    if ( Properties.Settings.Default.OverrideColor )
                      Console.ForegroundColor = Properties.Settings.Default.ColorError;
                    Console.Error.WriteLine ( Properties.Resources.ErrorSourceNotFound, sourcePath );
                  }
                  finally
                  {
                    Console.ForegroundColor = foregroundColor;
                  }
                }
                zipArchive.Dispose ( );
              }
              stream.Dispose ( );
            }

            if ( rc == 0 )
            {
              using ( FileStream stream = new FileStream ( archivePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None ) )
              {
                using ( ZipArchive zipArchive = new ZipArchive ( stream, ZipArchiveMode.Update ) )
                {
                  ZipArchiveEntry zipArchiveEntry = zipArchive.GetEntry ( entryPath );
                  if ( !( zipArchiveEntry == null ) )
                  {
                    tc++;
                    tl += (float)zipArchiveEntry.Length;
                    tcl += (float)zipArchiveEntry.CompressedLength;

                    if ( stats )
                    {
                      string c = tc.ToString ( "N0" );
                      string l = "***.** ??";
                      if ( tl > 999999999 )
                      {
                        l = ( tl / ( 1024F * 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatGB;
                      }
                      else
                      {
                        if ( tl > 999999 )
                        {
                          l = ( tl / ( 1024F * 1024F ) ).ToString ( "N2" ) + Properties.Resources.FormatMB;
                        }
                        else
                        {
                          l = ( tl ).ToString ( "N0" ) + Properties.Resources.FormatB;
                        }
                      }
                      string p = ( ( tl != 0.0F ) ? ( ( tl - tcl ) / tl ) : 0.0F ).ToString ( "P" );
                      string e = elapsed.Elapsed.ToString ( "g" ).Substring ( 0, 10 );
                      Console.WriteLine ( Properties.Resources.FormatStats, c, l, p, e );
                    }
                  }
                  else
                  {
                    rc = 1;
                    ConsoleColor foregroundColor = Console.ForegroundColor;
                    try
                    {
                      if ( Properties.Settings.Default.OverrideColor )
                        Console.ForegroundColor = Properties.Settings.Default.ColorError;
                      Console.Error.WriteLine ( Properties.Resources.ErrorEntryNotFound, entryPath );
                    }
                    finally
                    {
                      Console.ForegroundColor = foregroundColor;
                    }
                  }
                  zipArchive.Dispose ( );
                }
                stream.Dispose ( );
              }
            }
          }
          else
          {
            rc = -1;
            ConsoleColor foregroundColor = Console.ForegroundColor;
            try
            {
              if ( Properties.Settings.Default.OverrideColor )
                Console.ForegroundColor = Properties.Settings.Default.ColorError;
              Console.Error.WriteLine ( Properties.Resources.ErrorArchiveNotFound, archivePath );
            }
            finally
            {
              Console.ForegroundColor = foregroundColor;
            }
          }
        }
      }
      else
      {
        Console.Error.WriteLine ( Properties.Resources.HelpUnzipDir );
        rc = -1;
      }
      return rc;
    }
  }
}