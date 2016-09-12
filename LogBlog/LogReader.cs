using System;

public class LogReader
{
    public LogReader(String LogAddress)
	{
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = System.IO.File.ReadAllLines(@LogAddress);

        // Display the file contents by using a foreach loop.
        System.Console.WriteLine("Log Contents:" + "\n");
        foreach (string line in lines)
        {
            if (line.ToLower().Contains("exception") || line.ToLower().Contains("error"))
            {
                //Get the line number by using the array index
                Int32 LineNo = Array.IndexOf(lines, line) +1;

                // Use a tab to indent each line of the file.
                Console.WriteLine("Line #" + LineNo + " ----- " + line + "\n");
            }
        }
	}
}
