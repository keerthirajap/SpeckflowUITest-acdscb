using InfinityMatrix.Niraiya.UITests.Data;
using InfinityMatrix.Niraiya.UITests.Extentions;
using InfinityMatrix.Niraiya.UITests.Models;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using TechTalk.SpecFlow;

namespace InfinityMatrix.Niraiya.UITests.Steps
{
    [Binding]
    public class PrivacyAndSecurityScreenSteps : IDisposable
    {
        private static IConfiguration _config;

        private IWebDriver _driver;
        private ChromeOptions _chromeOptions;
        private FirefoxOptions _firefoxOptions;
        private IJavaScriptExecutor _jsDriver;

        private readonly DbConnection _sqlConnection;

        private string BrowserEnvironment = string.Empty;
        private string Device = string.Empty;
        private string TestRunNo = string.Empty;

        private string AppURL = string.Empty;
        private string ApplicationStorageName = string.Empty;
        private static Stopwatch PageLoadTimer;

        private Int32 PageHeight = 0;
        private Int32 PageWidth = 0;

        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;

        private string ScenarioInfoTitle = string.Empty;
        private string ScenarioInfoEnvironment = string.Empty;

        private string ScenarioId = string.Empty;
        private string TestName = string.Empty;
        private string ScenarioName = string.Empty;

        private string MailBoxEmailId = string.Empty;
        private string MailBoxPassword = string.Empty;

        public PrivacyAndSecurityScreenSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
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

            SqlInsightDbProvider.RegisterProvider();
            string sqlDbConnection = _config["DatabaseSetting:SqlDbConnection"];
            this._sqlConnection = new SqlConnection(sqlDbConnection);
        }

        #region Shared

        [Given(@"Get PrivacyAndSecurity page URL from AppSetting")]
        public void GivenGetPrivacyAndSecurityPageURLFromAppSetting()
        {
            this.AppURL = _config["App-URL"] + _config["URL:PrivacyAndSecurity-URL"];
            this.ApplicationStorageName = _config["ApplicationStorageName"];
        }

        [Given(@"The PrivacyAndSecurity page is loaded for browser (.*) for device (.*)")]
        public void GivenThePrivacyAndSecurityPageIsLoadedForBrowserForDevice(string browserEnvironment, string device)
        {
            this.BrowserEnvironment = browserEnvironment;
            this.Device = device;

            PageLoadTimer = new Stopwatch();
            PageLoadTimer.Start();

            var data = SeleniumExtentions.LoadPage(browserEnvironment, device, this._chromeOptions, this._firefoxOptions, this._driver, this._jsDriver, _config, this.AppURL);

            this._chromeOptions = data.chromeOptions;
            this._firefoxOptions = data.firefoxOptions;
            this._driver = data.driver;
            this._jsDriver = data.jsDriver;

            PageLoadTimer.Stop();
        }

        [When(@"Accept cookie button is clicked in PrivacyAndSecurity page")]
        public void WhenAcceptCookieButtonIsClickedInPrivacyAndSecurityPage()
        {
            var element = _driver.FindElement(By.Id("alertBtnAcceptCookie"));

            element.Click();
        }

        [Then(@"We were able to set the PrivacyAndSecurity Screen color mode from AppSetting")]
        public void ThenWeWereAbleToSetThePrivacyAndSecurityScreenColorModeFromAppSetting()
        {
            string colorMode = _config["App-Color-Mode"];

            Thread.Sleep(1000);

            if (this.Device != "Desktop")
            {
                var element11 = _driver.FindElement(By.XPath("//button[@class ='navbar-toggler']"));
                element11.Click();
                Thread.Sleep(1000);
            }

            if (colorMode == "dark")
            {
                var element2 = _driver.FindElement(By.Id("switchDarkMode"));

                Actions action = new Actions(_driver);

                action.MoveToElement(element2);

                action.Click();
                action.Perform();

                Thread.Sleep(1500);
            }

            if (this.Device != "Desktop")
            {
                var element12 = _driver.FindElement(By.XPath("//button[@class ='navbar-toggler']"));
                element12.Click();
                Thread.Sleep(1000);
            }
        }

        [Then(@"PrivacyAndSecurity Local storage value for theme '(.*)' is '(.*)'")]
        public void ThenPrivacyAndSecurityLocalStorageValueForThemeIs(string localStorage, string expectdValue)
        {
            Thread.Sleep(1000);

            string colorMode = _config["App-Color-Mode"];

            if (colorMode == "dark")
            {
                string executeScript = string.Format("return localStorage.getItem('{0}')", localStorage);

                var actualValue = Convert.ToString(this._jsDriver.ExecuteScript(executeScript));

                Assert.AreEqual(expectdValue, actualValue, "Local storage value mismatch");
            }
        }

        #endregion Shared

        #region 03_10 Get PrivacyAndSecurity Screen

        [Then(@"The PrivacyAndSecurity page loaded with in (.*) seconds")]
        public void ThenThePrivacyAndSecurityPageLoadedWithInSeconds(int seconds)
        {
            TimeSpan ts = PageLoadTimer.Elapsed;

            Assert.IsTrue(seconds > ts.Seconds, "Page load time exceeds given value");
            Debug.WriteLine("Actual page load time :" + ts.Seconds.ToString());
        }

        [Then(@"We were able to get PrivacyAndSecurity page total dimention")]
        public void ThenWeWereAbleToGetPrivacyAndSecurityPageTotalDimention()
        {
            var pageDimention = SeleniumExtentions.GetPageDimention(this._jsDriver);

            this.PageHeight = pageDimention.y;
            this.PageWidth = pageDimention.x;

            Assert.IsTrue(this.PageHeight > 0, "Invalid page height");
            Assert.IsTrue(this.PageWidth > 0, "Invalid Page Width");
        }

        [Then(@"The PrivacyAndSecurity page is scrolled step by step to end by millisecond delay (.*)")]
        public void ThenThePrivacyAndSecurityPageIsScrolledStepByStepToEndByMillisecondDelay(string millisecond)
        {
            SeleniumExtentions.ScrollPageToEnd(this._jsDriver, this.PageHeight, this.PageWidth, Convert.ToInt32(millisecond));
        }

        #endregion 03_10 Get PrivacyAndSecurity Screen

        #region 03_20 Accept Cookie Button for PrivacyAndSecurity

        [Given(@"Accept cookie alert is visible for first time in PrivacyAndSecurity page")]
        public void GivenAcceptCookieAlertIsVisibleForFirstTimeInPrivacyAndSecurityPage()
        {
            bool isElementDisplayed = _driver.FindElement(By.Id("alertAcceptCookie")).Displayed;

            Assert.IsTrue(isElementDisplayed, "Accept cookie alert is not visible for first time");
        }

        [Given(@"Accept cookie button is visible for first time in PrivacyAndSecurity page")]
        public void GivenAcceptCookieButtonIsVisibleForFirstTimeInPrivacyAndSecurityPage()
        {
            bool isElementDisplayed = _driver.FindElement(By.Id("alertBtnAcceptCookie")).Displayed;

            Assert.IsTrue(isElementDisplayed, "Accept cookie button is not visible for first time");
        }

        [Given(@"Local storage value for Accept cookie '(.*)' is '(.*)' in PrivacyAndSecurity page")]
        public void GivenLocalStorageValueForAcceptCookieIsInPrivacyAndSecurityPage(string localStorageName, string expectdValue)
        {
            localStorageName = localStorageName.Replace("ApplicationStorageName", this.ApplicationStorageName);
            string executeScript = string.Format("return localStorage.getItem('{0}')", localStorageName);

            var actualValue = Convert.ToString(this._jsDriver.ExecuteScript(executeScript));

            Assert.AreEqual(expectdValue, actualValue, "Local storage value mismatch");
        }

        [When(@"Accept cookie alert should not be visisble in PrivacyAndSecurity page")]
        public void WhenAcceptCookieAlertShouldNotBeVisisbleInPrivacyAndSecurityPage()
        {
            var isElementDisplayed = SeleniumExtentions.IsElementPresent(_driver, By.Id("alertAcceptCookie"));

            Assert.IsFalse(isElementDisplayed, "Accept cookie alert should not be visible");
        }

        [When(@"Accept cookie button should not be visisble in PrivacyAndSecurity page")]
        public void WhenAcceptCookieButtonShouldNotBeVisisbleInPrivacyAndSecurityPage()
        {
            var isElementDisplayed = SeleniumExtentions.IsElementPresent(_driver, By.Id("alertBtnAcceptCookie"));

            Assert.IsFalse(isElementDisplayed, "Accept cookie button should not be visible");
        }

        [When(@"Local storage value for Accept cookie '(.*)' is '(.*)' in PrivacyAndSecurity page")]
        public void WhenLocalStorageValueForAcceptCookieIsInPrivacyAndSecurityPage(string localStorageName, string expectdValue)
        {
            localStorageName = localStorageName.Replace("ApplicationStorageName", this.ApplicationStorageName);
            string executeScript = string.Format("return localStorage.getItem('{0}')", localStorageName);

            var actualValue = Convert.ToString(this._jsDriver.ExecuteScript(executeScript));

            Assert.AreEqual(expectdValue, actualValue, "Local storage value mismatch");
        }

        #endregion 03_20 Accept Cookie Button for PrivacyAndSecurity

        #region 03_30 Check PrivacyAndSecurity Accordion

        [Then(@"When click on Accordion Link '(.*)' Page should scroll to respective accordion and check header '(.*)'")]
        public void ThenWhenClickOnAccordionLinkPageShouldScrollToRespectiveAccordionAndCheckHeader(string accordionName, string accordionHeader)
        {
            _driver.Navigate().GoToUrl(AppURL);
            _jsDriver = (IJavaScriptExecutor)_driver;

            Thread.Sleep(1500);

            string accordion = string.Format("[href*='#{0}']", accordionName);

            var accordionLink = _driver.FindElement(By.CssSelector(accordion));

            SeleniumExtentions.ScrollToView(this._jsDriver, accordionLink);

            Thread.Sleep(2000);

            accordionLink.Click();

            var accordionText = _driver.FindElement(By.Id(accordionName));

            Thread.Sleep(2000);

            Assert.AreEqual(accordionHeader, accordionText.Text, "accordion header mismatch");
        }

        #endregion 03_30 Check PrivacyAndSecurity Accordion

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