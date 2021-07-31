using ApplicationServices.Business.IAppServices;
using Domain.Contracts.V1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationServices.Business
{
    public class SearchNumberPlate : ISearchNumberPlate
    {
        public CarModel testSearchNumberPlate(string no,string Proxy)
        {

            CarModel cars = new CarModel();
            ChromeDriver driver;
            var options = new ChromeOptions();
            var proxy = new OpenQA.Selenium.Proxy();
            proxy.Kind = ProxyKind.Manual;
            proxy.IsAutoDetect = false;

            //140.227.8.93    USA
            //203.190.12.122  Bangal
            //118.140.160.84  HongKong
            proxy.HttpProxy = Proxy;
            options.Proxy = proxy;
            options.AddArguments("--start-maximized");
            options.AddArgument("--ignore-certificate-errors");
            var driverService = ChromeDriverService.CreateDefaultService();

            driver = new ChromeDriver(driverService, options);

            driver.ExecuteChromeCommand(
               "Network.setBlockedURLs",
               new Dictionary<string, object>
               {
                   ["urls"] = new[] {
                        "https://client.px-cloud.net/PXuR63h57Z/main.min.js",
                        "https://collector-pxur63h57z.px-cloud.net/api/v2/collector",
                        "https://client.perimeterx.net/PXuR63h57Z/main.min.js",
                        "https://client.px-cdn.net/PXuR63h57Z/main.min.js"
                   }
               }
           );
            driver.ExecuteChromeCommand("Network.enable", new Dictionary<string, object>());
            try {
            


            driver.Navigate().GoToUrl("https://siv-auto.fr/index.php");

            Thread.Sleep(1000);

            var acceptServices = driver.FindElementsByXPath("//form[@class='consent']/nav/div[@class='front']/button");
            if (acceptServices.Count > 0)
            {
                acceptServices[2].Click();
                Thread.Sleep(500);
            }
            var inputNumber = driver.FindElementsByXPath("//form/center/input");
            if (inputNumber.Count > 0)
            {
                //AS852XZ
                //BG494NA
                inputNumber[0].Click();
                inputNumber[0].SendKeys(no);
                Thread.Sleep(100);
              

            }
          
            var antiCaptchaKey = "72638d6ad7aa9d010d0b82bf4e7b11d3";

            try
            {
                //driver.SwitchTo().Frame(0);
                var captcha = driver.FindElementByXPath("//div[@class='g-recaptcha']");


                //driver.FindElementById("px-captcha");
                Console.WriteLine("Captcha detected");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript(
                    "var antcptAccountKeyDiv = document.getElementById('anticaptcha-imacros-account-key');" +
                    "if (!antcptAccountKeyDiv)" +
                    "{" +
                    "antcptAccountKeyDiv = document.createElement('div');" +
                    $"antcptAccountKeyDiv.innerHTML = '{antiCaptchaKey}';" +
                    "antcptAccountKeyDiv.style.display = 'none';" +
                    "antcptAccountKeyDiv.id = 'anticaptcha-imacros-account-key';" +
                    "document.body.appendChild(antcptAccountKeyDiv);" +
                    "}"
                );

                js.ExecuteScript(
                    @"
                        var script = document.createElement('script');
                        script.type = 'text/javascript';
                        script.src = 'https://cdn.antcpt.com/imacros_inclusion/recaptcha.js';    

                        document.getElementsByTagName('head')[0].appendChild(script);
                        "
                );
                Thread.Sleep(5000);
            
                var RecaptchaSucess = driver.FindElementsByXPath("//div[@class='g-recaptcha']/div/div");
                if (RecaptchaSucess.Count > 0)
                {
                up:
                    var divCount = driver.FindElementsByXPath("//div[@class='g-recaptcha']/div/div[2]");
                    if (divCount.Count > 0)
                    {
                        if (divCount[0].Text != "Solved")
                        {
                            Thread.Sleep(2000);
                            goto up;
                        }
                    }
                }

                var submitBtn = driver.FindElementByXPath("//button[@class='myButton']");
                submitBtn.Click();

            }
            catch (Exception ex)
            {
                cars.Message = new[] {ex.Message};
                // ignored -- dont leave empty. write exception in log/file
                //So we come to know what xception is
            }

            var errorMessage = driver.FindElementsByXPath("//form/span");
            if (errorMessage.Count > 0)
            {
                if (errorMessage[0].Text == "Oups Cette recherche ne mène à rien.")
                {
                    cars.Message = new[] { "Oops This search does not lead to anything." };
                    //Console.WriteLine("Oops This search does not lead to anything.");
                    
                }
            }

                var BtnforNextPage = driver.FindElementsByXPath("//a[@class='myButton']");
                if (BtnforNextPage.Count > 0)
                {
                    BtnforNextPage[0].Click();
                }

                var licenseNumber = driver.FindElementsByXPath("//div[@class='titre']");
                if (licenseNumber.Count > 0)
                {
                    cars.data.licensePlate = licenseNumber[0].Text;
                }
                var ListofDetails = driver.FindElementsByXPath("//ul[@class='specs']/li");
                if (ListofDetails.Count > 0)
                {
                    foreach (var item in ListofDetails)
                    {
                        var Li = item.FindElement(By.XPath("./strong"));
                    //string str=Li.Text +" "+ item.FindElement(By.XPath("./span")).Text;
                    //str = str + "/";
                    string Attributes = Li.Text.ToString();
                    switch(Attributes)
                    {
                        case "Marque":
                            cars.data.Mark = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Gamme":
                            cars.data.Range = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Modèle":
                            cars.data.Model = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Phase":
                            cars.data.Phase = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Série":
                            cars.data.Series = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Libelle":
                            cars.data.Wording = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Carrosserie":
                            cars.data.Body = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Type Mines":
                            cars.data.TypeMines = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "VIN":
                            cars.data.WINE = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Moteur":
                            cars.data.Motor = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Couleur":
                            cars.data.Color = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Mise en Circulation":
                            cars.data.Release = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Début de Commercialisation":
                            cars.data.StartofMarketing = item.FindElement(By.XPath("./span")).Text;
                            break;
                        case "Fin de Commercialisation":
                            cars.data.EndofMarketing = item.FindElement(By.XPath("./span")).Text;
                            break;
                        default:
                            Console.WriteLine("Null");
                            break;
                    }
                        //////if (Li.Text == "Marque")
                        //////{
                        //////    cars.data.Mark = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Gamme")
                        //////{
                        //////    cars.data.Range = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Modèle")
                        //////{
                        //////    cars.data.Model = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Phase")
                        //////{
                        //////    cars.data.Phase = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Série")
                        //////{
                        //////    cars.data.Series = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Libelle")
                        //////{
                        //////    cars.data.Wording = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Carrosserie")
                        //////{
                        //////    cars.data.Body = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Type Mines")
                        //////{
                        //////    cars.data.TypeMines = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "VIN")
                        //////{
                        //////    cars.data.WINE = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Moteur")
                        //////{
                        //////    cars.data.Motor = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Couleur")
                        //////{
                        //////    cars.data.Color = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Mise en Circulation")
                        //////{
                        //////    cars.data.Release = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Début de Commercialisation")
                        //////{
                        //////    cars.data.StartofMarketing = item.FindElement(By.XPath("./span")).Text;
                        //////}
                        //////else if (Li.Text == "Fin de Commercialisation")
                        //////{
                        //////    cars.data.EndofMarketing = item.FindElement(By.XPath("./span")).Text;
                        //////}

                    }
                    cars.Success = true;
                }
            }
            catch (Exception ex)
            {
                cars.Message = new[] { ex.Message };
                
            }
            driver.Close();
            return cars;
          

        }
    }
}
