using InfinityMatrix.Niraiya.UITests.Extentions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace InfinityMatrix.Niraiya.UITests.Steps
{
    [Binding]
    public class HomeScreenSteps : IDisposable
    {
        private static IConfiguration _config;

        private IWebDriver _driver;
        private ChromeOptions _chromeOptions;
        private FirefoxOptions _firefoxOptions;
        private IJavaScriptExecutor _jsDriver;

        private string AppURL = string.Empty;
        private string ApplicationStorageName = string.Empty;
        private static Stopwatch PageLoadTimer;

        private Int32 PageHeight = 0;
        private Int32 PageWidth = 0;

        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;

        private string ScenarioInfoTitle = string.Empty;
        private string ScenarioInfoEnvironment = string.Empty;

        public HomeScreenSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            this._scenarioContext = scenarioContext;
            this._featureContext = featureContext;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (_config == null)
            {
                _config = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(path, "appsettings.json"), optional: false, reloadOnChange: true)
                    .Build();
            }

            this.ScenarioInfoTitle = _scenarioContext.ScenarioInfo.Title;
            var arguments = _scenarioContext.ScenarioInfo.Arguments;
            this.ScenarioInfoEnvironment = arguments[(string)"Environment"].ToString();
        }

        #region Shared

        [Given(@"Get Home page URL from AppSetting")]
        public void GivenGetHomePageURLFromAppSetting()
        {
            this.AppURL = _config["App-URL"];
            this.ApplicationStorageName = _config["ApplicationStorageName"];
        }

        [Given(@"The Home page is loaded for browser (.*) for device (.*)")]
        public void GivenTheHomePageIsLoadedForBrowserForDevice(string browserEnvironment, string device)
        {
            PageLoadTimer = new Stopwatch();
            PageLoadTimer.Start();

            var data = SeleniumExtentions.LoadPage(browserEnvironment, device, this._chromeOptions, this._firefoxOptions, this._driver, this._jsDriver, _config, this.AppURL);

            this._chromeOptions = data.chromeOptions;
            this._firefoxOptions = data.firefoxOptions;
            this._driver = data.driver;
            this._jsDriver = data.jsDriver;

            PageLoadTimer.Stop();
        }

        [When(@"Accept cookie button is clicked")]
        public void WhenAcceptCookieButtonIsClicked()
        {
            var element = _driver.FindElement(By.Id("alertBtnAcceptCookie"));

            element.Click();
        }

        [Given(@"Take Home page screenshot with name suffix '(.*)'")]
        [Then(@"Take Home page screenshot with name suffix '(.*)'")]
        public void ThenTakeHomePageScreenshotWithNameSuffix(string suffixname)
        {
            if (this._driver != null)
            {
                var testResultsFolder = Directory.GetCurrentDirectory();
                string filename = this.ScenarioInfoTitle.Replace(" ", "_") + "_" + this.ScenarioInfoEnvironment + "_" + suffixname;
                SeleniumExtentions.TakesScreenshot(this._driver, testResultsFolder, filename);
            }
        }

        #endregion Shared

        #region 01.10 Get Home Screen

        [When(@"The Home page is loaded for browser (.*) for device (.*)")]
        public void WhenTheHomePageIsLoadedForBrowserForDevice(string browserEnvironment, string device)
        {
            PageLoadTimer = new Stopwatch();
            PageLoadTimer.Start();

            var data = SeleniumExtentions.LoadPage(browserEnvironment, device, this._chromeOptions, this._firefoxOptions, this._driver, this._jsDriver, _config, this.AppURL);

            this._chromeOptions = data.chromeOptions;
            this._firefoxOptions = data.firefoxOptions;
            this._driver = data.driver;
            this._jsDriver = data.jsDriver;

            PageLoadTimer.Stop();
        }

        [Then(@"The Home page loaded with in (.*) seconds")]
        public void ThenTheHomePageLoadedWithInSeconds(int seconds)
        {
            TimeSpan ts = PageLoadTimer.Elapsed;

            Assert.IsTrue(seconds > ts.Seconds, "Page load time exceeds given value");
        }

        [Given(@"We were able to get Home page total dimention")]
        [Then(@"We were able to get Home page total dimention")]
        public void ThenWeWereAbleToGetHomePageTotalDimention()
        {
            var pageDimention = SeleniumExtentions.GetPageDimention(this._jsDriver);

            this.PageHeight = pageDimention.y;
            this.PageWidth = pageDimention.x;

            Assert.IsTrue(this.PageHeight > 0, "Invalid page height");
            Assert.IsTrue(this.PageWidth > 0, "Invalid Page Width");
        }

        [Then(@"The Home page is scrolled step by step to end by millisecond delay (.*) by (.*)")]
        public void ThenTheHomePageIsScrolledStepByStepToEndByMillisecondDelayBy(string millisecond, int pageScrollStep)
        {
            SeleniumExtentions.ScrollPageToEndByStep(this._jsDriver, this.PageHeight, this.PageWidth, Convert.ToInt32(millisecond), pageScrollStep);
            Thread.Sleep(1000);
        }

        #endregion 01.10 Get Home Screen

        #region 01.20 Accept Cookie Button

        [Given(@"Accept cookie alert is visible for first time")]
        public void GivenAcceptCookieAlertIsVisibleForFirstTime()
        {
            bool isElementDisplayed = _driver.FindElement(By.Id("alertAcceptCookie")).Displayed;

            Assert.IsTrue(isElementDisplayed, "Accept cookie alert is not visible for first time");
        }

        [Given(@"Accept cookie button is visible for first time")]
        public void GivenAcceptCookieButtonIsVisibleForFirstTime()
        {
            bool isElementDisplayed = _driver.FindElement(By.Id("alertBtnAcceptCookie")).Displayed;

            Assert.IsTrue(isElementDisplayed, "Accept cookie button is not visible for first time");
        }

        [Given(@"Local storage value for Accept cookie '(.*)' is '(.*)'")]
        public void GivenLocalStorageValueForAcceptCookieIs(string localStorageName, string expectdValue)
        {
            localStorageName = localStorageName.Replace("ApplicationStorageName", this.ApplicationStorageName);
            string executeScript = string.Format("return localStorage.getItem('{0}')", localStorageName);

            var actualValue = Convert.ToString(this._jsDriver.ExecuteScript(executeScript));

            Assert.AreEqual(expectdValue, actualValue, "Local storage value mismatch");
        }

        [When(@"Accept cookie alert should not be visisble")]
        public void WhenAcceptCookieAlertShouldNotBeVisisble()
        {
            var isElementDisplayed = SeleniumExtentions.IsElementPresent(_driver, By.Id("alertAcceptCookie"));

            Assert.IsFalse(isElementDisplayed, "Accept cookie alert should not be visible");
        }

        [When(@"Accept cookie button should not be visisble")]
        public void WhenAcceptCookieButtonShouldNotBeVisisble()
        {
            var isElementDisplayed = SeleniumExtentions.IsElementPresent(_driver, By.Id("alertAcceptCookie"));

            Assert.IsFalse(isElementDisplayed, "Accept cookie alert should not be visible");
        }

        [When(@"Local storage value for Accept cookie '(.*)' is '(.*)'")]
        public void WhenLocalStorageValueForAcceptCookieIs(string localStorageName, string expectdValue)
        {
            localStorageName = localStorageName.Replace("ApplicationStorageName", this.ApplicationStorageName);
            string executeScript = string.Format("return localStorage.getItem('{0}')", localStorageName);

            var actualValue = Convert.ToString(this._jsDriver.ExecuteScript(executeScript));

            Assert.AreEqual(expectdValue, actualValue, "Local storage value mismatch");
        }

        #endregion 01.20 Accept Cookie Button

        #region 01_12 On IP Address Change

        [Given(@"IP Address details should not be empty for fields '(.*)' '(.*)' '(.*)' '(.*)'")]
        public void GivenIPAddressDetailsShouldNotBeEmptyForFields(string ip, string isp, string region, string country)
        {
            Thread.Sleep(2500);

            ip = _driver.FindElement(By.Id("txtUserIpAddress")).Text;
            isp = _driver.FindElement(By.Id("txtUserIpISP")).Text;
            region = _driver.FindElement(By.Id("txtUserRegion")).Text;
            country = _driver.FindElement(By.Id("txtUserCountry")).Text;

            Assert.IsFalse(string.IsNullOrEmpty(ip), "IP address should not be empty");
            Assert.IsFalse(string.IsNullOrEmpty(isp), "ISP address should not be empty");
            Assert.IsFalse(string.IsNullOrEmpty(region), "Region address should not be empty");
            Assert.IsFalse(string.IsNullOrEmpty(country), "Country address should not be empty");
        }

        [Given(@"The Home page is scrolled to bottom")]
        public void GivenTheHomePageIsScrolledToBottom()
        {
            SeleniumExtentions.ScrollPageToElement(this._jsDriver, this.PageHeight, this.PageWidth);
        }

        [When(@"On IP Address Change configured from AppSetting")]
        public void WhenOnIPAddressChangeConfiguredFromAppSetting()
        {
            //Proxy proxy = new Proxy();
            //proxy.Kind = ProxyKind.Manual;
            //proxy.IsAutoDetect = false;
            //proxy.SslProxy = "42.119.252.88:8001";
            //_chromeOptions.Proxy = proxy;

            //_chromeOptions.AddExcludedArgument("enable-automation");
            //_chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

            ////chromeOptions.AddArguments("headless");
            //_driver = new ChromeDriver(this._testContext.TestDirectory, _chromeOptions);
            //_driver.Navigate().GoToUrl(this.AppURL);
            //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Then(@"IP Address details should be changed for fields '(.*)' '(.*)' '(.*)' '(.*)'")]
        public void ThenIPAddressDetailsShouldBeChangedForFields(string p0, string p1, string p2, string p3)
        {
        }

        #endregion 01_12 On IP Address Change

        #region Home Dev_Tools Not Allowed Redirect Validation

        [Given(@"The Homepage is loaded for browser with devtools (.*) for device (.*)")]
        public void GivenTheHomepageIsLoadedForBrowserWithDevtoolsForDevice(string browserEnvironment, string device)
        {
            PageLoadTimer = new Stopwatch();
            PageLoadTimer.Start();

            var data = SeleniumExtentions.LoadPageWithDevTools(browserEnvironment, device, this._chromeOptions, this._firefoxOptions, this._driver, this._jsDriver, _config, this.AppURL);

            this._chromeOptions = data.chromeOptions;
            this._firefoxOptions = data.firefoxOptions;
            this._driver = data.driver;
            this._jsDriver = data.jsDriver;

            PageLoadTimer.Stop();
        }

        [When(@"Dev Tools Tab is opened in browser")]
        public void WhenDevToolsTabIsOpenedInBrowser()
        {
            Thread.Sleep(3000);
        }

        [Then(@"Validate user shown as dev tools not allowed from h(.*) tag")]
        public void ThenValidateUserShownAsDevToolsNotAllowedFromHTag(int p0)
        {
        }

        #endregion Home Dev_Tools Not Allowed Redirect Validation

        public void Dispose()
        {
            if (this._driver != null)
            {
                this._driver.Close();
                this._driver.Quit();
                this._driver.Dispose();
                this._driver = null;
            }
        }

        [AfterScenario()]
        public void AfterScenario()
        {
            if (this._driver != null)
            {
                var testResultsFolder = Directory.GetCurrentDirectory();
                string filename = this.ScenarioInfoTitle.Replace(" ", "_") + "_" + this.ScenarioInfoEnvironment + "_" + "AftSce";
                SeleniumExtentions.TakesScreenshot(this._driver, testResultsFolder, filename);
            }
        }
    }
}