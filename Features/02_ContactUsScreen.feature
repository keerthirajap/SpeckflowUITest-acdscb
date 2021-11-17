Feature: 02_ContactUsScreen
![02_ContactUsScreen]
Link to a feature: [ContactUsScreen](InfinityMatrix.Niraiya.UITests/Features/02_ContactUsScreen.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@ContactUsScreen @scrollstepbystep @Parallel
Scenario: 02_10 Scroll To Bottom ContactUs Screen 
	Given Get URL from AppSetting to load contactus URL <SourceURL>
	When The ContactUs page is loaded for browser <Browser> for device <Device>
	Given Accept cookie button is clicked in contactus page
	Then The ContactUs page loaded with in <PageLoadedInSeconds> seconds
	And Take ContactUs page screenshot with name suffix '02_10_ScrToBottom_pgeload'
	And We were able to get ContactUs page total dimention
	And We were able to set the ContactUs Screen color mode from AppSetting
	And ContactUs Local storage value for theme 'theme' is 'dark'
	And Take ContactUs page screenshot with name suffix '02_10_ScrToBottom_theme'
	And The ContactUs page is scrolled step by step to end by millisecond delay <ScrollMillisecondDelay>

	Examples:
		| Environment      | Browser | Device    | SourceURL | ScrollMillisecondDelay | PageLoadedInSeconds |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | ContactUs | 600                    | 15                  |
		| chrome-iPhoneX   | chrome  | iPhone X  | ContactUs | 600                    | 15                  |
		| chrome-iPad      | chrome  | iPad      | ContactUs | 600                    | 15                  |
		| chrome-Desktop   | chrome  | Desktop   | ContactUs | 600                    | 15                  |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | ContactUs | 50                     | 20                  |
		| firefox-Desktop  | firefox | Desktop   | ContactUs | 50                     | 20                  |

@ContactUsScreen @ContactUsScreenSubmitNegative @Parallel
Scenario: 02_20 ContactUs Screen Submit - Negative
	Given Get URL from AppSetting to load contactus URL <SourceURL>
	And The ContactUs page is loaded for browser <Browser> for device <Device>
	And Accept cookie button is clicked in contactus page
	Then We were able to set the ContactUs Screen color mode from AppSetting
	And ContactUs Local storage value for theme 'theme' is 'dark'
	And Take ContactUs page screenshot with name suffix '02_20_theme'
	And Enter Name field data to the screen for scenario <ScenarioName> And Submit button is clicked And Validate
		| TestName                | ScenarioId    |
		| Test Name - Empty       | ContactUs-1.1 |
		| Test Name - Less Than 6 | ContactUs-1.2 |
	And Take ContactUs page screenshot with name suffix '02_20_ContactUs-1.2'
	And Enter EmailId field data to the screen for scenario <ScenarioName> And Submit button is clicked And Validate
		| TestName                            | ScenarioId    |
		| Test Name - Empty                   | ContactUs-1.3 |
		| Test Name - Less Than 6             | ContactUs-1.4 |
		| Test Name - Valid Email Less Than 6 | ContactUs-1.5 |
	And Take ContactUs page screenshot with name suffix '02_20_ContactUs-1.5'
	And Enter Subject field data to the screen for scenario <ScenarioName> And Submit button is clicked And Validate
		| TestName                   | ScenarioId    |
		| Test Subject - Empty       | ContactUs-1.6 |
		| Test Subject - Less Than 6 | ContactUs-1.7 |
	And Take ContactUs page screenshot with name suffix '02_20_ContactUs-1.7'
	And Enter Message field data to the screen for scenario <ScenarioName> And Submit button is clicked And Validate
		| TestName                      | ScenarioId     |
		| Test Message - Empty          | ContactUs-1.8  |
		| Test Message - Less Than 6    | ContactUs-1.9  |
		| Test Message - More Than 1000 | ContactUs-1.10 |
	And Take ContactUs page screenshot with name suffix '02_20_ContactUs-1.10'
	And Select AgreePrivacyPolicy field data to the screen for scenario <ScenarioName> And Submit button is clicked And Validate
		| TestName                   | ScenarioId     |
		| AgreePrivacyPolicy - False | ContactUs-1.11 |
	And Take ContactUs page screenshot with name suffix '02_20_ContactUs-1.11'
	And Enter 3 fields valid data to the screen for scenario <ScenarioName> And Submit button is clicked And Validate
		| TestName                                | ScenarioId     |
		| Validation Error for Name               | ContactUs-1.12 |
		| Validation Error for Email              | ContactUs-1.13 |
		| Validation Error for Subject            | ContactUs-1.14 |
		| Validation Error for Message 1          | ContactUs-1.15 |
		| Validation Error for Message 2          | ContactUs-1.16 |
		| Validation Error for AgreePrivacyPolicy | ContactUs-1.17 |
	And Take ContactUs page screenshot with name suffix '02_20_ContactUs-1.17'

	Examples:
		| Environment      | Browser | Device    | SourceURL | ScenarioName                     |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | ContactUs | ContactUs_Screen_Submit_Negative |
		| chrome-iPhoneX   | chrome  | iPhone X  | ContactUs | ContactUs_Screen_Submit_Negative |
		| chrome-iPad      | chrome  | iPad      | ContactUs | ContactUs_Screen_Submit_Negative |
		| chrome-Desktop   | chrome  | Desktop   | ContactUs | ContactUs_Screen_Submit_Negative |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | ContactUs | ContactUs_Screen_Submit_Negative |
		| firefox-Desktop  | firefox | Desktop   | ContactUs | ContactUs_Screen_Submit_Negative |

@ContactUsScreen @ContactUsScreenValidSubmitPositive @Sequential
Scenario: 02_30 ContactUs Screen Valid Submit - Positive
	Given Get URL from AppSetting to load contactus URL <SourceURL>
	And The ContactUs page is loaded for browser <Browser> for device <Device>
	And Accept cookie button is clicked in contactus page
	And Old Records for IP address are cleared in Database for ContactUs Page
	When All Mails for MailBox configured in AppSetting are deleted for ContactUs Screen
	Then We were able to set the ContactUs Screen color mode from AppSetting
	And ContactUs Local storage value for theme 'theme' is 'dark'
	And Enter Valid data for 1 time in the screen for scenario <ScenarioName> And Click submit button And Validate by <ContainsText>
		| TestName | ScenarioId     |
		| Valid    | ContactUs-2.10 |
	And Validate mailBox by To, subject, Message for ContactUs Screens
	And Take ContactUs page screenshot with name suffix '02_30_ContactUs_aftersubmit'

	Examples:
		| Environment      | Browser | Device    | SourceURL | ScenarioName                     | ContainsText                                          |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | ContactUs | ContactUs_Screen_Submit_Positive | Email successfully sent. Thank you for contacting us. |
		| chrome-iPhoneX   | chrome  | iPhone X  | ContactUs | ContactUs_Screen_Submit_Positive | Email successfully sent. Thank you for contacting us. |
		| chrome-iPad      | chrome  | iPad      | ContactUs | ContactUs_Screen_Submit_Positive | Email successfully sent. Thank you for contacting us. |
		| chrome-Desktop   | chrome  | Desktop   | ContactUs | ContactUs_Screen_Submit_Positive | Email successfully sent. Thank you for contacting us. |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | ContactUs | ContactUs_Screen_Submit_Positive | Email successfully sent. Thank you for contacting us. |
		| firefox-Desktop  | firefox | Desktop   | ContactUs | ContactUs_Screen_Submit_Positive | Email successfully sent. Thank you for contacting us. |

@ContactUsScreen @ContactUsScreenValidSubmitNegative @Parallel
Scenario: 02_31 ContactUs Screen Valid Submit - Negative
	Given Get URL from AppSetting to load contactus URL <SourceURL>
	And The ContactUs page is loaded for browser <Browser> for device <Device>
	And Accept cookie button is clicked in contactus page
	And Old Records for IP address are cleared in Database for ContactUs Page
	Then We were able to set the ContactUs Screen color mode from AppSetting
	And ContactUs Local storage value for theme 'theme' is 'dark'
	And Enter Valid data for 4 times in the screen for scenario <ScenarioName>, 'ContactUs-2.11' And Click submit button And Validate by <ContainsText>

	Examples:
		| Environment      | Browser | Device    | SourceURL | ScenarioName                     | ContainsText                                                                                             |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | ContactUs | ContactUs_Screen_Submit_Positive | Your IP address limit reached, please email us to support@niraiya.com directly or try again after a day. |
		| chrome-iPhoneX   | chrome  | iPhone X  | ContactUs | ContactUs_Screen_Submit_Positive | Your IP address limit reached, please email us to support@niraiya.com directly or try again after a day. |
		| chrome-iPad      | chrome  | iPad      | ContactUs | ContactUs_Screen_Submit_Positive | Your IP address limit reached, please email us to support@niraiya.com directly or try again after a day. |
		| chrome-Desktop   | chrome  | Desktop   | ContactUs | ContactUs_Screen_Submit_Positive | Your IP address limit reached, please email us to support@niraiya.com directly or try again after a day. |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | ContactUs | ContactUs_Screen_Submit_Positive | Your IP address limit reached, please email us to support@niraiya.com directly or try again after a day. |
		| firefox-Desktop  | firefox | Desktop   | ContactUs | ContactUs_Screen_Submit_Positive | Your IP address limit reached, please email us to support@niraiya.com directly or try again after a day. |