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
            string logAddress;

            //logAddress = @"C:\Users\cden\Desktop\GitHub\LogBlog\trunk\TestFiles\SWIM.JobServices.RetailWindowsService.2016.09.05.log";
            logAddress = @"Z:\Applications\SWIM\Logs\SWIM.JobServices.RetailWindowsService.2016.09.19.log";
            //logAddress = @"D:\Users\colin.denny\Desktop\GitHub\LogBlog\trunk\TestFiles\SWIM.JobServices.RetailWindowsService.2016.09.05.log";

            Console.WriteLine("Reading Log: " + logAddress);

            LogReader newLog = new LogReader(logAddress);
        }
    }
}
