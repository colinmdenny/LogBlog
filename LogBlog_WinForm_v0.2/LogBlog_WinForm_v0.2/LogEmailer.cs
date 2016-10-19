using System;
using System.Configuration;
using System.Net.Mail;

public class LogEmailer
{
    // Get the email settings from the config file
    private string toMailAddress = ConfigurationManager.AppSettings["toMailAddress"];
    private string fromMailAddress = ConfigurationManager.AppSettings["fromMailAddress"];
    private string smtpServer = ConfigurationManager.AppSettings["smtpServer"];
    private string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
    private string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];
    
    public LogEmailer(string body, string logName)
	{
        try
        {
            // Create the email and send using the config settings
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(smtpServer);

            mail.From = new MailAddress(fromMailAddress);
            mail.To.Add(toMailAddress);
            mail.Subject = "LogBlog - Issues encountered in " + logName + " @ " + DateTime.Now.ToString("HH:mm:ss");
            mail.Body = body;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(smtpUserName, smtpPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
	}
}
