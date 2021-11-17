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
    public class ContactUsScreenSteps : IDisposable
    {
        private static IConfiguration _config;

        private IWebDriver _driver;
        private ChromeOptions _chromeOptions;
        private FirefoxOptions _firefoxOptions;
        private IJavaScriptExecutor _jsDriver;

        private readonly DbConnection _sqlConnection;

        private string BrowserEnvironment = string.Empty;
        private string Device = string.Empty;

        private string AppURL = string.Empty;
        private string ApplicationStorageName = string.Empty;
        private static Stopwatch PageLoadTimer;

        private Int32 PageHeight = 0;
        private Int32 PageWidth = 0;

        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;

        private string ScenarioInfoTitle = string.Empty;
        private string ScenarioInfoEnvironment = string.Empty;

        private List<ContactUsScreenModel> ContactUsScreenModels = new List<ContactUsScreenModel>();
        private ContactUsScreenModel ContactUsScenario = new ContactUsScreenModel();

        private string ScenarioId = string.Empty;
        private string TestName = string.Empty;
        private string ScenarioName = string.Empty;

        private string MailBoxEmailId = string.Empty;
        private string MailBoxPassword = string.Empty;

        public ContactUsScreenSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
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

        [Given(@"Get URL from AppSetting to load contactus URL (.*)")]
        public void GivenGetURLFromAppSettingToLoadContactusURL(string contactUsUrl)
        {
            this.AppURL = _config["App-URL"] + contactUsUrl;
            this.ApplicationStorageName = _config["ApplicationStorageName"];
        }

        [Then(@"Take ContactUs page screenshot with name suffix '(.*)'")]
        public void ThenTakeContactUsPageScreenshotWithNameSuffix(string suffixname)
        {
            if (this._driver != null)
            {
                var testResultsFolder = Directory.GetCurrentDirectory();
                string filename = this.ScenarioInfoTitle.Replace(" ", "_") + "_" + this.ScenarioInfoEnvironment + "_" + suffixname;
                SeleniumExtentions.TakesScreenshot(this._driver, testResultsFolder, filename);
            }
        }

        [Given(@"The ContactUs page is loaded for browser (.*) for device (.*)")]
        [When(@"The ContactUs page is loaded for browser (.*) for device (.*)")]
        public void WhenTheContactUsPageIsLoadedForBrowserForDevice(string browserEnvironment, string device)
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

        [Given(@"Accept cookie button is clicked in contactus page")]
        public void GivenAcceptCookieButtonIsClickedInContactusPage()
        {
            var element = _driver.FindElement(By.Id("alertBtnAcceptCookie"));

            element.Click();
        }

        [Then(@"The ContactUs page loaded with in (.*) seconds")]
        public void ThenTheContactUsPageLoadedWithInSeconds(int seconds)
        {
            TimeSpan ts = PageLoadTimer.Elapsed;

            Assert.IsTrue(seconds > ts.Seconds, "Page load time exceeds given value");
        }

        [Then(@"We were able to set the ContactUs Screen color mode from AppSetting")]
        public void ThenWeWereAbleToSetTheContactUsScreenColorModeFromAppSetting()
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

        [Then(@"ContactUs Local storage value for theme '(.*)' is '(.*)'")]
        public void ThenContactUsLocalStorageValueForThemeIs(string localStorage, string expectdValue)
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

        #region 02_10 Scroll To Bottom ContactUs Screen

        [Then(@"We were able to get ContactUs page total dimention")]
        public void ThenWeWereAbleToGetContactUsPageTotalDimention()
        {
            var pageDimention = SeleniumExtentions.GetPageDimention(this._jsDriver);

            this.PageHeight = pageDimention.y;
            this.PageWidth = pageDimention.x;

            Assert.IsTrue(this.PageHeight > 0, "Invalid page height");
            Assert.IsTrue(this.PageWidth > 0, "Invalid Page Width");
        }

        [Then(@"The ContactUs page is scrolled step by step to end by millisecond delay (.*)")]
        public void ThenTheContactUsPageIsScrolledStepByStepToEndByMillisecondDelay(string millisecond)
        {
            SeleniumExtentions.ScrollPageToEnd(this._jsDriver, this.PageHeight, this.PageWidth, Convert.ToInt32(millisecond));
        }

        #endregion 02_10 Scroll To Bottom ContactUs Screen

        #region 02_20 ContactUs Screen Submit - Negative - chrome

        [Then(@"Enter Name field data to the screen for scenario (.*) And Submit button is clicked And Validate")]
        public void ThenEnterNameFieldDataToTheScreenForScenarioAndSubmitButtonIsClickedAndValidate(string scenarioName, Table table)
        {
            Thread.Sleep(1000);

            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs Screen Submit - Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsName"));

                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.Name);

                Thread.Sleep(500);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                Thread.Sleep(500);

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(1000);

                contactSupportBtn.Click();

                Thread.Sleep(500);

                var validationFieldExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsName-error"));

                Assert.IsTrue(validationFieldExists, "Name validation failed for scenario Id : " + this.ScenarioId.ToString());

                var validationMessageActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsName-error"));

                Assert.AreEqual(ContactUsScenario.NameEM, validationMessageActual, "Name validation text failed for scenario Id : " + this.ScenarioId.ToString());
            }
        }

        [Then(@"Enter EmailId field data to the screen for scenario (.*) And Submit button is clicked And Validate")]
        public void ThenEnterEmailIdFieldDataToTheScreenForScenarioAndSubmitButtonIsClickedAndValidate(string scenarioName, Table table)
        {
            if (table.RowCount > 0)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(1500);
            }

            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs Screen Submit - Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsEmailId"));

                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.EmailId);

                Thread.Sleep(500);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                Thread.Sleep(500);

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(500);

                contactSupportBtn.Click();

                Thread.Sleep(500);

                var validationFieldExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsEmailId-error"));

                Assert.IsTrue(validationFieldExists, "EmailId validation failed for scenario Id : " + this.ScenarioId.ToString());

                var validationMessageActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsEmailId-error"));

                Assert.AreEqual(ContactUsScenario.EmailIdEM, validationMessageActual, "EmailId validation text failed for scenario Id : " + this.ScenarioId.ToString());
            }
        }

        [Then(@"Enter Subject field data to the screen for scenario (.*) And Submit button is clicked And Validate")]
        public void ThenEnterSubjectFieldDataToTheScreenForScenarioAndSubmitButtonIsClickedAndValidate(string scenarioName, Table table)
        {
            if (table.RowCount > 0)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(1500);
            }

            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs Screen Submit - Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsSubject"));

                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.Subject);

                Thread.Sleep(500);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                Thread.Sleep(500);

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(500);

                contactSupportBtn.Click();

                Thread.Sleep(500);

                var validationFieldExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsSubject-error"));

                Assert.IsTrue(validationFieldExists, "Subject validation failed for scenario Id : " + this.ScenarioId.ToString());

                var validationMessageActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsSubject-error"));

                Assert.AreEqual(ContactUsScenario.SubjectEM, validationMessageActual, "Subject validation text failed for scenario Id : " + this.ScenarioId.ToString());
            }
        }

        [Then(@"Enter Message field data to the screen for scenario (.*) And Submit button is clicked And Validate")]
        public void ThenEnterMessageFieldDataToTheScreenForScenarioAndSubmitButtonIsClickedAndValidate(string scenarioName, Table table)
        {
            if (table.RowCount > 0)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(1500);
            }

            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs Screen Submit - Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsMessage"));

                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.Message);

                Thread.Sleep(500);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                Thread.Sleep(500);

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(500);

                contactSupportBtn.Click();

                Thread.Sleep(500);

                var validationFieldExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsMessage-error"));

                Assert.IsTrue(validationFieldExists, "Message validation failed for scenario Id : " + this.ScenarioId.ToString());

                var validationMessageActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsMessage-error"));

                Assert.AreEqual(ContactUsScenario.MessageEM, validationMessageActual, "Message validation text failed for scenario Id : " + this.ScenarioId.ToString());
            }
        }

        [Then(@"Select AgreePrivacyPolicy field data to the screen for scenario (.*) And Submit button is clicked And Validate")]
        public void ThenSelectAgreePrivacyPolicyFieldDataToTheScreenForScenarioAndSubmitButtonIsClickedAndValidate(string scenarioName, Table table)
        {
            if (table.RowCount > 0)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(1500);
            }

            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs Screen Submit - Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                IWebElement webContactUsName = _driver.FindElement(By.Id("CheckAgreePrivacyPolicy"));

                Thread.Sleep(500);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                Thread.Sleep(500);

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(500);

                contactSupportBtn.Click();

                Thread.Sleep(500);

                var validationFieldExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("AgreePrivacyPolicy-error"));

                Assert.IsTrue(validationFieldExists, "AgreePrivacyPolicy validation failed for scenario Id : " + this.ScenarioId.ToString());

                var validationMessageActual = SeleniumExtentions.GetElementText(_driver, By.Id("AgreePrivacyPolicy-error"));

                Assert.AreEqual(ContactUsScenario.AgreePrivacyPolicyEM, validationMessageActual, "AgreePrivacyPolicy validation text failed for scenario Id : " + this.ScenarioId.ToString());
            }
        }

        [Then(@"Enter (.*) fields valid data to the screen for scenario (.*) And Submit button is clicked And Validate")]
        public void ThenEnterFieldsValidDataToTheScreenForScenarioAndSubmitButtonIsClickedAndValidate(int p0, string scenarioName, Table table)
        {
            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs Screen Submit - Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(1500);

                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                if (ContactUsScenario.AgreePrivacyPolicy)
                {
                    IWebElement webContactUsCheckAgreePrivacyPolicy = _driver.FindElement(By.Id("CheckAgreePrivacyPolicy"));

                    webContactUsCheckAgreePrivacyPolicy.Click();

                    Thread.Sleep(1000);
                }

                Thread.Sleep(100);

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsName"));
                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.Name);

                Thread.Sleep(100);

                IWebElement webContactUsEmailId = _driver.FindElement(By.Id("txtContactUsEmailId"));
                webContactUsEmailId.Clear();
                webContactUsEmailId.SendKeys(ContactUsScenario.EmailId);

                Thread.Sleep(100);

                IWebElement webContactUsSubject = _driver.FindElement(By.Id("txtContactUsSubject"));
                webContactUsSubject.Clear();
                webContactUsSubject.SendKeys(ContactUsScenario.Subject);

                Thread.Sleep(100);

                IWebElement webContactUsMessage = _driver.FindElement(By.Id("txtContactUsMessage"));
                webContactUsMessage.Clear();
                webContactUsMessage.SendKeys(ContactUsScenario.Message);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(1000);

                contactSupportBtn.Click();

                var validationFieldNameExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsName-error"));
                var validationFieldEmailIdExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsEmailId-error"));
                var validationFieldSubjectExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsSubject-error"));
                var validationFieldMessageExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("txtContactUsMessage-error"));
                var validationFieldAgreePrivacyPolicyExists = SeleniumExtentions.IsElementPresent(_driver, By.Id("AgreePrivacyPolicy-error"));

                if (ContactUsScenario.NameValidation)
                {
                    Assert.IsTrue(validationFieldNameExists, "Error message for name should be in screen");

                    var validationMessageNameActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsName-error"));

                    Assert.AreEqual(ContactUsScenario.NameEM, validationMessageNameActual, "Name validation text failed for scenario Id : " + this.ScenarioId.ToString());
                }
                else
                {
                    Assert.IsFalse(validationFieldNameExists, "Error message for name should be not in screen for ScenarioId" + this.ScenarioId);
                }

                if (ContactUsScenario.EmailIdValidation)
                {
                    Assert.IsTrue(validationFieldEmailIdExists, "Error message for email should be in screen for ScenarioId" + this.ScenarioId);

                    var validationMessageEmailIdActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsEmailId-error"));

                    Assert.AreEqual(ContactUsScenario.EmailIdEM, validationMessageEmailIdActual, "Email validation text failed for scenario Id : " + this.ScenarioId.ToString());
                }
                else
                {
                    Assert.IsFalse(validationFieldEmailIdExists, "Error message for email should be not in screen for ScenarioId" + this.ScenarioId);
                }

                if (ContactUsScenario.SubjectValidation)
                {
                    Assert.IsTrue(validationFieldSubjectExists, "Error message for Subject should be in screen for ScenarioId" + this.ScenarioId);

                    var validationMessageSubjectActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsSubject-error"));

                    Assert.AreEqual(ContactUsScenario.SubjectEM, validationMessageSubjectActual, "Subject validation text failed for scenario Id : " + this.ScenarioId.ToString());
                }
                else
                {
                    Assert.IsFalse(validationFieldSubjectExists, "Error message for Subject should not be in screen for ScenarioId" + this.ScenarioId);
                }

                if (ContactUsScenario.MessageValidation)
                {
                    Assert.IsTrue(validationFieldMessageExists, "Error message for Message should  be in screen for ScenarioId" + this.ScenarioId);

                    var validationMessageMessageActual = SeleniumExtentions.GetElementText(_driver, By.Id("txtContactUsMessage-error"));

                    Assert.AreEqual(ContactUsScenario.MessageEM, validationMessageMessageActual, "Message validation text failed for scenario Id : " + this.ScenarioId.ToString());
                }
                else
                {
                    Assert.IsFalse(validationFieldMessageExists, "Error message for Message should not be in screen for ScenarioId" + this.ScenarioId);
                }

                if (ContactUsScenario.AgreePrivacyPolicyValidation)
                {
                    Assert.IsTrue(validationFieldAgreePrivacyPolicyExists, "Error message for AgreePrivacyPolicy should be in screen for ScenarioId" + this.ScenarioId);
                    var validationMessageAgreePrivacyPolicyActual = SeleniumExtentions.GetElementText(_driver, By.Id("AgreePrivacyPolicy-error"));

                    Assert.AreEqual(ContactUsScenario.AgreePrivacyPolicyEM, validationMessageAgreePrivacyPolicyActual, "AgreePrivacyPolicy validation text failed for scenario Id : " + this.ScenarioId.ToString());
                }
                else
                {
                    Assert.IsFalse(validationFieldAgreePrivacyPolicyExists, "Error message for AgreePrivacyPolicy should not be in screen for ScenarioId" + this.ScenarioId);
                }
            }
        }

        #endregion 02_20 ContactUs Screen Submit - Negative - chrome

        #region 02_30 ContactUs Screen Valid Submit - Positive

        [Given(@"Old Records for IP address are cleared in Database for ContactUs Page")]
        public void GivenOldRecordsForIPAddressAreClearedInDatabaseForContactUsPage()
        {
            Thread.Sleep(1000);

            string ipAddress = _driver.FindElement(By.Id("txtUserIpAddress")).Text.Trim();

            Thread.Sleep(1000);

            string checkSQLConnectionByDBName = this._sqlConnection.ExecuteScalarSql<string>("SELECT db_name()").Trim();

            string dbNameExpected = _config["DatabaseSetting:DatabaseName"].Trim();

            Assert.AreEqual(dbNameExpected, checkSQLConnectionByDBName, "SQL connection failed for scenario Id : " + this.ScenarioId.ToString());

            this._sqlConnection.ExecuteSql("DELETE FROM [dbo].[LogContactUs] WHERE [CreatedIpAddress] = @ipAddress", new { ipAddress = ipAddress });

            Thread.Sleep(1000);
        }

        [When(@"All Mails for MailBox configured in AppSetting are deleted for ContactUs Screen")]
        public void WhenAllMailsForMailBoxConfiguredInAppSettingAreDeletedForContactUsScreen()
        {
            this.MailBoxEmailId = _config["Email-Box:EmailId"];
            this.MailBoxPassword = _config["Email-Box:Password"];

            MailBoxExtentions.DeleteAllEmails(this.MailBoxEmailId, this.MailBoxPassword);
        }

        [Then(@"Enter Valid data for (.*) time in the screen for scenario (.*) And Click submit button And Validate by (.*)")]
        public void ThenEnterValidDataForTimeInTheScreenForScenarioAndClickSubmitButtonAndValidateBy(int noofTimes, string scenarioName, string containsText, Table table)
        {
            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs_Screen_Submit_Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            foreach (var row in table.Rows)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(1500);

                this.ScenarioId = row["ScenarioId"];

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                if (ContactUsScenario.AgreePrivacyPolicy)
                {
                    IWebElement webContactUsCheckAgreePrivacyPolicy = _driver.FindElement(By.Id("CheckAgreePrivacyPolicy"));

                    SeleniumExtentions.ScrollToView(this._jsDriver, webContactUsCheckAgreePrivacyPolicy);

                    Thread.Sleep(1000);

                    webContactUsCheckAgreePrivacyPolicy.Click();

                    Thread.Sleep(1000);
                }

                Thread.Sleep(100);

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsName"));
                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.Name);

                Thread.Sleep(100);

                IWebElement webContactUsEmailId = _driver.FindElement(By.Id("txtContactUsEmailId"));
                webContactUsEmailId.Clear();
                webContactUsEmailId.SendKeys(ContactUsScenario.EmailId);

                Thread.Sleep(100);

                IWebElement webContactUsSubject = _driver.FindElement(By.Id("txtContactUsSubject"));
                webContactUsSubject.Clear();
                webContactUsSubject.SendKeys(ContactUsScenario.Subject);

                Thread.Sleep(100);

                IWebElement webContactUsMessage = _driver.FindElement(By.Id("txtContactUsMessage"));
                webContactUsMessage.Clear();
                webContactUsMessage.SendKeys(ContactUsScenario.Message);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                if (this._driver != null)
                {
                    var testResultsFolder = Directory.GetCurrentDirectory();
                    string filename = this.ScenarioInfoTitle.Replace(" ", "_") + "_" + this.ScenarioInfoEnvironment + "_" + "beforesubmit";
                    SeleniumExtentions.TakesScreenshot(this._driver, testResultsFolder, filename);
                }

                Thread.Sleep(1000);

                contactSupportBtn.Click();
            }
        }

        [Then(@"Validate mailBox by To, subject, Message for ContactUs Screens")]
        public void ThenValidateMailBoxByToSubjectMessageForContactUsScreens()
        {
            Thread.Sleep(9000);

            var emailDetails = MailBoxExtentions.SearchByEmailSubject(this.MailBoxEmailId, this.MailBoxPassword, ContactUsScenario.Subject);

            Assert.AreEqual(ContactUsScenario.Subject, emailDetails.Subject, "Email subject mismatch for Scenario Id : " + this.ScenarioId);

            Assert.IsTrue(emailDetails.HtmlBody.Contains(ContactUsScenario.Message.Trim()), "Email Message mismatch for Scenario Id : " + this.ScenarioId);

            var addresses = emailDetails.To.OfType<MailboxAddress>().ToList();

            var mailBoxEmailIdExists = addresses.Any(x => x.Address.Contains(this.MailBoxEmailId));
            var supportBoxEmailIdExists = addresses.Any(x => x.Address.Contains(this.MailBoxEmailId));

            Assert.IsTrue(mailBoxEmailIdExists, "Email Address validation failed for Scenario Id : " + this.ScenarioId);
            Assert.IsTrue(supportBoxEmailIdExists, "Email Address validation failed for Scenario Id : " + this.ScenarioId);
        }

        #endregion 02_30 ContactUs Screen Valid Submit - Positive

        #region 02_31 ContactUs Screen Valid Submit - Negative

        [Then(@"Enter Valid data for (.*) times in the screen for scenario (.*), '(.*)' And Click submit button And Validate by (.*)")]
        public void ThenEnterValidDataForTimesInTheScreenForScenarioAndClickSubmitButtonAndValidateBy(int noofTimes, string scenarioName, string scenarioId, string containsText)
        {
            this.ScenarioName = scenarioName;

            List<ContactUsScreenModel> contactUsScreenData = new List<ContactUsScreenModel>();

            if (this.ScenarioName == "ContactUs_Screen_Submit_Negative")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Negative();
            }
            else if (this.ScenarioName == "ContactUs_Screen_Submit_Positive")
            {
                contactUsScreenData = ContactUsScreenData.ContactUs_Screen_Submit_Positive();
            }

            for (int i = 0; i < noofTimes; i++)
            {
                _driver.Navigate().GoToUrl(AppURL);
                _jsDriver = (IJavaScriptExecutor)_driver;

                Thread.Sleep(2500);

                this.ScenarioId = scenarioId;

                ContactUsScenario = contactUsScreenData.Where(x => x.ScenarioId == this.ScenarioId).FirstOrDefault();

                if (ContactUsScenario.AgreePrivacyPolicy)
                {
                    IWebElement webContactUsCheckAgreePrivacyPolicy = _driver.FindElement(By.Id("CheckAgreePrivacyPolicy"));

                    SeleniumExtentions.ScrollToView(this._jsDriver, webContactUsCheckAgreePrivacyPolicy);

                    Thread.Sleep(1000);

                    webContactUsCheckAgreePrivacyPolicy.Click();

                    Thread.Sleep(1000);
                }

                Thread.Sleep(100);

                IWebElement webContactUsName = _driver.FindElement(By.Id("txtContactUsName"));
                webContactUsName.Clear();
                webContactUsName.SendKeys(ContactUsScenario.Name);

                Thread.Sleep(100);

                IWebElement webContactUsEmailId = _driver.FindElement(By.Id("txtContactUsEmailId"));
                webContactUsEmailId.Clear();
                webContactUsEmailId.SendKeys(ContactUsScenario.EmailId);

                Thread.Sleep(100);

                IWebElement webContactUsSubject = _driver.FindElement(By.Id("txtContactUsSubject"));
                webContactUsSubject.Clear();
                webContactUsSubject.SendKeys(ContactUsScenario.Subject);

                Thread.Sleep(100);

                IWebElement webContactUsMessage = _driver.FindElement(By.Id("txtContactUsMessage"));
                webContactUsMessage.Clear();
                webContactUsMessage.SendKeys(ContactUsScenario.Message);

                IWebElement contactSupportBtn = _driver.FindElement(By.Id("btnContactUs"));

                SeleniumExtentions.ScrollToView(_jsDriver, contactSupportBtn);

                Thread.Sleep(1000);

                contactSupportBtn.Click();

                Thread.Sleep(1000);
            }

            var htmlData = _driver.FindElement(By.TagName("body")).Text;

            var errorMessageContains = htmlData.Contains(containsText);

            var txtUserIpAddress = _driver.FindElement(By.Id("txtUserIpAddress"));

            SeleniumExtentions.ScrollToView(this._jsDriver, txtUserIpAddress);

            Thread.Sleep(1000);

            string ipAddress = txtUserIpAddress.Text.Trim();

            Thread.Sleep(1000);

            int emailCount = this._sqlConnection.ExecuteScalarSql<int>("SELECT COUNT(*) FROM [dbo].[LogContactUs] WHERE [CreatedIpAddress] = @ipAddress", new { ipAddress = ipAddress });

            Assert.IsTrue(emailCount >= 3, "Contact us email count validation failed for Scenario Id : " + this.ScenarioId);

            Assert.IsTrue(errorMessageContains, "Contact us validation failed for Scenario Id : " + this.ScenarioId);
        }

        #endregion 02_31 ContactUs Screen Valid Submit - Negative

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