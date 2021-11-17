namespace InfinityMatrix.Niraiya.UITests.Extentions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;

    using OpenQA.Selenium.DevTools.V93.Emulation;
    using OpenQA.Selenium.DevTools;

    public static class SeleniumExtentions
    {
        public static (ChromeOptions chromeOptions, FirefoxOptions firefoxOptions, IWebDriver driver, IJavaScriptExecutor jsDriver) LoadPage(string browserEnvironment, string device, ChromeOptions _chromeOptions, FirefoxOptions _firefoxOptions, IWebDriver _driver, IJavaScriptExecutor _jsDriver, IConfiguration _config, string AppURL)
        {
            if (browserEnvironment == "chrome")
            {
                _chromeOptions = new ChromeOptions();

                //_chromeOptions.AddArgument("--start-maximized");
                _chromeOptions.AddArguments("--disable-notifications");
                _chromeOptions.AddArguments("disable-infobars");
                _chromeOptions.AddArgument("no-sandbox");
                _chromeOptions.AddExcludedArgument("enable-automation");
                _chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

                if (device != "Desktop")
                {
                    _chromeOptions.EnableMobileEmulation(device);
                }
                else
                {
                    _chromeOptions.AddArgument("--window-size=1920,1080");
                    // _chromeOptions.AddAdditionalCapability("resolution", "2048x1536", true);
                }

                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                _driver = new ChromeDriver(path, _chromeOptions);

                _driver.Manage().Window.Maximize();

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            else if (browserEnvironment == "firefox")
            {
                _firefoxOptions = new FirefoxOptions();

                if (device != "Desktop")
                {
                    string user_agent = _config["FireFox_Mobile_user_agent"];

                    _firefoxOptions.SetPreference("general.useragent.override", user_agent);
                }

                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                _driver = new FirefoxDriver(path, _firefoxOptions);

                if (device != "Desktop")
                {
                    int fireFox_Mobile_Width = Convert.ToInt32(_config["FireFox_Mobile_Width"]);
                    int fireFox_Mobile_Height = Convert.ToInt32(_config["FireFox_Mobile_Height"]);

                    _driver.Manage().Window.Size = new Size(fireFox_Mobile_Width, fireFox_Mobile_Height);
                }
                else
                {
                    _driver.Manage().Window.Maximize();
                }
            }

            _driver.Navigate().GoToUrl(AppURL);
            _jsDriver = (IJavaScriptExecutor)_driver;

            return (_chromeOptions, _firefoxOptions, _driver, _jsDriver);
        }

        public static (ChromeOptions chromeOptions, FirefoxOptions firefoxOptions, IWebDriver driver, IJavaScriptExecutor jsDriver) LoadPageWithDevTools(string browserEnvironment, string device, ChromeOptions _chromeOptions, FirefoxOptions _firefoxOptions, IWebDriver _driver, IJavaScriptExecutor _jsDriver, IConfiguration _config, string AppURL)
        {
            if (browserEnvironment == "chrome")
            {
                _chromeOptions = new ChromeOptions();

                //_chromeOptions.AddArgument("--start-maximized");
                _chromeOptions.AddArguments("--disable-notifications");
                // _chromeOptions.AddArguments("disable-infobars");
                _chromeOptions.AddArgument("no-sandbox");
                _chromeOptions.AddExcludedArgument("enable-automation");
                _chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                _chromeOptions.AddArgument("--auto-open-devtools-for-tabs");
                //_chromeOptions.AddUserProfilePreference("auto-open-devtools-for-tabs", "true");

                if (device != "Desktop")
                {
                    _chromeOptions.EnableMobileEmulation(device);
                }
                else
                {
                    _chromeOptions.AddArgument("--window-size=1920,1080");
                    // _chromeOptions.AddAdditionalCapability("resolution", "2048x1536", true);
                }

                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                _driver = new ChromeDriver(path, _chromeOptions);

                _driver.Manage().Window.Maximize();

                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
            else if (browserEnvironment == "firefox")
            {
                _firefoxOptions = new FirefoxOptions();

                if (device != "Desktop")
                {
                    string user_agent = _config["FireFox_Mobile_user_agent"];

                    _firefoxOptions.SetPreference("general.useragent.override", user_agent);
                }

                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                _driver = new FirefoxDriver(path, _firefoxOptions);

                if (device != "Desktop")
                {
                    int fireFox_Mobile_Width = Convert.ToInt32(_config["FireFox_Mobile_Width"]);
                    int fireFox_Mobile_Height = Convert.ToInt32(_config["FireFox_Mobile_Height"]);

                    _driver.Manage().Window.Size = new Size(fireFox_Mobile_Width, fireFox_Mobile_Height);
                }
                else
                {
                    _driver.Manage().Window.Maximize();
                }
            }

            _driver.Navigate().GoToUrl(AppURL);
            _jsDriver = (IJavaScriptExecutor)_driver;

            return (_chromeOptions, _firefoxOptions, _driver, _jsDriver);
        }

        public static void TakesScreenshot(IWebDriver driver, string path, string filename)
        {
            if (driver != null)
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                Thread.Sleep(1500);

                string fullPath = Path.Combine(path, filename + ".png");

                ss.SaveAsFile(fullPath);
                Thread.Sleep(500);
            }
        }

        public static bool IsElementPresent(IWebDriver driver, By by)
        {
            var oldTime = driver.Manage().Timeouts().ImplicitWait;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1500);

            try
            {
                driver.FindElement(by);
                driver.Manage().Timeouts().ImplicitWait = oldTime;
                return true;
            }
            catch (NoSuchElementException)
            {
                driver.Manage().Timeouts().ImplicitWait = oldTime;
                return false;
            }
        }

        public static string GetElementText(IWebDriver driver, By by)
        {
            try
            {
                var element = driver.FindElement(by);

                return element.Text;
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }

        public static void ScrollTo(IJavaScriptExecutor jsDriver, int xPosition = 0, int yPosition = 0)
        {
            var js = String.Format("window.scrollTo({0}, {1})", xPosition, yPosition);
            jsDriver.ExecuteScript(js);
        }

        public static IWebElement ScrollToView(IJavaScriptExecutor jsDriver, IWebDriver driver, By selector)
        {
            var element = driver.FindElement(selector);
            ScrollToView(jsDriver, element);
            return element;
        }

        public static void ScrollToView(IJavaScriptExecutor jsDriver, IWebElement element)
        {
            if (element.Location.Y > 200)
            {
                ScrollTo(jsDriver, 0, element.Location.Y - 100); // Make sure element is in the view but below the top navigation pane
            }
        }

        public static (int x, int y) GetCoordinatesOfElement(IWebDriver driver, By by)
        {
            IsElementPresent(driver, by);

            var element = driver.FindElement(by);

            return (1, 1);
        }

        public static (int x, int y) GetPageDimention(IJavaScriptExecutor jsDriver)
        {
            string executeScript1 = string.Format("return $(document).height()");
            string executeScript2 = string.Format("return $(document).width()");

            int PageHeight = Convert.ToInt32(jsDriver.ExecuteScript(executeScript1));
            int PageWidth = Convert.ToInt32(jsDriver.ExecuteScript(executeScript2));

            return (PageWidth, PageHeight);
        }

        public static void ScrollPageToElement(IJavaScriptExecutor jsDriver, int pageHeight, int pageWidth)
        {
            SeleniumExtentions.ScrollTo(jsDriver, pageWidth, pageHeight);
        }

        public static void ScrollPageToEnd(IJavaScriptExecutor jsDriver, int pageHeight, int pageWidth, int millisecond)
        {
            List<int> pageScrollHeight = new List<int>();

            var pageScrollHeightAll = Enumerable.Range(1, pageHeight).ToList();

            int count = 0;

            foreach (var item in pageScrollHeightAll)
            {
                count++;

                if (count >= 200)
                {
                    pageScrollHeight.Add(item);
                    count = 0;
                }
            }

            foreach (var item in pageScrollHeight)
            {
                Thread.Sleep(millisecond);
                SeleniumExtentions.ScrollTo(jsDriver, 0, item);
            }
        }

        public static void ScrollPageToEndByStep(IJavaScriptExecutor jsDriver, int pageHeight, int pageWidth, int millisecond, int step)
        {
            List<int> pageScrollHeight = new List<int>();

            var pageScrollHeightAll = Enumerable.Range(1, pageHeight).ToList();

            int count = 0;

            foreach (var item in pageScrollHeightAll)
            {
                count++;

                if (count >= step)
                {
                    pageScrollHeight.Add(item);
                    count = 0;
                }
            }

            foreach (var item in pageScrollHeight)
            {
                Thread.Sleep(millisecond);
                SeleniumExtentions.ScrollTo(jsDriver, 0, item);
            }
        }
    }
}