using InfinityMatrix.Niraiya.UITests.Extentions;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

using TechTalk.SpecFlow;

namespace InfinityMatrix.Niraiya.UITests.Steps
{
    [Binding]
    public class StaticScreensSteps : IDisposable
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

        public StaticScreensSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
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

        [Given(@"Get Static Page URL from AppSetting for (.*) and (.*)")]
        public void GivenGetStaticPageURLFromAppSettingForAnd(string scenarioName, string pageURL_AppSettings)
        {
            this.ScenarioName = scenarioName;
            this.AppURL = _config["App-URL"] + _config[pageURL_AppSettings];
        }

        [Given(@"The Static Page is loaded for browser (.*) for device (.*)")]
        public void GivenTheStaticPageIsLoadedForBrowserForDevice(string browserEnvironment, string device)
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

        [When(@"Accept cookie button is clicked in Static Page")]
        public void WhenAcceptCookieButtonIsClickedInStaticPage()
        {
            var element = _driver.FindElement(By.Id("alertBtnAcceptCookie"));

            element.Click();
        }

        [Then(@"We were able to set the Static Page Screen color mode from AppSetting")]
        public void ThenWeWereAbleToSetTheStaticPageScreenColorModeFromAppSetting()
        {
            SeleniumExtentions.ScrollTo(this._jsDriver);

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

        [Then(@"Static Page Local storage value for theme '(.*)' is '(.*)'")]
        public void ThenStaticPageLocalStorageValueForThemeIs(string localStorage, string expectdValue)
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

        [Then(@"Take Static Pages page screenshot with name prefix (.*)")]
        public void ThenTakeStaticPagesPageScreenshotWithNamePrefix(string env)
        {
            if (this._driver != null)
            {
                var testResultsFolder = Directory.GetCurrentDirectory();
                string filename = this.ScenarioInfoTitle.Replace(" ", "_") + "_" + this.ScenarioInfoEnvironment + "_" + "toppage";
                SeleniumExtentions.TakesScreenshot(this._driver, testResultsFolder, filename);
            }
        }

        #endregion Shared

        #region 04_10 Navigate to Static Pages

        [Then(@"The Static Page loaded with in (.*) seconds")]
        public void ThenTheStaticPageLoadedWithInSeconds(int seconds)
        {
            TimeSpan ts = PageLoadTimer.Elapsed;

            Assert.IsTrue(seconds > ts.Seconds, "Page load time exceeds given value");
        }

        [Then(@"We were able to get Static Page total dimention")]
        public void ThenWeWereAbleToGetStaticPageTotalDimention()
        {
            var pageDimention = SeleniumExtentions.GetPageDimention(this._jsDriver);

            this.PageHeight = pageDimention.y;
            this.PageWidth = pageDimention.x;

            Assert.IsTrue(this.PageHeight > 0, "Invalid page height");
            Assert.IsTrue(this.PageWidth > 0, "Invalid Page Width");
        }

        [Then(@"The Static Page is scrolled step by step to end by millisecond delay (.*) by (.*)")]
        public void ThenTheStaticPageIsScrolledStepByStepToEndByMillisecondDelayBy(string millisecond, int pageScrollStep)
        {
            SeleniumExtentions.ScrollPageToEndByStep(this._jsDriver, this.PageHeight, this.PageWidth, Convert.ToInt32(millisecond), pageScrollStep);
            Thread.Sleep(1000);
        }

        #endregion 04_10 Navigate to Static Pages

        #region 04_20 Validate to LoadingIndicator Pages

        [Then(@"We were able check the LoadingIndicator text")]
        public void ThenWeWereAbleCheckTheLoadingIndicatorText()
        {
            Thread.Sleep(500);
            var element = _driver.FindElement(By.Id("myNavLoadingIndicatorText"));
            var text = element.Text;

            Thread.Sleep(500);
            Assert.AreEqual("Processing, Please Wait...", text, " LoadingIndicator text mismatch");
        }

        #endregion 04_20 Validate to LoadingIndicator Pages

        #region 04_30 & 04_40 Validate Error

        [Then(@"Raise ajax exception at server by clicking '(.*)' button")]
        public void ThenRaiseAjaxExceptionAtServerByClickingButton(string ajaxButton)
        {
            Thread.Sleep(500);
            var element = _driver.FindElement(By.Id(ajaxButton));
            element.Click();
            Thread.Sleep(1000);
        }

        [Then(@"Validate AjaxErrorPage Page by popup display")]
        public void ThenValidateAjaxErrorPagePageByPopupDisplay()
        {
            Thread.Sleep(1000);
            var element = _driver.FindElement(By.Id("ajaxErrorMessagePopUpMessage"));
            Thread.Sleep(1000);
            Assert.IsTrue(element.Displayed, "AjaxErrorPage popup is not displayed");
        }

        [Then(@"Validate Request Id against database log - AjaxErrorPage")]
        public void ThenValidateRequestIdAgainstDatabaseLog_AjaxErrorPage()
        {
            Thread.Sleep(1000);
            var element = _driver.FindElement(By.Id("RequestId"));

            Thread.Sleep(1000);

            Assert.IsTrue(element.Displayed, "AjaxErrorPage RequestId is not displayed");

            string requestId = element.Text;

            Assert.IsTrue(requestId.Length > 0, "AjaxErrorPage RequestId is has invalid length");

            string ipAddress = _driver.FindElement(By.Id("txtUserIpAddress")).Text.Trim();

            Thread.Sleep(1000);

            dynamic logUserDeviceInfo = this._sqlConnection.QuerySql(@"SELECT
	                                                            *
                                                              FROM [dbo].[LogUserDeviceInfo] WITH (NOLOCK)
                                                            WHERE RequestId = @RequestId", new { RequestId = requestId }).FirstOrDefault();

            Thread.Sleep(1000);

            Assert.IsNotNull(logUserDeviceInfo, "log UserDevice Info details should not be null");

            string actualIPAddress = logUserDeviceInfo.IpAddress;
            string actualRequestBlockType = logUserDeviceInfo.RequestBlockType;

            //Assert.AreEqual(actualIPAddress, ipAddress, " AjaxErrorPage IP Address mismatch");
            Assert.AreEqual("Error Occured", actualRequestBlockType, " AjaxErrorPage Request Block Type mismatch");
        }

        [Then(@"Validate Request Id against database log - ErrorPage")]
        public void ThenValidateRequestIdAgainstDatabaseLog_ErrorPage()
        {
            Thread.Sleep(1000);
            var element = _driver.FindElement(By.Id("RequestId"));

            Thread.Sleep(1000);

            Assert.IsTrue(element.Displayed, "ErrorPage RequestId is not displayed");

            string requestId = element.Text;

            Assert.IsTrue(requestId.Length > 0, "ErrorPage RequestId is has invalid length");

            string ipAddress = _driver.FindElement(By.Id("txtUserIpAddress")).Text.Trim();

            Thread.Sleep(1000);

            dynamic logUserDeviceInfo = this._sqlConnection.QuerySql(@"SELECT
	                                                            *
                                                              FROM [dbo].[LogUserDeviceInfo] WITH (NOLOCK)
                                                            WHERE RequestId = @RequestId", new { RequestId = requestId }).FirstOrDefault();

            Thread.Sleep(1000);

            Assert.IsNotNull(logUserDeviceInfo, "log UserDevice Info details should not be null");

            string actualIPAddress = logUserDeviceInfo.IpAddress;
            string actualRequestBlockType = logUserDeviceInfo.RequestBlockType;

            //Assert.AreEqual(actualIPAddress, ipAddress, " ErrorPage IP Address mismatch");
            Assert.AreEqual("Error Occured", actualRequestBlockType, " ErrorPage Request Block Type mismatch");
        }

        #endregion 04_30 & 04_40 Validate Error

        #region 04_50 04.41 Validate Raw HTML Page

        [Then(@"The Static Page wait for few seconds (.*)")]
        public void ThenTheStaticPageWaitForFewSeconds(int milisec)
        {
            Thread.Sleep(milisec);
        }

        #endregion 04_50 04.41 Validate Raw HTML Page

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