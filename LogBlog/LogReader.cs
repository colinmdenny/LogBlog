using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

public class LogReader
{
    // Get the context buffer setting from the app.config file
    // Use these to determine how many lines to write either side of the error
    // Setup the buffered line which is used to determine where we are in the file

    private int beforeContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["beforeContextBuffer"]);
    private int afterContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["afterContextBuffer"]);
    private int bufferedLine = -1;
    private string logAddress;

    public LogReader(string logPath)
	{
        // Set the class variable log path
        logAddress = logPath;
        
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = WriteSafeReadAllLines(@logAddress); 

        // Display the lines to the console
        DisplayAllErrors(lines, logAddress);

        // Watch the log file
        WatchLog();    
    }
        
    // Used to read a log file into an array 
    // Done in this way as you may only have read access to the log file. 
    // string[] lines = ReadAllLines(@LogAddress) is much quicker but cannot set the access level so it will throw and exception.
    // Not happy with performance

    private string[] WriteSafeReadAllLines(string path)
    {
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var sr = new StreamReader(fs))
        {
            List<string> listlines = new List<string>();
            while (!sr.EndOfStream)
            {
                listlines.Add(sr.ReadLine());
            }

            return listlines.ToArray();
        }
    }

    // Used to display all the errors in the log file
    // Do this when initially reading the file

    private void DisplayAllErrors(Array lines, string logAddress)
    {
        // Display the errors in the file contents by using a loop.
        System.Console.WriteLine("Log Contents:" + Environment.NewLine);
        foreach (string line in lines)
        {
            // Get the line number by using the array index and use a bufferedLine to ensure no duplicate errors
            int LineNo = Array.IndexOf(lines, line) + 1;

            if ((line.ToLower().Contains("exception") || line.ToLower().Contains("error")) && (bufferedLine < LineNo))
            {
                // Write to console
                Console.WriteLine("\n" + "Error on Line #" + LineNo + " in " + logAddress + Environment.NewLine);

                // Print out the error using the context buffers to determine the number of lines
                // Take 1 off the line number to account for zero index in array
                int startLine = LineNo - beforeContextBuffer;
                int endLine = LineNo + afterContextBuffer;
                while (startLine <= endLine)
                {
                    Console.WriteLine(startLine + " --- " + lines.GetValue(startLine - 1));
                    startLine++;
                }
                bufferedLine = endLine;
            }
        }
    }

    private void WatchLog ()
    {
        // Create a new FileSystemWatcher and set its properties.
        FileSystemWatcher watcher = new FileSystemWatcher();
        watcher.Path = Path.GetDirectoryName(logAddress);
        // Filter to watch the specific file
        watcher.Filter = Path.GetFileName(logAddress);

        // Watch for changes in LastAccess and LastWritimes
        watcher.NotifyFilter = NotifyFilters.LastWrite;

        // Add event handler
        watcher.Changed += new FileSystemEventHandler(OnChanged);

        // Begin watching.
        watcher.EnableRaisingEvents = true;

        // Wait for the user to quit the program.
        Console.WriteLine(Environment.NewLine + "Press \'q\' to quit the sample.");
        while (Console.Read() != 'q') ;
    }

    // Define the event handlers.
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        // Specify what is done when a file is changed, created, or deleted.
        DisplayUpdate();
    }

    private void DisplayUpdate ()
    {
        Console.WriteLine(Environment.NewLine + "Fileupdated");
    }

}
