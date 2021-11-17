Feature: 03_PrivacyAndSecurityScreen
![03_PrivacyAndSecurityScreen]
Link to a feature: [PrivacyAndSecurityScreen](InfinityMatrix.Niraiya.UITests/Features/03_PrivacyAndSecurityScreen.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@PrivacyAndSecurity @GetPrivacyAndSecurityScreen @Parallel
Scenario: 03_10 Get PrivacyAndSecurity Screen
	Given Get PrivacyAndSecurity page URL from AppSetting
	And The PrivacyAndSecurity page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked in PrivacyAndSecurity page
	Then The PrivacyAndSecurity page loaded with in <PageLoadedInSeconds> seconds
	And We were able to get PrivacyAndSecurity page total dimention
	And We were able to set the PrivacyAndSecurity Screen color mode from AppSetting
	And PrivacyAndSecurity Local storage value for theme 'theme' is 'dark'
	And The PrivacyAndSecurity page is scrolled step by step to end by millisecond delay <Millisecond>

	Examples:
		| Environment      | Browser | Device    | Millisecond | PageLoadedInSeconds |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | 500         | 10                  |
		| chrome-iPhoneX   | chrome  | iPhone X  | 500         | 10                  |
		| chrome-iPad      | chrome  | iPad      | 500         | 10                  |
		| chrome-Desktop   | chrome  | Desktop   | 500         | 10                  |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | 200         | 20                  |
		| firefox-Desktop  | firefox | Desktop   | 200         | 20                  |

@PrivacyAndSecurity @Parallel
Scenario: 03_20 Accept Cookie Button for PrivacyAndSecurity
	Given Get PrivacyAndSecurity page URL from AppSetting
	And The PrivacyAndSecurity page is loaded for browser <Browser> for device <Device>
	And Accept cookie alert is visible for first time in PrivacyAndSecurity page
	And Accept cookie button is visible for first time in PrivacyAndSecurity page
	And Local storage value for Accept cookie 'ApplicationStorageName_AcceptCookie' is 'No' in PrivacyAndSecurity page
	When Accept cookie button is clicked in PrivacyAndSecurity page
	And Accept cookie alert should not be visisble in PrivacyAndSecurity page
	And Accept cookie button should not be visisble in PrivacyAndSecurity page
	And Local storage value for Accept cookie 'ApplicationStorageName_AcceptCookie' is 'Yes' in PrivacyAndSecurity page

	Examples:
		| Environment      | Browser | Device    |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 |
		| chrome-iPhoneX   | chrome  | iPhone X  |
		| chrome-iPad      | chrome  | iPad      |
		| chrome-Desktop   | chrome  | Desktop   |
		| firefox-GalaxyS5 | firefox | Galaxy S5 |
		| firefox-Desktop  | firefox | Desktop   |

@PrivacyAndSecurity @Parallel
Scenario: 03_30 Check PrivacyAndSecurity Accordion
	Given Get PrivacyAndSecurity page URL from AppSetting
	And The PrivacyAndSecurity page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked in PrivacyAndSecurity page
	Then We were able to set the PrivacyAndSecurity Screen color mode from AppSetting
	And PrivacyAndSecurity Local storage value for theme 'theme' is 'dark'
	And When click on Accordion Link 'Privacy&SecurityPolicy' Page should scroll to respective accordion and check header 'Privacy & Security Policy'
	And When click on Accordion Link 'AboutNiraiya' Page should scroll to respective accordion and check header 'About Niraiya'
	And When click on Accordion Link 'SecurityAtNiraiya' Page should scroll to respective accordion and check header 'Security at Niraiya'
	And When click on Accordion Link 'PrivacyAtNiraiya' Page should scroll to respective accordion and check header 'Privacy at Niraiya'
	And When click on Accordion Link 'DataSharing' Page should scroll to respective accordion and check header 'Data sharing'
	And When click on Accordion Link 'AdsAndTrackers' Page should scroll to respective accordion and check header 'Ads and Trackers'
	And When click on Accordion Link 'AccountTermination' Page should scroll to respective accordion and check header 'Account Termination'

	Examples:
		| Environment      | Browser | Device    |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 |
		| chrome-iPhoneX   | chrome  | iPhone X  |
		| chrome-iPad      | chrome  | iPad      |
		| chrome-Desktop   | chrome  | Desktop   |
		| firefox-GalaxyS5 | firefox | Galaxy S5 |
		| firefox-Desktop  | firefox | Desktop   |