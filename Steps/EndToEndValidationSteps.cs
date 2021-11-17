using HtmlAgilityPack;
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
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;

namespace InfinityMatrix.Niraiya.UITests.Steps
{
    [Binding]
    public class EndToEndValidationSteps : IDisposable
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

        private string NewEmailAccount_EmailId = string.Empty;
        private string NewEmailAccount_Name = string.Empty;
        private string NewEmailAccount_Password = string.Empty;
        private string NewEmailAccount_ChangeName = string.Empty;

        private MimeMessage EmailMimeMessage;
        private MimeMessage EmailMimeMessagePassRecKey;
        private MimeMessage EmailMimeMessageForgetPass;

        public EndToEndValidationSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
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

        [Given(@"Get new user details for registration from AppSetting")]
        public void GivenGetNewUserDetailsForRegistrationFromAppSetting()
        {
            this.NewEmailAccount_EmailId = _config["NewEmailAccount:EmailId"];
            this.NewEmailAccount_Name = _config["NewEmailAccount:Name"];
            this.NewEmailAccount_Password = _config["NewEmailAccount:Password"];
        }

        [Given(@"Clear Database records for new user registration from AppSetting")]
        public void GivenClearDatabaseRecordsForNewUserRegistrationFromAppSetting()
        {
            string checkSQLConnectionByDBName = this._sqlConnection.ExecuteScalarSql<string>("SELECT db_name()").Trim();

            string dbNameExpected = _config["DatabaseSetting:DatabaseName"].Trim();

            Assert.AreEqual(dbNameExpected, checkSQLConnectionByDBName, "SQL connection failed for scenario Id : " + this.ScenarioId.ToString());

            this._sqlConnection.ExecuteSql("DELETE FROM [dbo].[User] WHERE [EmailId] = @EmailId", new { EmailId = this.NewEmailAccount_EmailId });

            Thread.Sleep(1000);
        }

        [Given(@"Clear all the emails from Mail box for '(.*)' is '(.*)' from AppSetting")]
        public void GivenClearAllTheEmailsFromMailBoxForIsFromAppSetting(string emailIdAppSetting, string passwordAppSetting)
        {
            this.MailBoxEmailId = _config[emailIdAppSetting];
            this.MailBoxPassword = _config[passwordAppSetting];

            MailBoxExtentions.DeleteAllEmails(this.MailBoxEmailId, this.MailBoxPassword);

            Thread.Sleep(2000);
        }

        [Given(@"Setup the browser (.*) for device (.*)")]
        public void GivenSetupTheBrowserForDevice(string browserEnvironment, string device)
        {
            this.BrowserEnvironment = browserEnvironment;
            this.Device = device;
        }

        [Given(@"Get and load SignUp page URL to browser from AppSetting '(.*)' and page loaded with in (.*) seconds")]
        public void GivenGetAndLoadSignUpPageURLToBrowserFromAppSettingAndPageLoadedWithInSeconds(string signUpSettings, int seconds)
        {
            this.AppURL = _config["App-URL"] + _config[signUpSettings];
            this.ApplicationStorageName = _config["ApplicationStorageName"];

            PageLoadTimer = new Stopwatch();
            PageLoadTimer.Start();

            var data = SeleniumExtentions.LoadPage(this.BrowserEnvironment, this.Device, this._chromeOptions, this._firefoxOptions, this._driver, this._jsDriver, _config, this.AppURL);

            this._chromeOptions = data.chromeOptions;
            this._firefoxOptions = data.firefoxOptions;
            this._driver = data.driver;
            this._jsDriver = data.jsDriver;

            PageLoadTimer.Stop();

            TimeSpan ts = PageLoadTimer.Elapsed;

            Assert.IsTrue(seconds > ts.Seconds, "Page load time exceeds given value");
        }

        [Given(@"Accept cookie button is clicked in Static Page")]
        public void GivenAcceptCookieButtonIsClickedInStaticPage()
        {
            Thread.Sleep(2000);

            var element = _driver.FindElement(By.Id("alertBtnAcceptCookie"));

            element.Click();

            Thread.Sleep(1000);
        }

        [Given(@"We were able to set the Screen color mode from AppSetting")]
        public void GivenWeWereAbleToSetTheScreenColorModeFromAppSetting()
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

        [Given(@"Local storage value for theme '(.*)' is '(.*)'")]
        public void GivenLocalStorageValueForThemeIs(string localStorage, string expectdValue)
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

        #region 05_10 End to end positive - New user Sign Up

        [When(@"Enter user details from AppSetting to SignUp page and click submit")]
        public void WhenEnterUserDetailsFromAppSettingToSignUpPageAndClickSubmit()
        {
            IWebElement webCheckAgreePrivacyPolicy = _driver.FindElement(By.Id("checkSignUpAgreePrivacy"));

            webCheckAgreePrivacyPolicy.Click();

            Thread.Sleep(500);

            IWebElement checkSignUpNiraiya = _driver.FindElement(By.Id("checkSignUpNiraiya"));

            checkSignUpNiraiya.Click();

            Thread.Sleep(500);

            IWebElement txtsignUpEmailId = _driver.FindElement(By.Id("txtsignUpEmailId"));
            txtsignUpEmailId.Clear();
            txtsignUpEmailId.SendKeys(this.NewEmailAccount_EmailId);

            Thread.Sleep(500);

            IWebElement txtsignUpUserName = _driver.FindElement(By.Id("txtsignUpUserName"));
            txtsignUpUserName.Clear();
            txtsignUpUserName.SendKeys(this.NewEmailAccount_Name);

            Thread.Sleep(500);

            IWebElement txtsignUpPassword = _driver.FindElement(By.Id("txtsignUpPassword"));
            txtsignUpPassword.Clear();
            txtsignUpPassword.SendKeys(this.NewEmailAccount_Password);

            Thread.Sleep(500);

            IWebElement txtsignUpConfirmPassword = _driver.FindElement(By.Id("txtsignUpConfirmPassword"));
            txtsignUpConfirmPassword.Clear();
            txtsignUpConfirmPassword.SendKeys(this.NewEmailAccount_Password);

            Thread.Sleep(500);

            IWebElement btnSubmitSignUp = _driver.FindElement(By.Id("btnSubmitSignUp"));

            SeleniumExtentions.ScrollToView(_jsDriver, btnSubmitSignUp);

            Thread.Sleep(1000);

            btnSubmitSignUp.Click();

            Thread.Sleep(6000);
        }

        [Then(@"Validate SignUp page details in screen for sucessfull response")]
        public void ThenValidateSignUpPageDetailsInScreenForSucessfullResponse()
        {
            //if (this.BrowserEnvironment != "firefox")
            //{
            //    var htmlData = _driver.FindElement(By.TagName("body")).Text;

            //    Thread.Sleep(1000);

            //    string containsPopUpText = this.NewEmailAccount_EmailId + " - Email address registered successfully.";

            //    var contains = htmlData.Contains(containsPopUpText);

            //    Assert.IsTrue(contains, "Signup page  sucessfull response Validation failed");
            //}
        }

        [Then(@"Validate Application auto navigated to Email Pending Verification page from AppSetting '(.*)'")]
        public void ThenValidateApplicationAutoNavigatedToEmailPendingVerificationPageFromAppSetting(string emailPendingVerificationSettings)
        {
            Thread.Sleep(8000);

            string expectdUrl = _config["App-URL"] + _config[emailPendingVerificationSettings];

            String currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(expectdUrl, currentURL, "Email Pending Verification page not redirected");

            string expectdText1 = "Email Pending Verification!";

            IWebElement element1 = _driver.FindElement(By.Id("lblEmailPendingVerification"));

            Thread.Sleep(1000);

            string actualText1 = element1.Text;

            Assert.AreEqual(expectdText1, actualText1, "Email Pending Verification! text mismatch");

            string expectdText2 = "Your email address is pending verification.";

            IWebElement element2 = _driver.FindElement(By.Id("lblYourEmailAddressPendingVerification."));

            Thread.Sleep(1000);

            string actualText2 = element2.Text;

            Assert.AreEqual(expectdText2, actualText2, "Your email address is pending verification. text mismatch");
        }

        [Then(@"Validate latest SignUp Email from Mail box for '(.*)' is '(.*)' from AppSetting")]
        public void ThenValidateLatestSignUpEmailFromMailBoxForIsFromAppSetting(string emailIdAppSetting, string passwordAppSetting)
        {
            this.MailBoxEmailId = _config[emailIdAppSetting];
            this.MailBoxPassword = _config[passwordAppSetting];

            this.EmailMimeMessage = MailBoxExtentions.SearchLatestEmailBySubject(this.MailBoxEmailId, this.MailBoxPassword,
                "Niraiya.com - SignUp Email Verification");

            Assert.IsNotNull(EmailMimeMessage, "Email not available to " + this.MailBoxEmailId);
        }

        [Then(@"Open the email as HTML in new tab and click the activate your account button")]
        public void ThenOpenTheEmailAsHTMLInNewTabAndClickTheActivateYourAccountButton()
        {
            this._jsDriver.ExecuteScript("window.open();");

            Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[1]);

            string htmlBody = this.EmailMimeMessage.HtmlBody;

            this._jsDriver.ExecuteScript("javascript:document.open('text/html');");

            Thread.Sleep(1000);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlBody);

            var htmlBody1 = htmlDoc.DocumentNode.SelectSingleNode("//body");

            string htmlBody2 = Regex.Replace(htmlBody1.InnerHtml.ToString(), @"\t|\n|\r", "");
            string htmlBody3 = htmlBody2.Replace("'", "\"");

            var executeScript = "document.write('" + htmlBody3 + "');";

            this._jsDriver.ExecuteScript(executeScript);

            Thread.Sleep(1000);

            IWebElement WebElementLink;

            var elements = _driver.FindElements(By.XPath("//*[@href]"));

            string linkSearch = _config["App-URL"] + _config["URLAuth:VerifyEmail-URL"];

            foreach (var item in elements)
            {
                string link = item.GetAttribute("href");

                if (link.ToLower().Contains(linkSearch.ToLower()))
                {
                    WebElementLink = item;
                    Thread.Sleep(1000);

                    WebElementLink.Click();
                    break;
                }
            }

            Thread.Sleep(1000);
        }

        [Then(@"New tab will be opened and total tab count (.*) and navigate to (.*)rd tab")]
        public void ThenNewTabWillBeOpenedAndTotalTabCountAndNavigateToRdTab(int totalTabCountExpected, int moveToTab)
        {
            int totalTabCountActual = _driver.WindowHandles.Count;

            Thread.Sleep(1000);

            Assert.AreEqual(totalTabCountExpected, totalTabCountActual, "total tab count mismatch");

            _driver.SwitchTo().Window(_driver.WindowHandles[moveToTab - 1]);

            Thread.Sleep(1000);
        }

        [Then(@"Validate Email address Verified Successfully page from AppSetting '(.*)' and content")]
        public void ThenValidateEmailAddressVerifiedSuccessfullyPageFromAppSettingAndContent(string emailVerifiedURL)
        {
            string expectdUrl = _config["App-URL"] + _config[emailVerifiedURL];

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(expectdUrl, currentURL, "Email address Verified Successfully not redirected");

            string expectdText1 = "Email address Verified Successfully!";

            IWebElement element1 = _driver.FindElement(By.Id("lblEmailVerifiedHeader"));

            Thread.Sleep(1000);

            string actualText1 = element1.Text;

            Assert.AreEqual(expectdText1, actualText1, "Email address Verified Successfully! text mismatch");

            string expectdText2 = "Your email address " + this.MailBoxEmailId + " verified successfully.";

            IWebElement element2 = _driver.FindElement(By.Id("lblEmailVerified"));

            Thread.Sleep(1000);

            string actualText2 = element2.Text;

            Assert.AreEqual(expectdText2, actualText2, "Email address Verified Successfully with mail id - text mismatch");
        }

        [Then(@"Goto (.*)st tab and close other (.*) tabs")]
        public void ThenGotoStTabAndCloseOtherTabs(int p0, int p1)
        {
            //_driver.SwitchTo().Window(_driver.WindowHandles[3]);
            //Thread.Sleep(1000);
            //_driver.Close();
            //Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[2]);
            Thread.Sleep(1000);
            _driver.Close();
            Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
            Thread.Sleep(1000);
            _driver.Close();
            Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[0]);

            _jsDriver = (IJavaScriptExecutor)_driver;
        }

        [Then(@"Validate latest password recovery key Email from Mail box for '(.*)' is '(.*)' from AppSetting")]
        public void ThenValidateLatestPasswordRecoveryKeyEmailFromMailBoxForIsFromAppSetting(string emailIdAppSetting, string passwordAppSetting)
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);

            Thread.Sleep(7000);

            this.MailBoxEmailId = _config[emailIdAppSetting];
            this.MailBoxPassword = _config[passwordAppSetting];

            this.EmailMimeMessagePassRecKey = MailBoxExtentions.SearchLatestEmailBySubject(this.MailBoxEmailId, this.MailBoxPassword,
                "Niraiya.com - Your password recovery key");

            string htmlBody = this.EmailMimeMessagePassRecKey.TextBody;

            this._jsDriver.ExecuteScript("javascript:document.open('text/html');");

            Thread.Sleep(1000);

            htmlBody = Regex.Replace(htmlBody, @"\t|\n|\r", "");

            var executeScript = "document.write('" + htmlBody + "');";

            this._jsDriver.ExecuteScript(executeScript);

            Thread.Sleep(1000);

            Assert.IsNotNull(EmailMimeMessage, "Password recovery key Email not send to " + this.MailBoxEmailId);
        }

        [Then(@"Open SignIn URL from AppSetting '(.*)' and Enter user details from AppSetting and click submit button")]
        public void ThenOpenSignInURLFromAppSettingAndEnterUserDetailsFromAppSettingAndClickSubmitButton(string signInURLAppSettings)
        {
            this.NewEmailAccount_EmailId = _config["NewEmailAccount:EmailId"];
            this.NewEmailAccount_Name = _config["NewEmailAccount:Name"];
            this.NewEmailAccount_Password = _config["NewEmailAccount:Password"];

            this.AppURL = _config["App-URL"] + _config[signInURLAppSettings];

            _driver.Navigate().GoToUrl(AppURL);

            Thread.Sleep(3000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "Sign in page not loaded");

            IWebElement txtLoginEmailId = _driver.FindElement(By.Id("txtLoginEmailId"));
            txtLoginEmailId.Clear();
            txtLoginEmailId.SendKeys(this.NewEmailAccount_EmailId);

            Thread.Sleep(500);

            IWebElement txtLoginPassword = _driver.FindElement(By.Id("txtLoginPassword"));
            txtLoginPassword.Clear();
            txtLoginPassword.SendKeys(this.NewEmailAccount_Password);

            Thread.Sleep(500);

            IWebElement btnSubmitSignUp = _driver.FindElement(By.Id("btnSubmitSignIn"));

            btnSubmitSignUp.Click();

            Thread.Sleep(15000);
        }

        [Then(@"Validate screen navigated to NiraiyaAccounts page from AppSetting '(.*)'")]
        public void ThenValidateScreenNavigatedToNiraiyaAccountsPageFromAppSetting(string niraiyaAccountsURLAppSettings)
        {
            this.AppURL = _config["App-URL"] + _config[niraiyaAccountsURLAppSettings];

            Thread.Sleep(3000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "NiraiyaAccounts page not loaded");
        }

        [Then(@"Click Add Linked Account card and validate popup is opened")]
        public void ThenClickAddLinkedAccountCardAndValidatePopupIsOpened()
        {
            if (this.Device == "Desktop")
            {
                IWebElement element1 = _driver.FindElement(By.Id("addLinkedAccountCardBig"));
                element1.Click();
                Thread.Sleep(1500);
            }
            else
            {
                IWebElement element1 = _driver.FindElement(By.Id("addLinkedAccountCardSmall"));
                element1.Click();
                Thread.Sleep(1500);
            }

            IWebElement element2 = _driver.FindElement(By.Id("addLinkedAccountPartialViewModal"));
            Thread.Sleep(1500);
            var addLinkedAccountPartialViewModal = element2.Displayed;

            Assert.IsTrue(addLinkedAccountPartialViewModal, "Add Linked Account partial view not displayed");
        }

        [Then(@"Click close button at popup bottom of Add Linked Account popup and validate popup is closed")]
        public void ThenClickCloseButtonAtPopupBottomOfAddLinkedAccountPopupAndValidatePopupIsClosed()
        {
            IWebElement btnCancelAddNiraiyaAcc = _driver.FindElement(By.Id("btnCancelAddNiraiyaAcc"));
            btnCancelAddNiraiyaAcc.Click();
            Thread.Sleep(1500);

            var isPopUpClosed = SeleniumExtentions.IsElementPresent(_driver, By.Id("addLinkedAccountPartialViewModal"));

            Assert.IsFalse(isPopUpClosed, "Add Linked Account partial view not not closed");
        }

        [Then(@"Click close button at popup top of Add Linked Account popup and validate popup is closed")]
        public void ThenClickCloseButtonAtPopupTopOfAddLinkedAccountPopupAndValidatePopupIsClosed()
        {
            IWebElement iconCancelAddLinkedAcc = _driver.FindElement(By.Id("iconCancelAddLinkedAcc"));
            iconCancelAddLinkedAcc.Click();
            Thread.Sleep(1500);

            var isPopUpClosed = SeleniumExtentions.IsElementPresent(_driver, By.Id("addLinkedAccountPartialViewModal"));

            Assert.IsFalse(isPopUpClosed, "Add Linked Account partial view not not closed");
        }

        [Then(@"Navigate to LoginAndSecurity via menu bar link and validate LoginAndSecurity page from AppSetting '(.*)'")]
        public void ThenNavigateToLoginAndSecurityViaMenuBarLinkAndValidateLoginAndSecurityPageFromAppSetting(string loginAndSecurityURL)
        {
            if (this.Device != "Desktop")
            {
                SeleniumExtentions.ScrollTo(this._jsDriver);

                Thread.Sleep(1000);

                var element11 = _driver.FindElement(By.XPath("//button[@class ='navbar-toggler']"));
                element11.Click();
                Thread.Sleep(3000);
            }

            IWebElement nav_item_LoginAndSecurity = _driver.FindElement(By.Id("nav-item-LoginAndSecurity"));
            nav_item_LoginAndSecurity.Click();
            Thread.Sleep(3000);

            this.AppURL = _config["App-URL"] + _config[loginAndSecurityURL];

            Thread.Sleep(1000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "loginAndSecurityURL page not loaded");
        }

        [Then(@"Navigate to NiraiyaAccounts via menu bar link and validate NiraiyaAccounts page from AppSetting '(.*)'")]
        public void ThenNavigateToNiraiyaAccountsViaMenuBarLinkAndValidateNiraiyaAccountsPageFromAppSetting(string niraiyaAccountsURLAppSettings)
        {
            if (this.Device != "Desktop")
            {
                var element11 = _driver.FindElement(By.XPath("//button[@class ='navbar-toggler']"));
                element11.Click();
                Thread.Sleep(1000);
            }

            IWebElement manageEmailAccounts = _driver.FindElement(By.Id("nav-item-ManageEmailAccounts"));
            manageEmailAccounts.Click();
            Thread.Sleep(3000);

            this.AppURL = _config["App-URL"] + _config[niraiyaAccountsURLAppSettings];

            Thread.Sleep(1000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "NiraiyaAccounts page not loaded");
        }

        [Then(@"Navigate to LoginAndSecurity via menu bar button and validate LoginAndSecurity page from AppSetting '(.*)'")]
        public void ThenNavigateToLoginAndSecurityViaMenuBarButtonAndValidateLoginAndSecurityPageFromAppSetting(string loginAndSecurityURL)
        {
            if (this.Device != "Desktop")
            {
                var element11 = _driver.FindElement(By.XPath("//button[@class ='navbar-toggler']"));
                element11.Click();
                Thread.Sleep(1000);
            }

            IWebElement nav_item_LoginAndSecurity = _driver.FindElement(By.Id("btnLoggedInUserNameMenuBar"));
            nav_item_LoginAndSecurity.Click();
            Thread.Sleep(3000);

            this.AppURL = _config["App-URL"] + _config[loginAndSecurityURL];

            Thread.Sleep(1000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "loginAndSecurityURL page not loaded");
        }

        [Then(@"Validate LoginAndSecurity page field values from AppSetting")]
        public void ThenValidateLoginAndSecurityPageFieldValuesFromAppSetting()
        {
            string userNameActual = _driver.FindElement(By.Id("txtUserName")).Text;
            Thread.Sleep(1000);

            Assert.AreEqual(this.NewEmailAccount_Name, userNameActual, "User name mismatch");

            string emailActual = _driver.FindElement(By.Id("txtEmailId")).Text;
            Thread.Sleep(1000);

            Assert.AreEqual(this.NewEmailAccount_EmailId, emailActual, "EmailId mismatch");
        }

        [Then(@"Open Edit User Name popup by click edit button in LoginAndSecurity page")]
        public void ThenOpenEditUserNamePopupByClickEditButtonInLoginAndSecurityPage()
        {
            _driver.FindElement(By.Id("btnGetEditUserNamePartialView")).Click();

            Thread.Sleep(2000);

            IWebElement editUserNamePartialViewModal = _driver.FindElement(By.Id("editUserNamePartialViewModal"));
            Thread.Sleep(1500);
            var editUserNamePartialViewModalExists = editUserNamePartialViewModal.Displayed;

            Assert.IsTrue(editUserNamePartialViewModalExists, "Edit User Name popup is not displayed");
        }

        [Then(@"Click close Edit User Name popup button and check popup is closed")]
        public void ThenClickCloseEditUserNamePopupButtonAndCheckPopupIsClosed()
        {
            IWebElement btnCloseEditUserName = _driver.FindElement(By.Id("btnCloseEditUserName"));
            btnCloseEditUserName.Click();
            Thread.Sleep(1500);

            var isPopUpClosed = SeleniumExtentions.IsElementPresent(_driver, By.Id("editUserNamePartialViewModal"));

            Assert.IsFalse(isPopUpClosed, "Edit User Name popup is not closed");

            Thread.Sleep(1000);
        }

        #endregion 05_10 End to end positive - New user Sign Up

        #region 05_20 End to end positive - Login And Security Change Name

        [Then(@"User name in Edit User Name popup from AppSetting '(.*)' and click save")]
        public void ThenUserNameInEditUserNamePopupFromAppSettingAndClickSave(string usernameAppSet)
        {
            this.NewEmailAccount_ChangeName = _config[usernameAppSet];

            IWebElement txteditUserName = _driver.FindElement(By.Id("txteditUserName"));
            txteditUserName.Clear();
            txteditUserName.SendKeys(this.NewEmailAccount_ChangeName);

            Thread.Sleep(1500);

            IWebElement btnEditUserName = _driver.FindElement(By.Id("btnEditUserName"));

            btnEditUserName.Click();

            Thread.Sleep(10000);
        }

        [Then(@"Validate screen navigated to Home page from AppSetting")]
        public void ThenValidateScreenNavigatedToHomePageFromAppSetting()
        {
            this.AppURL = _config["App-URL"];

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "Home page not navigated");
        }

        [Then(@"Accept cookie button is clicked in Static Pages")]
        public void ThenAcceptCookieButtonIsClickedInStaticPages()
        {
            Thread.Sleep(2500);

            var element = _driver.FindElement(By.Id("alertBtnAcceptCookie"));

            element.Click();

            Thread.Sleep(1000);
        }

        [Then(@"Validate LoginAndSecurity page new field values from AppSetting")]
        public void ThenValidateLoginAndSecurityPageNewFieldValuesFromAppSetting()
        {
            string userNameActual = _driver.FindElement(By.Id("txtUserName")).Text;
            Thread.Sleep(1000);

            Assert.AreEqual(this.NewEmailAccount_ChangeName, userNameActual, "new User name mismatch");

            string emailActual = _driver.FindElement(By.Id("txtEmailId")).Text;
            Thread.Sleep(1000);

            Assert.AreEqual(this.NewEmailAccount_EmailId, emailActual, "EmailId mismatch");
        }

        #endregion 05_20 End to end positive - Login And Security Change Name

        #region 05_30 Recover forget password positive

        [Given(@"Open SignIn URL from AppSetting '(.*)' and click on forget password button")]
        public void GivenOpenSignInURLFromAppSettingAndClickOnForgetPasswordButton(string signInURLAppSettings)
        {
            this.NewEmailAccount_EmailId = _config["NewEmailAccount:EmailId"];
            this.NewEmailAccount_Name = _config["NewEmailAccount:Name"];
            this.NewEmailAccount_Password = _config["NewEmailAccount:Password"];

            this.AppURL = _config["App-URL"] + _config[signInURLAppSettings];

            _driver.Navigate().GoToUrl(AppURL);

            Thread.Sleep(3000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "Sign in page not loaded");

            var linkForgetPassword = _driver.FindElement(By.Id("linkForgetPassword"));

            linkForgetPassword.Click();

            Thread.Sleep(2500);
        }

        [Then(@"Enter SignUp Email from NewEmailAccount from AppSetting and click send email")]
        public void ThenEnterSignUpEmailFromNewEmailAccountFromAppSettingAndClickSendEmail()
        {
            this.NewEmailAccount_EmailId = _config["NewEmailAccount:EmailId"];
            this.NewEmailAccount_Name = _config["NewEmailAccount:Name"];
            this.NewEmailAccount_Password = _config["NewEmailAccount:Password"];

            IWebElement txtForgetPasswordEmailId = _driver.FindElement(By.Id("txtForgetPasswordEmailId"));
            txtForgetPasswordEmailId.Clear();
            txtForgetPasswordEmailId.SendKeys(this.NewEmailAccount_EmailId);

            Thread.Sleep(1000);

            var btnSendResetEmail = _driver.FindElement(By.Id("btnSendResetEmail"));

            btnSendResetEmail.Click();

            Thread.Sleep(15000);
        }

        [Then(@"Validate latest Forget Password Request Email from Mail box for '(.*)' is '(.*)' from AppSetting")]
        public void ThenValidateLatestForgetPasswordRequestEmailFromMailBoxForIsFromAppSetting(string emailIdAppSetting, string passwordAppSetting)
        {
            this.MailBoxEmailId = _config[emailIdAppSetting];
            this.MailBoxPassword = _config[passwordAppSetting];

            this.EmailMimeMessageForgetPass = MailBoxExtentions.SearchLatestEmailBySubject(this.MailBoxEmailId, this.MailBoxPassword,
                "Niraiya.com - Forget Password Request Raised");

            Assert.IsNotNull(EmailMimeMessageForgetPass, "Email not available to " + this.MailBoxEmailId);
        }

        [Then(@"Open the email as HTML in new tab and click the complete your request button")]
        public void ThenOpenTheEmailAsHTMLInNewTabAndClickTheCompleteYourRequestButton()
        {
            this._jsDriver.ExecuteScript("window.open();");

            Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[1]);

            string htmlBody = this.EmailMimeMessageForgetPass.HtmlBody;

            this._jsDriver.ExecuteScript("javascript:document.open('text/html');");

            Thread.Sleep(1000);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlBody);

            var htmlBody1 = htmlDoc.DocumentNode.SelectSingleNode("//body");

            string htmlBody2 = Regex.Replace(htmlBody1.InnerHtml.ToString(), @"\t|\n|\r", "");
            string htmlBody3 = htmlBody2.Replace("'", "\"");

            var executeScript = "document.write('" + htmlBody3 + "');";

            this._jsDriver.ExecuteScript(executeScript);

            Thread.Sleep(1000);

            IWebElement WebElementLink;

            var elements = _driver.FindElements(By.XPath("//*[@href]"));

            string linkSearch = _config["App-URL"] + _config["URLAuth:ForgetPassVerify-URL"];

            foreach (var item in elements)
            {
                string link = item.GetAttribute("href");

                if (link.ToLower().Contains(linkSearch.ToLower()))
                {
                    WebElementLink = item;
                    Thread.Sleep(1000);

                    WebElementLink.Click();
                    break;
                }
            }

            Thread.Sleep(3000);
        }

        [Then(@"Goto (.*)rd tab to verify")]
        public void ThenGotoRdTabToVerify(int tab)
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[tab - 1]);
            Thread.Sleep(1500);
        }

        [Then(@"Validate Forget Pass Verified Successfully page redirect to '(.*)' from and its content")]
        public void ThenValidateForgetPassVerifiedSuccessfullyPageRedirectToFromAndItsContent(string recoverPasswordURL)
        {
            this.AppURL = _config["App-URL"] + _config[recoverPasswordURL];

            Thread.Sleep(3000);

            string currentURL = _driver.Url;

            Thread.Sleep(1000);

            Assert.AreEqual(this.AppURL, currentURL, "Password recovery page not loaded page not loaded");

            var lblAreaRecoveryKey = _driver.FindElement(By.Id("lblAreaRecoveryKey"));

            Actions action = new Actions(_driver);

            action.MoveToElement(lblAreaRecoveryKey);

            action.Click();

            Thread.Sleep(1500);

            Assert.AreEqual("Enter or paste your private key to recover password", lblAreaRecoveryKey.Text, "Password recovery page label mismatch");
        }

        [Then(@"Goto (.*)rd tab and close other (.*) tabs")]
        public void ThenGotoRdTabAndCloseOtherTabs(int p0, int p1)
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            Thread.Sleep(1000);
            _driver.Close();
            Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[0]);
            Thread.Sleep(1000);
            _driver.Close();
            Thread.Sleep(1000);

            _driver.SwitchTo().Window(_driver.WindowHandles[0]);

            _jsDriver = (IJavaScriptExecutor)_driver;
        }

        [Then(@"Get latest password recovery key Email from Mail box for '(.*)' is '(.*)' from AppSetting")]
        public void ThenGetLatestPasswordRecoveryKeyEmailFromMailBoxForIsFromAppSetting(string emailIdAppSetting, string passwordAppSetting)
        {
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);

            Thread.Sleep(2000);

            this.MailBoxEmailId = _config[emailIdAppSetting];
            this.MailBoxPassword = _config[passwordAppSetting];

            this.EmailMimeMessagePassRecKey = MailBoxExtentions.SearchLatestEmailBySubject(this.MailBoxEmailId, this.MailBoxPassword,
                "Niraiya.com - Your password recovery key, Do not delete or share this email and keep it safe");

            Assert.IsNotNull(this.EmailMimeMessagePassRecKey, "Password recovery key Email not found in " + this.MailBoxEmailId);
        }

        [Then(@"Enter password recovery key Mail to texbox and click decrypt button")]
        public void ThenEnterPasswordRecoveryKeyMailToTexboxAndClickDecryptButton()
        {
            var textareaRecoveryKey = _driver.FindElement(By.Id("textareaRecoveryKey"));

            Thread.Sleep(1000);

            Actions action = new Actions(_driver);

            action.MoveToElement(textareaRecoveryKey);

            action.Perform();

            Thread.Sleep(2000);

            textareaRecoveryKey.Clear();
            textareaRecoveryKey.SendKeys(this.EmailMimeMessagePassRecKey.TextBody);

            var btnDecryptPassword = _driver.FindElement(By.Id("btnDecryptPassword"));

            SeleniumExtentions.ScrollToView(this._jsDriver, btnDecryptPassword);

            Thread.Sleep(1000);

            btnDecryptPassword.Click();

            Thread.Sleep(2500);
        }

        [Then(@"Validate user password wrt to recovered password and click on CopyPassword")]
        public void ThenValidateUserPasswordWrtToRecoveredPasswordAndClickOnCopyPassword()
        {
            this.NewEmailAccount_Password = _config["NewEmailAccount:Password"];

            var recoveredPassword = _driver.FindElement(By.Id("recoveredPassword")).Text;

            Assert.AreEqual(NewEmailAccount_Password, recoveredPassword, "Recovered Password mismatch");

            var btnCopyrecoveredPassword = _driver.FindElement(By.Id("btnCopyrecoveredPassword"));

            Thread.Sleep(1000);

            Actions action = new Actions(_driver);

            action.MoveToElement(btnCopyrecoveredPassword);

            action.Click();
            action.Perform();

            Thread.Sleep(10000);
        }

        #endregion 05_30 Recover forget password positive

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