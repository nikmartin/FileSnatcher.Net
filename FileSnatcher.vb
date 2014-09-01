''' FileSnatcher
''' Allows you to copy files automatically when they are created

Imports System
Imports System.IO
Imports Microsoft.VisualBasic

Module FileSnatcher
   Dim WithEvents Snatch As New FileSystemWatcher()
   Dim args() As String

   Sub Main()

      Console.WriteLine("FileSnatcher version .9")
      args = System.Environment.GetCommandLineArgs()
      ' If a directory is not specified, exit the program.
      If args.Length < 3 Then
         ' Display the proper way to call the program.
         Console.WriteLine("Usage: FileSnatcher.exe (directory to watch) (directory to copy to) (filter [*.*])")
         Return
      End If

      Snatch.NotifyFilter = NotifyFilters.FileName

      Try



      Snatch.Path = args(1)
      If args.Length < 4 Then
         Snatch.Filter = "*.*"
      Else
         Snatch.Filter = args(3)
      End If

      AddHandler Snatch.Created, AddressOf OnChanged
      Snatch.EnableRaisingEvents = True

      ' Wait for the user to quit the program.
      Console.WriteLine("Press 'q' to quit watching.")
      While Chr(Console.Read()) <> "q"c
         System.Threading.Thread.Sleep(0)
      End While

      Catch e As Exception

         Console.WriteLine(e.Message)



      End Try




   End Sub

   Sub OnChanged(ByVal sender As Object, ByVal e As System.IO.FileSystemEventArgs)

      'File.Copy(e.FullPath, args(2) & "\" & e.Name, True)
      Try
         File.Copy(e.FullPath, args(2) & "\" & e.Name, True)
         'File.Delete(e.FullPath)
      Catch

      End Try

      Console.WriteLine("Moving: " & e.FullPath & " To: " & args(2) & "\" & e.Name)

   End Sub

End Module
