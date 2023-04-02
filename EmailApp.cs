using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using EASendMail;

/* Any kind of accesses or info was modified to protect the company's integrity */

namespace BrowserApp
{
    class EntryPoint
    {
        static void Main(String[] args)
        {
            string pathDownload = "C:\\Users\\Data.pdf";

            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://app.powerbi.com");

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email")));
            driver.FindElement(By.Id("email")).SendKeys("bi.manager@");
            driver.FindElement(By.Id("submitBtn")).Click();

            //driver.manage().timeouts().implicitlyWait(30, TimeUnit.SECONDS);
            //driver.Manage().Timeouts().ImplicitWait(30, TimeUnit.SECONDS);
            //WebElement ele = new WebDriverWait(driver, 3).Until(driver -> driver.findElement)

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("i0118")));
            driver.FindElement(By.Id("i0118")).SendKeys("Zol67363");
            driver.FindElement(By.Id("idSIButton9")).Click();
            driver.FindElement(By.Id("idBtn_Back")).Click();


            //wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("Workspaces")));

            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(" //*[@id=\"leftNavPane\"]/section/nav/button/span")));

            driver.FindElement(By.XPath("//*[@id=\"leftNavPane\"]/section/nav/button")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"cdk-overlay-1\"]/nav/nav-pane-workspaces/div[3]/virtual-scroll/div[2]/ul/li[1]/workspace-button/button[1]")));
            driver.FindElement(By.XPath("//*[@id=\"cdk-overlay-1\"]/nav/nav-pane-workspaces/div[3]/virtual-scroll/div[2]/ul/li[1]/workspace-button/button[1]")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"workspace-list-nav\"]/div[2]/div/div/button[2]")));
            driver.FindElement(By.XPath("//*[@id=\"workspace-list-nav\"]/div[2]/div/div/button[2]")).Click();
            driver.FindElement(By.LinkText("Data File")).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("exportMenuBtn")));
            driver.FindElement(By.Id("exportMenuBtn")).Click();
            driver.FindElement(By.XPath("/html/body/div[3]/div[3]/div/div/div/button[3]")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("okButton")));
            driver.FindElement(By.Id("okButton")).Click();

            Thread.Sleep(660000);
            driver.Quit();

            try
            {
                SmtpMail oMail = new SmtpMail("Mail");

                // Your email address
                oMail.From = "@outlook.com";

                // Set recipient email address
                oMail.To = "@gmail.com";

                // Set email subject
                oMail.Subject = "attachment";

                // Set email body
                oMail.TextBody = "this is a email sent from # project with a pdf attachment named Data";

                // Hotmail/Outlook SMTP server address
                SmtpServer oServer = new SmtpServer("smtp.office365.com");
                oMail.AddAttachment(pathDownload);

                // User authentication should use your
                // email address as the user name.
                oServer.User = "@outlook.com";

                // If you got authentication error, try to create an app password instead of your user password.
                oServer.Password = "pass";

                // use 587 TLS port
                oServer.Port = 587;

                // detect SSL/TLS connection automatically
                oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                Console.WriteLine("start to send email over TLS...");

                SmtpClient oSmtp = new SmtpClient();
                oSmtp.SendMail(oServer, oMail);

                Console.WriteLine("email was sent successfully!");

                File.Delete(pathDownload);
            }
            catch (Exception ep)
            {
                Console.WriteLine("failed to send email with the following error:");
                Console.WriteLine(ep.Message);
            }
        }
    }
}
