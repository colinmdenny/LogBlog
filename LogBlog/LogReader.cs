using System;
using System.Configuration;

public class LogReader
{
    public LogReader(String LogAddress)
	{
        // Get the context buffer setting from the app.config file
        // Use this to determine how many lines to write either side of the error
        // Setup the buffered line

        int beforeContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["beforeContextBuffer"]);
        int afterContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["afterContextBuffer"]);
        int bufferedLine = -1;

        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = System.IO.File.ReadAllLines(@LogAddress);

        // Display the errors in the file contents by using a foreach loop.
        System.Console.WriteLine("Log Contents:" + "\n");
        foreach (string line in lines)
        {
            // Get the line number by using the array index and use a bufferedLine to ensure no duplicate errors
            int LineNo = Array.IndexOf(lines, line) +1;

            if ((line.ToLower().Contains("exception") || line.ToLower().Contains("error")) && (bufferedLine < LineNo))
            {
                // Write to console
                Console.WriteLine("\n" + "Error on Line #" + LineNo + " in " + LogAddress + "\n");

                // Print out the error using the context buffers to determine the number of lines
                // Take 1 off the line number to account for zero index in array
                int startLine = LineNo - beforeContextBuffer;
                int endLine = LineNo + afterContextBuffer;
                while (startLine <= endLine)
                {
                    Console.WriteLine(startLine + " --- " + lines.GetValue(startLine -1));
                    startLine++;
                }
                bufferedLine = endLine;
                //Console.WriteLine(bufferedLine);
            }
        }
	}
}
