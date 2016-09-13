using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logblog_console
{
    class LogBlog
    {
        static void Main(string[] args)
        {
            String logAddress;


            //Console.SetWindowSize(84, 84);
            //Console.SetWindowPosition(50, 50);

            Console.WriteLine("Please point me to the log");

            //logAddress = Console.ReadLine();
            //logAddress = "D:\\Users\\colin.denny\\Desktop\\LogBlog\\SWIM.JobServices.RetailWindowsService.2016.09.05.log";
            logAddress = @"C:\Users\cden\Desktop\GitHub\LogBlog\trunk\TestFiles\SWIM.JobServices.RetailWindowsService.2016.09.05.log";

            Console.WriteLine("Reading Log: " + logAddress);

            LogReader newLog = new LogReader(logAddress);

            Console.WriteLine("\n" + "Please enter any key to close");
            Console.ReadKey();
        }
    }
}
