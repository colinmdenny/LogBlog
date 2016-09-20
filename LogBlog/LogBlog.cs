﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace logblog_console
{
    class LogBlog
    {
        static void Main(string[] args)
        {
            string logAddress;



            //logAddress = @"C:\Users\cden\Desktop\GitHub\LogBlog\trunk\TestFiles\SWIM.JobServices.RetailWindowsService.2016.09.05.log";
            //logAddress = @"Z:\Applications\SWIM\Logs\SWIM.JobServices.RetailWindowsService.2016.09.20.log";
            logAddress = @"D:\Users\colin.denny\Desktop\GitHub\LogBlog\trunk\TestFiles\SWIM.JobServices.RetailWindowsService.2016.09.05.log";

            
            
            // Set up the console
            Console.BufferHeight = Int16.MaxValue - 1;
            Console.BufferWidth = 1000;
            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(150, 50);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Title = "LogBlog - " + "blogging for " + Path.GetFileName(logAddress);
            //Want to be able to setup - insert and quick edit mode as standard on startup - allows copy paste

            // Interact with the user
            Console.WriteLine("***** LogBlog *****" + Environment.NewLine);
            Console.WriteLine("Please enter the full path, including filename and extension, to the log" + Environment.NewLine);
            //logAddress = @Console.ReadLine();
            Console.WriteLine("Reading Log: " + logAddress + Environment.NewLine);

            // Initiate the log reader
            LogReader newLog = new LogReader(logAddress);
        }
    }
}
