using LogBlog_WinForm_v0._2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

public class LogReader
{
    // Get the context buffer setting from the app.config file
    // Use these to determine how many lines to write either side of the error
    // Setup the buffered line which is used to determine where we are in the file

    private static int beforeContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["beforeContextBuffer"]);
    private static int afterContextBuffer = Int32.Parse(ConfigurationManager.AppSettings["afterContextBuffer"]);
    private static double triggerBuffer = double.Parse(ConfigurationManager.AppSettings["triggerBuffer"]);
    private int bufferedLine = -1;
    private static string logAddress;
    private static MainForm form;
    private static FileSystemWatcher watcher;
    private Timer timer;
    private static DateTime triggerTime;
    private static DateTime nextTriggerTime = DateTime.UtcNow;
    private LogEmailer emailer;


    public LogReader(string logPath, MainForm f)
    {
        // Setup the class variable form to the current instance of the passed over form
        form = f;
 
        // Set the class variable log path
        logAddress = logPath;
        
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] lines = WriteSafeReadAllLines(@logAddress); 

        // Display the lines to the console
        DisplayAllErrors(lines);

        // Setup a timer to do a asynchronous call to read the log. This will ensure the UI doesn't freeze       
        timer = new Timer();
        timer.Interval = 1000;
        timer.Enabled = true;
        
        // Setup up and event to handle what to do when then timer is activated
        timer.Tick += new EventHandler(TimerEventProcessor);

        // Setup the log watcher
        SetupWatchLog();   
 
        // Start the timer to check the log at every interval
        timer.Start();
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
            string allText = sr.ReadToEnd();
            string[] lines = allText.Split('\n');
            return lines;

            //List<string> listlines = new List<string>();
            //while (!sr.EndOfStream)
            //{
            //    listlines.Add(sr.ReadLine());
            //}
            //return listlines.ToArray();
        }
    }

    // Used to read a log file into an array then reduce the array to only the new lines
    // Not used as it help anything as still need to read the full file in. A better solution would be able to read from the log starting from the new lines.

    private string[] WriteSafeReadNewLines(string path)
    {
        using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var sr = new StreamReader(fs))
        {
            List<string> listlines = new List<string>();
            while (!sr.EndOfStream)
            {
                listlines.Add(sr.ReadLine());
            }
            listlines.RemoveRange(0, ((bufferedLine - 1) - beforeContextBuffer));
            return listlines.ToArray();
        }
    }

    // Used to display all the errors in the log file
    // Do this when initially reading the file

    private void DisplayAllErrors(Array lines)
    {
        // Create a string builder to create the output
        System.Text.StringBuilder errors = new System.Text.StringBuilder();
        
        // Use a variable to determine the amount of errors
        int errorCount = 0;
        
        // Display the errors in the file contents by using a loop.
        foreach (string line in lines)
        {
            // Get the line number by using the array index and use a bufferedLine to ensure no duplicate errors
            int LineNo = Array.IndexOf(lines, line) + 1;

            if ((line.ToLower().Contains("exception") || line.ToLower().Contains("error") || line.ToLower().Contains("paused")) && (bufferedLine < LineNo))
            {
                // Write to error the string builder
                errors.Append("\n\r" + "Error on Line #" + LineNo + " in " + logAddress + "\n\r");
                //form.UpdateOutput(Environment.NewLine + "Error on Line #" + LineNo + " in " + logAddress + Environment.NewLine + Environment.NewLine);
                
                // Print out the error using the context buffers to determine the number of lines
                // Take 1 off the line number to account for zero index in array
                int startLine = LineNo - beforeContextBuffer - 1;
                int endLine = LineNo + afterContextBuffer - 1;
                if (endLine > lines.Length)
                {
                    endLine = lines.Length;
                }

                while (startLine < endLine)
                {
                    errors.Append((startLine + 1) + " --- " + lines.GetValue(startLine));
                    //form.UpdateOutput((startLine + 1) + " --- " + lines.GetValue(startLine));
                    startLine++;
                }
                bufferedLine = endLine;
                errorCount++;
            }
        }

        if (errorCount.Equals(0))
        {
            //Update the GUI with the error string
            errors.Append("No errors or exceptions found!" + "\n\r");
            form.UpdateOutput(errors.ToString());
        }
        else
        {

            //Update the GUI with the error string and email the error
            form.UpdateOutput(errors.ToString());
            emailer = new LogEmailer(errors.ToString(), Path.GetFileName(logAddress));
        }
    }

    // Used to display only the new errors in the log file
    // Not used still under development

    private void DisplayNewErrors(Array lines)
    {
        // Display the new errors in the file contents by using a loop.
        // Determine where to start looping by using the bufferedLine

        int newLineStart = bufferedLine - 1;
        int newLineEnd = lines.Length;

        while (newLineStart < newLineEnd)
        {
            // Get the line number by using the array index and use a bufferedLine to ensure no duplicate errors
            //int LineNo = Array.IndexOf(lines, line) + 1;

            //if ((line.ToLower().Contains("exception") || line.ToLower().Contains("error")) && (bufferedLine < LineNo))
            //{
            //    // Write to console
            //    Console.WriteLine("\n" + "Error on Line #" + LineNo + " in " + logAddress + Environment.NewLine);

            //    // Print out the error using the context buffers to determine the number of lines
            //    // Take 1 off the line number to account for zero index in array
            //    int startLine = LineNo - beforeContextBuffer -1;
            //    int endLine = LineNo + afterContextBuffer -1;
            //    if (endLine > lines.Length)
            //    {
            //        endLine = lines.Length;
            //    }

            //    while (startLine < endLine)
            //    {
            //         Console.WriteLine((startLine + 1) + " --- " + lines.GetValue(startLine));
            //          startLine++;
            //    }
            //    bufferedLine = endLine;
            //}
        }
    }

    private void SetupWatchLog()
    {
        // Create a new FileSystemWatcher and set its properties.
        watcher = new FileSystemWatcher();
        watcher.Path = Path.GetDirectoryName(logAddress);

        // Filter to watch the specific file
        watcher.Filter = Path.GetFileName(logAddress);

        // Watch for changes in LastAccess and LastWritimes
        watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size;

        // Add event handler
        watcher.Changed += new FileSystemEventHandler(OnChanged);
    }

    // Define the event handlers.
    private void OnChanged(object source, FileSystemEventArgs e)
    {
        // Get the time of trigger to avoid double trigger
        triggerTime = DateTime.UtcNow;

        if (triggerTime >= nextTriggerTime)
        {
            Console.WriteLine("Passed Trigger Test - triggerTime = " + triggerTime + " nextTriggerTime = " + nextTriggerTime);
            form.UpdateOutput("\n\r" + "File updated " + DateTime.Now.ToString("HH:mm:ss") + "\n\r");

            // Specify what is done when a file is changed, created, or deleted.
            DisplayUpdate();

            // Set the next trigger time to ensure we don't get any other notifications within that period - configured by triggerBuffer
            nextTriggerTime = triggerTime.AddSeconds(triggerBuffer);
        }
        else
        {
            Console.WriteLine("Failed Trigger Test - triggerTime = " + triggerTime + " nextTriggerTime = " + nextTriggerTime);
        }
    }

    private void DisplayUpdate()
    {
        // Read each line of the file into a string array. Each element
        // of the array is one line of the file.
        string[] newLines = WriteSafeReadAllLines(@logAddress);

        // Display the lines to the console
        DisplayAllErrors(newLines);
    }

    private static void TimerEventProcessor(Object myObject,
                                        EventArgs myEventArgs)
    {
        // Begin watching the log
        watcher.EnableRaisingEvents = true;
    }
}
