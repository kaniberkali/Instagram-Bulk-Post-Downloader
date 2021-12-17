using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Net;
using System.Drawing;

namespace Instagram_Bulk_Post_Downloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "Instagram Bulk Post Downloader";
            Console.WriteLine("Website : https://kodzamani.weebly.com");
            Console.WriteLine("Instagram : @kodzamani.tk");
            Console.WriteLine("--------------------------------------");

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            ChromeDriver instagram = new ChromeDriver(service);
            instagram.Navigate().GoToUrl("https://www.instagram.com/accounts/login/?source=auth_switcher");
            for (; ;)
            {
                try
                {
                    string veri = instagram.FindElement(By.XPath("//span[@class='_2dbep qNELH']/img")).GetAttribute("alt");
                    break;
                }
                catch
                {
                    Console.WriteLine("Hesabınıza giriş yapmanız bekleniyor.");
                }
                Thread.Sleep(2000);
            }
            for (; ; )
            {
                try
                {
                    instagram.FindElement(By.XPath("/html/body/div[1]/section/main/div/div[3]/article/div[1]/div/div[1]/div[1]/a/div/div[2]")).Click();
                    break;
                }
                catch
                {
                    Console.WriteLine("Profil seçmeniz bekleniyor.");
                }
                Thread.Sleep(2000);
            }
            Console.Clear();
            Console.WriteLine("Website : https://kodzamani.weebly.com");
            Console.WriteLine("Instagram : @kodzamani.tk");
            Console.WriteLine("--------------------------------------");
            if (Directory.Exists("Resimler") == false)
                Directory.CreateDirectory("Resimler");
            List<string> gonderiler = new List<string>();
            bool ilkbasma = false;
            int i = 0;
            for(; ; )
            {
                Thread.Sleep(2000);
                try
                {
                    i++;
                    int count = instagram.FindElements(By.XPath("//div[@class='KL4Bh']/img")).Count() - 1;
                    string resim = instagram.FindElements(By.XPath("//div[@class='KL4Bh']/img"))[count].GetAttribute("src");
                    if (gonderiler.Contains(resim) == false)
                    {
                        string zamanbaslık = instagram.FindElement(By.XPath("//time[@class='_1o9PC Nzb55']")).GetAttribute("title");
                        string zamandetay = instagram.FindElement(By.XPath("//time[@class='_1o9PC Nzb55']")).GetAttribute("datetime");
                        string zamansonuc = zamanbaslık + " " + zamandetay.Split('T')[1].Split('.')[0].Replace(":", ".");
                        using (var client = new WebClient())
                        {
                            client.DownloadFile(resim, "Resimler/" + zamansonuc + ".jpg");
                        }
                        gonderiler.Add(resim);
                        Console.WriteLine(zamansonuc + " > Tarihindeki gönderiniz başarıyla indirildi.");
                        if (ilkbasma == false)
                        {
                            instagram.FindElements(By.XPath("//html/body/div[6]/div[1]/div/div/div/button"))[0].Click();
                            ilkbasma = true;
                        }
                    }
                    try
                    {
                        if (i >= 2)
                        {
                            instagram.FindElements(By.XPath("//html/body/div[6]/div[1]/div/div/div/button"))[1].Click();
                            i = 2;
                        }
                    }
                    catch { break; }
                }
                catch{ }
            }
            Console.Clear();
            Console.WriteLine("Website : https://kodzamani.weebly.com");
            Console.WriteLine("Instagram : @kodzamani.tk");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Toplam indirilen gönderi :" + gonderiler.Count);
            Console.WriteLine("Tüm işlemler başarıyla bitirildi çıkış yapmak için herhangi bir tuşa basın.");
            instagram.Quit();
            Console.ReadLine();
        }
    }
}
