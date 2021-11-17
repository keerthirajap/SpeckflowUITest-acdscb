Feature: 01_HomeScreen
![01_HomeScreen]
Link to a feature: [HomeScreen](InfinityMatrix.Niraiya.UITests/Features/01_HomeScreen.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@HomeScreen @FirstCAll @Parallel
Scenario: 01_00 Check Home Screen
	Given Get Home page URL from AppSetting
	When The Home page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked	

	Examples:
		| Environment      | Browser | Device    | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | 500         | 15                  | 600            |
		| chrome-iPhoneX   | chrome  | iPhone X  | 500         | 15                  | 600            |
		| chrome-iPad      | chrome  | iPad      | 500         | 15                  | 600            |
		| chrome-Desktop   | chrome  | Desktop   | 500         | 15                  | 500            |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | 50          | 20                  | 550            |
		| firefox-Desktop  | firefox | Desktop   | 50          | 20                  | 500            |


@HomeScreen @scrollstepbystep @Parallel
Scenario: 01_10 Get Home Screen
	Given Get Home page URL from AppSetting
	When The Home page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked
	Then The Home page loaded with in <PageLoadedInSeconds> seconds
	And Take Home page screenshot with name suffix 'ScrToBottom_pgeload'
	And We were able to get Home page total dimention
	And The Home page is scrolled step by step to end by millisecond delay <Millisecond> by <PageScrollStep>

	Examples:
		| Environment      | Browser | Device    | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | 500         | 15                  | 600            |
		| chrome-iPhoneX   | chrome  | iPhone X  | 500         | 15                  | 600            |
		| chrome-iPad      | chrome  | iPad      | 500         | 15                  | 600            |
		| chrome-Desktop   | chrome  | Desktop   | 500         | 15                  | 500            |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | 50          | 20                  | 550            |
		| firefox-Desktop  | firefox | Desktop   | 50          | 20                  | 500            |

@HomeScreen @AcceptCookieButton @Parallel
Scenario: 01_20 Accept Cookie Button
	Given Get Home page URL from AppSetting
	And The Home page is loaded for browser <Browser> for device <Device>
	And Accept cookie alert is visible for first time
	And Accept cookie button is visible for first time
	And Local storage value for Accept cookie 'ApplicationStorageName_AcceptCookie' is 'No'
	And Take Home page screenshot with name suffix 'ScrToBottom_pgeload'
	When Accept cookie button is clicked
	And Accept cookie alert should not be visisble
	And Accept cookie button should not be visisble
	And Local storage value for Accept cookie 'ApplicationStorageName_AcceptCookie' is 'Yes'

	Examples:
		| Environment      | Browser | Device    |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 |
		| chrome-iPhoneX   | chrome  | iPhone X  |
		| chrome-iPad      | chrome  | iPad      |
		| chrome-Desktop   | chrome  | Desktop   |
		| firefox-GalaxyS5 | firefox | Galaxy S5 |
		| firefox-Desktop  | firefox | Desktop   |

@mstest:donotparallelize @HomeScreen @Parallel
Scenario: 01_30 On IP Address Change
	Given Get Home page URL from AppSetting
	And The Home page is loaded for browser <Browser> for device <Device>
	And We were able to get Home page total dimention
	And The Home page is scrolled to bottom
	And Take Home page screenshot with name suffix 'IPAddressChang_pgeload'
	And IP Address details should not be empty for fields 'IP' 'ISP' 'Region' 'Country'
	When On IP Address Change configured from AppSetting
	Then IP Address details should be changed for fields 'IP' 'ISP' 'Region' 'Country'

	Examples:
		| Environment      | Browser | Device    |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 |
		| chrome-iPhoneX   | chrome  | iPhone X  |
		| chrome-iPad      | chrome  | iPad      |
		| chrome-Desktop   | chrome  | Desktop   |
		| firefox-GalaxyS5 | firefox | Galaxy S5 |
		| firefox-Desktop  | firefox | Desktop   |

@DevToolsNotAllowed @HomeScreen @Parallel
Scenario: 01_40 Home Dev_Tools Not Allowed Redirect Validation
	Given Get Home page URL from AppSetting
	And The Homepage is loaded for browser with devtools <Browser> for device <Device> 
	When Dev Tools Tab is opened in browser
	Then Validate user shown as dev tools not allowed from h1 tag			
	
	Examples:
		| Environment      | Browser | Device    |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 |
		| chrome-iPhoneX   | chrome  | iPhone X  |
		| chrome-iPad      | chrome  | iPad      |
		| chrome-Desktop   | chrome  | Desktop   |
		| firefox-GalaxyS5 | firefox | Galaxy S5 |
		| firefox-Desktop  | firefox | Desktop   |