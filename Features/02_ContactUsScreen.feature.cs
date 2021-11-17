﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace InfinityMatrix.Niraiya.UITests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("02_ContactUsScreen", Description=@"![02_ContactUsScreen]
Link to a feature: [ContactUsScreen](InfinityMatrix.Niraiya.UITests/Features/02_ContactUsScreen.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**", SourceFile="Features\\02_ContactUsScreen.feature", SourceLine=0)]
    public partial class _02_ContactUsScreenFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
#line 1 "02_ContactUsScreen.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "02_ContactUsScreen", @"![02_ContactUsScreen]
Link to a feature: [ContactUsScreen](InfinityMatrix.Niraiya.UITests/Features/02_ContactUsScreen.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void _02_10ScrollToBottomContactUsScreen(string environment, string browser, string device, string sourceURL, string scrollMillisecondDelay, string pageLoadedInSeconds, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ContactUsScreen",
                    "scrollstepbystep",
                    "Parallel"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Environment", environment);
            argumentsOfScenario.Add("Browser", browser);
            argumentsOfScenario.Add("Device", device);
            argumentsOfScenario.Add("SourceURL", sourceURL);
            argumentsOfScenario.Add("ScrollMillisecondDelay", scrollMillisecondDelay);
            argumentsOfScenario.Add("PageLoadedInSeconds", pageLoadedInSeconds);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02_10 Scroll To Bottom ContactUs Screen", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 7
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
 testRunner.Given(string.Format("Get URL from AppSetting to load contactus URL {0}", sourceURL), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 9
 testRunner.When(string.Format("The ContactUs page is loaded for browser {0} for device {1}", browser, device), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 10
 testRunner.Given("Accept cookie button is clicked in contactus page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 11
 testRunner.Then(string.Format("The ContactUs page loaded with in {0} seconds", pageLoadedInSeconds), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 12
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_10_ScrToBottom_pgeload\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 13
 testRunner.And("We were able to get ContactUs page total dimention", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 14
 testRunner.And("We were able to set the ContactUs Screen color mode from AppSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 15
 testRunner.And("ContactUs Local storage value for theme \'theme\' is \'dark\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 16
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_10_ScrToBottom_theme\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 17
 testRunner.And(string.Format("The ContactUs page is scrolled step by step to end by millisecond delay {0}", scrollMillisecondDelay), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_10 Scroll To Bottom ContactUs Screen, chrome-GalaxyS5", new string[] {
                "ContactUsScreen",
                "scrollstepbystep",
                "Parallel"}, SourceLine=20)]
        public virtual void _02_10ScrollToBottomContactUsScreen_Chrome_GalaxyS5()
        {
#line 7
this._02_10ScrollToBottomContactUsScreen("chrome-GalaxyS5", "chrome", "Galaxy S5", "ContactUs", "600", "15", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_10 Scroll To Bottom ContactUs Screen, chrome-iPhoneX", new string[] {
                "ContactUsScreen",
                "scrollstepbystep",
                "Parallel"}, SourceLine=20)]
        public virtual void _02_10ScrollToBottomContactUsScreen_Chrome_IPhoneX()
        {
#line 7
this._02_10ScrollToBottomContactUsScreen("chrome-iPhoneX", "chrome", "iPhone X", "ContactUs", "600", "15", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_10 Scroll To Bottom ContactUs Screen, chrome-iPad", new string[] {
                "ContactUsScreen",
                "scrollstepbystep",
                "Parallel"}, SourceLine=20)]
        public virtual void _02_10ScrollToBottomContactUsScreen_Chrome_IPad()
        {
#line 7
this._02_10ScrollToBottomContactUsScreen("chrome-iPad", "chrome", "iPad", "ContactUs", "600", "15", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_10 Scroll To Bottom ContactUs Screen, chrome-Desktop", new string[] {
                "ContactUsScreen",
                "scrollstepbystep",
                "Parallel"}, SourceLine=20)]
        public virtual void _02_10ScrollToBottomContactUsScreen_Chrome_Desktop()
        {
#line 7
this._02_10ScrollToBottomContactUsScreen("chrome-Desktop", "chrome", "Desktop", "ContactUs", "600", "15", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_10 Scroll To Bottom ContactUs Screen, firefox-GalaxyS5", new string[] {
                "ContactUsScreen",
                "scrollstepbystep",
                "Parallel"}, SourceLine=20)]
        public virtual void _02_10ScrollToBottomContactUsScreen_Firefox_GalaxyS5()
        {
#line 7
this._02_10ScrollToBottomContactUsScreen("firefox-GalaxyS5", "firefox", "Galaxy S5", "ContactUs", "50", "20", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_10 Scroll To Bottom ContactUs Screen, firefox-Desktop", new string[] {
                "ContactUsScreen",
                "scrollstepbystep",
                "Parallel"}, SourceLine=20)]
        public virtual void _02_10ScrollToBottomContactUsScreen_Firefox_Desktop()
        {
#line 7
this._02_10ScrollToBottomContactUsScreen("firefox-Desktop", "firefox", "Desktop", "ContactUs", "50", "20", ((string[])(null)));
#line hidden
        }
        
        public virtual void _02_20ContactUsScreenSubmit_Negative(string environment, string browser, string device, string sourceURL, string scenarioName, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ContactUsScreen",
                    "ContactUsScreenSubmitNegative",
                    "Parallel"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Environment", environment);
            argumentsOfScenario.Add("Browser", browser);
            argumentsOfScenario.Add("Device", device);
            argumentsOfScenario.Add("SourceURL", sourceURL);
            argumentsOfScenario.Add("ScenarioName", scenarioName);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02_20 ContactUs Screen Submit - Negative", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 29
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 30
 testRunner.Given(string.Format("Get URL from AppSetting to load contactus URL {0}", sourceURL), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 31
 testRunner.And(string.Format("The ContactUs page is loaded for browser {0} for device {1}", browser, device), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 32
 testRunner.And("Accept cookie button is clicked in contactus page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 33
 testRunner.Then("We were able to set the ContactUs Screen color mode from AppSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 34
 testRunner.And("ContactUs Local storage value for theme \'theme\' is \'dark\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 35
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_theme\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table1.AddRow(new string[] {
                            "Test Name - Empty",
                            "ContactUs-1.1"});
                table1.AddRow(new string[] {
                            "Test Name - Less Than 6",
                            "ContactUs-1.2"});
#line 36
 testRunner.And(string.Format("Enter Name field data to the screen for scenario {0} And Submit button is clicked" +
                            " And Validate", scenarioName), ((string)(null)), table1, "And ");
#line hidden
#line 40
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_ContactUs-1.2\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table2.AddRow(new string[] {
                            "Test Name - Empty",
                            "ContactUs-1.3"});
                table2.AddRow(new string[] {
                            "Test Name - Less Than 6",
                            "ContactUs-1.4"});
                table2.AddRow(new string[] {
                            "Test Name - Valid Email Less Than 6",
                            "ContactUs-1.5"});
#line 41
 testRunner.And(string.Format("Enter EmailId field data to the screen for scenario {0} And Submit button is clic" +
                            "ked And Validate", scenarioName), ((string)(null)), table2, "And ");
#line hidden
#line 46
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_ContactUs-1.5\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table3 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table3.AddRow(new string[] {
                            "Test Subject - Empty",
                            "ContactUs-1.6"});
                table3.AddRow(new string[] {
                            "Test Subject - Less Than 6",
                            "ContactUs-1.7"});
#line 47
 testRunner.And(string.Format("Enter Subject field data to the screen for scenario {0} And Submit button is clic" +
                            "ked And Validate", scenarioName), ((string)(null)), table3, "And ");
#line hidden
#line 51
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_ContactUs-1.7\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table4.AddRow(new string[] {
                            "Test Message - Empty",
                            "ContactUs-1.8"});
                table4.AddRow(new string[] {
                            "Test Message - Less Than 6",
                            "ContactUs-1.9"});
                table4.AddRow(new string[] {
                            "Test Message - More Than 1000",
                            "ContactUs-1.10"});
#line 52
 testRunner.And(string.Format("Enter Message field data to the screen for scenario {0} And Submit button is clic" +
                            "ked And Validate", scenarioName), ((string)(null)), table4, "And ");
#line hidden
#line 57
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_ContactUs-1.10\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table5.AddRow(new string[] {
                            "AgreePrivacyPolicy - False",
                            "ContactUs-1.11"});
#line 58
 testRunner.And(string.Format("Select AgreePrivacyPolicy field data to the screen for scenario {0} And Submit bu" +
                            "tton is clicked And Validate", scenarioName), ((string)(null)), table5, "And ");
#line hidden
#line 61
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_ContactUs-1.11\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table6.AddRow(new string[] {
                            "Validation Error for Name",
                            "ContactUs-1.12"});
                table6.AddRow(new string[] {
                            "Validation Error for Email",
                            "ContactUs-1.13"});
                table6.AddRow(new string[] {
                            "Validation Error for Subject",
                            "ContactUs-1.14"});
                table6.AddRow(new string[] {
                            "Validation Error for Message 1",
                            "ContactUs-1.15"});
                table6.AddRow(new string[] {
                            "Validation Error for Message 2",
                            "ContactUs-1.16"});
                table6.AddRow(new string[] {
                            "Validation Error for AgreePrivacyPolicy",
                            "ContactUs-1.17"});
#line 62
 testRunner.And(string.Format("Enter 3 fields valid data to the screen for scenario {0} And Submit button is cli" +
                            "cked And Validate", scenarioName), ((string)(null)), table6, "And ");
#line hidden
#line 70
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_20_ContactUs-1.17\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_20 ContactUs Screen Submit - Negative, chrome-GalaxyS5", new string[] {
                "ContactUsScreen",
                "ContactUsScreenSubmitNegative",
                "Parallel"}, SourceLine=73)]
        public virtual void _02_20ContactUsScreenSubmit_Negative_Chrome_GalaxyS5()
        {
#line 29
this._02_20ContactUsScreenSubmit_Negative("chrome-GalaxyS5", "chrome", "Galaxy S5", "ContactUs", "ContactUs_Screen_Submit_Negative", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_20 ContactUs Screen Submit - Negative, chrome-iPhoneX", new string[] {
                "ContactUsScreen",
                "ContactUsScreenSubmitNegative",
                "Parallel"}, SourceLine=73)]
        public virtual void _02_20ContactUsScreenSubmit_Negative_Chrome_IPhoneX()
        {
#line 29
this._02_20ContactUsScreenSubmit_Negative("chrome-iPhoneX", "chrome", "iPhone X", "ContactUs", "ContactUs_Screen_Submit_Negative", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_20 ContactUs Screen Submit - Negative, chrome-iPad", new string[] {
                "ContactUsScreen",
                "ContactUsScreenSubmitNegative",
                "Parallel"}, SourceLine=73)]
        public virtual void _02_20ContactUsScreenSubmit_Negative_Chrome_IPad()
        {
#line 29
this._02_20ContactUsScreenSubmit_Negative("chrome-iPad", "chrome", "iPad", "ContactUs", "ContactUs_Screen_Submit_Negative", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_20 ContactUs Screen Submit - Negative, chrome-Desktop", new string[] {
                "ContactUsScreen",
                "ContactUsScreenSubmitNegative",
                "Parallel"}, SourceLine=73)]
        public virtual void _02_20ContactUsScreenSubmit_Negative_Chrome_Desktop()
        {
#line 29
this._02_20ContactUsScreenSubmit_Negative("chrome-Desktop", "chrome", "Desktop", "ContactUs", "ContactUs_Screen_Submit_Negative", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_20 ContactUs Screen Submit - Negative, firefox-GalaxyS5", new string[] {
                "ContactUsScreen",
                "ContactUsScreenSubmitNegative",
                "Parallel"}, SourceLine=73)]
        public virtual void _02_20ContactUsScreenSubmit_Negative_Firefox_GalaxyS5()
        {
#line 29
this._02_20ContactUsScreenSubmit_Negative("firefox-GalaxyS5", "firefox", "Galaxy S5", "ContactUs", "ContactUs_Screen_Submit_Negative", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_20 ContactUs Screen Submit - Negative, firefox-Desktop", new string[] {
                "ContactUsScreen",
                "ContactUsScreenSubmitNegative",
                "Parallel"}, SourceLine=73)]
        public virtual void _02_20ContactUsScreenSubmit_Negative_Firefox_Desktop()
        {
#line 29
this._02_20ContactUsScreenSubmit_Negative("firefox-Desktop", "firefox", "Desktop", "ContactUs", "ContactUs_Screen_Submit_Negative", ((string[])(null)));
#line hidden
        }
        
        public virtual void _02_30ContactUsScreenValidSubmit_Positive(string environment, string browser, string device, string sourceURL, string scenarioName, string containsText, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ContactUsScreen",
                    "ContactUsScreenValidSubmitPositive",
                    "Sequential"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Environment", environment);
            argumentsOfScenario.Add("Browser", browser);
            argumentsOfScenario.Add("Device", device);
            argumentsOfScenario.Add("SourceURL", sourceURL);
            argumentsOfScenario.Add("ScenarioName", scenarioName);
            argumentsOfScenario.Add("ContainsText", containsText);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02_30 ContactUs Screen Valid Submit - Positive", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 82
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 83
 testRunner.Given(string.Format("Get URL from AppSetting to load contactus URL {0}", sourceURL), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 84
 testRunner.And(string.Format("The ContactUs page is loaded for browser {0} for device {1}", browser, device), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 85
 testRunner.And("Accept cookie button is clicked in contactus page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 86
 testRunner.And("Old Records for IP address are cleared in Database for ContactUs Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 87
 testRunner.When("All Mails for MailBox configured in AppSetting are deleted for ContactUs Screen", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
#line 88
 testRunner.Then("We were able to set the ContactUs Screen color mode from AppSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 89
 testRunner.And("ContactUs Local storage value for theme \'theme\' is \'dark\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "TestName",
                            "ScenarioId"});
                table7.AddRow(new string[] {
                            "Valid",
                            "ContactUs-2.10"});
#line 90
 testRunner.And(string.Format("Enter Valid data for 1 time in the screen for scenario {0} And Click submit butto" +
                            "n And Validate by {1}", scenarioName, containsText), ((string)(null)), table7, "And ");
#line hidden
#line 93
 testRunner.And("Validate mailBox by To, subject, Message for ContactUs Screens", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 94
 testRunner.And("Take ContactUs page screenshot with name suffix \'02_30_ContactUs_aftersubmit\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_30 ContactUs Screen Valid Submit - Positive, chrome-GalaxyS5", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitPositive",
                "Sequential"}, SourceLine=97)]
        public virtual void _02_30ContactUsScreenValidSubmit_Positive_Chrome_GalaxyS5()
        {
#line 82
this._02_30ContactUsScreenValidSubmit_Positive("chrome-GalaxyS5", "chrome", "Galaxy S5", "ContactUs", "ContactUs_Screen_Submit_Positive", "Email successfully sent. Thank you for contacting us.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_30 ContactUs Screen Valid Submit - Positive, chrome-iPhoneX", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitPositive",
                "Sequential"}, SourceLine=97)]
        public virtual void _02_30ContactUsScreenValidSubmit_Positive_Chrome_IPhoneX()
        {
#line 82
this._02_30ContactUsScreenValidSubmit_Positive("chrome-iPhoneX", "chrome", "iPhone X", "ContactUs", "ContactUs_Screen_Submit_Positive", "Email successfully sent. Thank you for contacting us.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_30 ContactUs Screen Valid Submit - Positive, chrome-iPad", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitPositive",
                "Sequential"}, SourceLine=97)]
        public virtual void _02_30ContactUsScreenValidSubmit_Positive_Chrome_IPad()
        {
#line 82
this._02_30ContactUsScreenValidSubmit_Positive("chrome-iPad", "chrome", "iPad", "ContactUs", "ContactUs_Screen_Submit_Positive", "Email successfully sent. Thank you for contacting us.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_30 ContactUs Screen Valid Submit - Positive, chrome-Desktop", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitPositive",
                "Sequential"}, SourceLine=97)]
        public virtual void _02_30ContactUsScreenValidSubmit_Positive_Chrome_Desktop()
        {
#line 82
this._02_30ContactUsScreenValidSubmit_Positive("chrome-Desktop", "chrome", "Desktop", "ContactUs", "ContactUs_Screen_Submit_Positive", "Email successfully sent. Thank you for contacting us.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_30 ContactUs Screen Valid Submit - Positive, firefox-GalaxyS5", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitPositive",
                "Sequential"}, SourceLine=97)]
        public virtual void _02_30ContactUsScreenValidSubmit_Positive_Firefox_GalaxyS5()
        {
#line 82
this._02_30ContactUsScreenValidSubmit_Positive("firefox-GalaxyS5", "firefox", "Galaxy S5", "ContactUs", "ContactUs_Screen_Submit_Positive", "Email successfully sent. Thank you for contacting us.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_30 ContactUs Screen Valid Submit - Positive, firefox-Desktop", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitPositive",
                "Sequential"}, SourceLine=97)]
        public virtual void _02_30ContactUsScreenValidSubmit_Positive_Firefox_Desktop()
        {
#line 82
this._02_30ContactUsScreenValidSubmit_Positive("firefox-Desktop", "firefox", "Desktop", "ContactUs", "ContactUs_Screen_Submit_Positive", "Email successfully sent. Thank you for contacting us.", ((string[])(null)));
#line hidden
        }
        
        public virtual void _02_31ContactUsScreenValidSubmit_Negative(string environment, string browser, string device, string sourceURL, string scenarioName, string containsText, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "ContactUsScreen",
                    "ContactUsScreenValidSubmitNegative",
                    "Parallel"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            string[] tagsOfScenario = @__tags;
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            argumentsOfScenario.Add("Environment", environment);
            argumentsOfScenario.Add("Browser", browser);
            argumentsOfScenario.Add("Device", device);
            argumentsOfScenario.Add("SourceURL", sourceURL);
            argumentsOfScenario.Add("ScenarioName", scenarioName);
            argumentsOfScenario.Add("ContainsText", containsText);
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02_31 ContactUs Screen Valid Submit - Negative", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 106
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 107
 testRunner.Given(string.Format("Get URL from AppSetting to load contactus URL {0}", sourceURL), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
#line 108
 testRunner.And(string.Format("The ContactUs page is loaded for browser {0} for device {1}", browser, device), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 109
 testRunner.And("Accept cookie button is clicked in contactus page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 110
 testRunner.And("Old Records for IP address are cleared in Database for ContactUs Page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 111
 testRunner.Then("We were able to set the ContactUs Screen color mode from AppSetting", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 112
 testRunner.And("ContactUs Local storage value for theme \'theme\' is \'dark\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 113
 testRunner.And(string.Format("Enter Valid data for 4 times in the screen for scenario {0}, \'ContactUs-2.11\' And" +
                            " Click submit button And Validate by {1}", scenarioName, containsText), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_31 ContactUs Screen Valid Submit - Negative, chrome-GalaxyS5", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitNegative",
                "Parallel"}, SourceLine=116)]
        public virtual void _02_31ContactUsScreenValidSubmit_Negative_Chrome_GalaxyS5()
        {
#line 106
this._02_31ContactUsScreenValidSubmit_Negative("chrome-GalaxyS5", "chrome", "Galaxy S5", "ContactUs", "ContactUs_Screen_Submit_Positive", "Your IP address limit reached, please email us to support@niraiya.com directly or" +
                    " try again after a day.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_31 ContactUs Screen Valid Submit - Negative, chrome-iPhoneX", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitNegative",
                "Parallel"}, SourceLine=116)]
        public virtual void _02_31ContactUsScreenValidSubmit_Negative_Chrome_IPhoneX()
        {
#line 106
this._02_31ContactUsScreenValidSubmit_Negative("chrome-iPhoneX", "chrome", "iPhone X", "ContactUs", "ContactUs_Screen_Submit_Positive", "Your IP address limit reached, please email us to support@niraiya.com directly or" +
                    " try again after a day.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_31 ContactUs Screen Valid Submit - Negative, chrome-iPad", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitNegative",
                "Parallel"}, SourceLine=116)]
        public virtual void _02_31ContactUsScreenValidSubmit_Negative_Chrome_IPad()
        {
#line 106
this._02_31ContactUsScreenValidSubmit_Negative("chrome-iPad", "chrome", "iPad", "ContactUs", "ContactUs_Screen_Submit_Positive", "Your IP address limit reached, please email us to support@niraiya.com directly or" +
                    " try again after a day.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_31 ContactUs Screen Valid Submit - Negative, chrome-Desktop", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitNegative",
                "Parallel"}, SourceLine=116)]
        public virtual void _02_31ContactUsScreenValidSubmit_Negative_Chrome_Desktop()
        {
#line 106
this._02_31ContactUsScreenValidSubmit_Negative("chrome-Desktop", "chrome", "Desktop", "ContactUs", "ContactUs_Screen_Submit_Positive", "Your IP address limit reached, please email us to support@niraiya.com directly or" +
                    " try again after a day.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_31 ContactUs Screen Valid Submit - Negative, firefox-GalaxyS5", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitNegative",
                "Parallel"}, SourceLine=116)]
        public virtual void _02_31ContactUsScreenValidSubmit_Negative_Firefox_GalaxyS5()
        {
#line 106
this._02_31ContactUsScreenValidSubmit_Negative("firefox-GalaxyS5", "firefox", "Galaxy S5", "ContactUs", "ContactUs_Screen_Submit_Positive", "Your IP address limit reached, please email us to support@niraiya.com directly or" +
                    " try again after a day.", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("02_31 ContactUs Screen Valid Submit - Negative, firefox-Desktop", new string[] {
                "ContactUsScreen",
                "ContactUsScreenValidSubmitNegative",
                "Parallel"}, SourceLine=116)]
        public virtual void _02_31ContactUsScreenValidSubmit_Negative_Firefox_Desktop()
        {
#line 106
this._02_31ContactUsScreenValidSubmit_Negative("firefox-Desktop", "firefox", "Desktop", "ContactUs", "ContactUs_Screen_Submit_Positive", "Your IP address limit reached, please email us to support@niraiya.com directly or" +
                    " try again after a day.", ((string[])(null)));
#line hidden
        }
    }
}
#pragma warning restore
#endregion