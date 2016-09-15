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
    
    public LogReader(String logAddress)
	{
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.

        string[] lines = WriteSafeReadAllLines(@logAddress); 

        //Display the lines to the console

        DisplayAllErrors(lines, logAddress);    
    }
        
    // Used to read a log file into an array 
    // Done in this way as you may only have read access to the log file. 
    // string[] lines = ReadAllLines(@LogAddress) is much quicker but cannot set the access level so it will throw and exception.
    // Not happy with performance

    private string[] WriteSafeReadAllLines(String path)
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
	
}
