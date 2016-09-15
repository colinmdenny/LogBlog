using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Linq;

public class LogReader_Phil
{
    public LogReader_Phil(String LogAddress)
	{
        // Get the context buffer setting from the app.config file
        // Use this to determine how many lines to write either side of the error
        // Setup the buffered line

        int beforeContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["beforeContextBuffer"]);
        int afterContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["afterContextBuffer"]);
        int bufferedLine = -1;

        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        List<string> lines = new List<string>();

        using (var fs = new FileStream(@LogAddress, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var sr = new StreamReader(fs, Encoding.Default))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }

        // Display the errors in the file contents by using a foreach loop.

        int LineNo = 0;
        System.Console.WriteLine("Log Contents:" + Environment.NewLine);
        foreach (string line in lines)
        {
            // Get the line number by using the array index and use a bufferedLine to ensure no duplicate errors
            LineNo++;
            if ((line.ToLower().Contains("exception") || line.ToLower().Contains("error")) && (bufferedLine < LineNo))
            {
                // Write to console
                Console.WriteLine("\n" + "Error on Line #" + LineNo + " in " + LogAddress + "\n");

                // Print out the error using the context buffers to determine the number of lines
                // Take 1 off the line number to account for zero index in array
                int startLine = LineNo - beforeContextBuffer;
                int endLine = LineNo + afterContextBuffer;

                //var filteredLines = lines.GetRange(startLine - 1, endLine - startLine);
                //filteredLines.ForEach(x => Console.WriteLine(startLine + " --- " + x));

                while (startLine <= endLine)
                {
                    Console.WriteLine(startLine + " --- " + lines.ToArray().GetValue(startLine -1));
                    startLine++;
                }
                bufferedLine = endLine;
                //Console.WriteLine(bufferedLine);
            }
        }
	}
}
