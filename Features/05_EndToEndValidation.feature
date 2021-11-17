Feature: 05_EndToEndValidation
![05_EndToEndValidation]
Link to a feature: [EndToEndValidation](InfinityMatrix.Niraiya.UITests/Features/05_EndToEndValidation.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@Endtoendpositive @Sequential
Scenario: 05_10 End to end positive - New user Sign Up
	Given Get new user details for registration from AppSetting
	And Clear Database records for new user registration from AppSetting
	And Clear all the emails from Mail box for 'Email-Box:EmailId' is 'Email-Box:Password' from AppSetting
	And Setup the browser <Browser> for device <Device>
	And Get and load SignUp page URL to browser from AppSetting 'URLAuth:SignUp-URL' and page loaded with in <PageLoadedInSeconds> seconds
	And Accept cookie button is clicked in Static Page
	And We were able to set the Screen color mode from AppSetting
	And Local storage value for theme 'theme' is 'dark'
	When Enter user details from AppSetting to SignUp page and click submit
	Then Validate SignUp page details in screen for sucessfull response
	And Validate Application auto navigated to Email Pending Verification page from AppSetting 'URLAuth:EmailIdVerificationPending-URL'
	And Validate latest SignUp Email from Mail box for 'Email-Box:EmailId' is 'Email-Box:Password' from AppSetting
	And Open the email as HTML in new tab and click the activate your account button
	And New tab will be opened and total tab count 3 and navigate to 3rd tab
	And Validate Email address Verified Successfully page from AppSetting 'URLAuth:EmailVerified-URL' and content
	And Validate latest password recovery key Email from Mail box for 'Email-Box:EmailId' is 'Email-Box:Password' from AppSetting
	And Goto 1st tab and close other 3 tabs
	And Open SignIn URL from AppSetting 'URLAuth:SignIn-URL' and Enter user details from AppSetting and click submit button
	And Validate screen navigated to NiraiyaAccounts page from AppSetting 'URLAuth:NiraiyaAccounts-URL'
	And Click Add Linked Account card and validate popup is opened
	And Click close button at popup bottom of Add Linked Account popup and validate popup is closed
	And Click Add Linked Account card and validate popup is opened
	And Click close button at popup top of Add Linked Account popup and validate popup is closed
	And Navigate to LoginAndSecurity via menu bar link and validate LoginAndSecurity page from AppSetting 'URLAuth:LoginAndSecurity-URL'
	And Navigate to NiraiyaAccounts via menu bar link and validate NiraiyaAccounts page from AppSetting 'URLAuth:NiraiyaAccounts-URL'
	And Navigate to LoginAndSecurity via menu bar button and validate LoginAndSecurity page from AppSetting 'URLAuth:LoginAndSecurity-URL'
	And Validate LoginAndSecurity page field values from AppSetting
	And Open Edit User Name popup by click edit button in LoginAndSecurity page
	And Click close Edit User Name popup button and check popup is closed

	Examples:
		| Environment      | Browser | Device    | PageLoadedInSeconds |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | 15                  |
		| chrome-iPhoneX   | chrome  | iPhone X  | 15                  |
		| chrome-iPad      | chrome  | iPad      | 15                  |
		| chrome-Desktop   | chrome  | Desktop   | 15                  |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | 20                  |
		| firefox-Desktop  | firefox | Desktop   | 20                  |

@Sequential
Scenario: 05_20 End to end positive - Login And Security Change Name
	Given Get new user details for registration from AppSetting
	And Setup the browser <Browser> for device <Device>
	And Get and load SignUp page URL to browser from AppSetting 'URLAuth:SignUp-URL' and page loaded with in <PageLoadedInSeconds> seconds
	And Accept cookie button is clicked in Static Page
	And We were able to set the Screen color mode from AppSetting
	And Local storage value for theme 'theme' is 'dark'
	Then Open SignIn URL from AppSetting 'URLAuth:SignIn-URL' and Enter user details from AppSetting and click submit button
	Then Validate screen navigated to NiraiyaAccounts page from AppSetting 'URLAuth:NiraiyaAccounts-URL'
	And Navigate to LoginAndSecurity via menu bar link and validate LoginAndSecurity page from AppSetting 'URLAuth:LoginAndSecurity-URL'
	And Validate LoginAndSecurity page field values from AppSetting
	And Open Edit User Name popup by click edit button in LoginAndSecurity page
	And User name in Edit User Name popup from AppSetting 'NewEmailAccount:ChangeName' and click save
	And Validate screen navigated to Home page from AppSetting
	And Accept cookie button is clicked in Static Pages
	And Open SignIn URL from AppSetting 'URLAuth:SignIn-URL' and Enter user details from AppSetting and click submit button
	And Navigate to LoginAndSecurity via menu bar link and validate LoginAndSecurity page from AppSetting 'URLAuth:LoginAndSecurity-URL'
	And Validate LoginAndSecurity page new field values from AppSetting

	Examples:
		| Environment      | Browser | Device    | PageLoadedInSeconds |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | 15                  |
		| chrome-iPhoneX   | chrome  | iPhone X  | 15                  |
		| chrome-iPad      | chrome  | iPad      | 15                  |
		| chrome-Desktop   | chrome  | Desktop   | 15                  |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | 20                  |
		| firefox-Desktop  | firefox | Desktop   | 20                  |

@Sequential
Scenario: 05_30 Recover forget password positive
	Given Get new user details for registration from AppSetting
	And Setup the browser <Browser> for device <Device>
	And Get and load SignUp page URL to browser from AppSetting 'URLAuth:SignUp-URL' and page loaded with in <PageLoadedInSeconds> seconds
	And Accept cookie button is clicked in Static Page
	And We were able to set the Screen color mode from AppSetting
	And Local storage value for theme 'theme' is 'dark'
	And Open SignIn URL from AppSetting 'URLAuth:SignIn-URL' and click on forget password button
	Then Enter SignUp Email from NewEmailAccount from AppSetting and click send email
	And Validate screen navigated to Home page from AppSetting
	And Validate latest Forget Password Request Email from Mail box for 'Email-Box:EmailId' is 'Email-Box:Password' from AppSetting
	And Open the email as HTML in new tab and click the complete your request button
	And Goto 3rd tab to verify
	And Validate Forget Pass Verified Successfully page redirect to 'URLAuth:RecoverPassword-URL' from and its content
	And Goto 3rd tab and close other 2 tabs
	And Get latest password recovery key Email from Mail box for 'Email-Box:EmailId' is 'Email-Box:Password' from AppSetting
	And Enter password recovery key Mail to texbox and click decrypt button
	And Validate user password wrt to recovered password and click on CopyPassword

	Examples:
		| Environment      | Browser | Device    | PageLoadedInSeconds |
		| chrome-GalaxyS5  | chrome  | Galaxy S5 | 15                  |
		| chrome-iPhoneX   | chrome  | iPhone X  | 15                  |
		| chrome-iPad      | chrome  | iPad      | 15                  |
		| chrome-Desktop   | chrome  | Desktop   | 15                  |
		| firefox-GalaxyS5 | firefox | Galaxy S5 | 20                  |
		| firefox-Desktop  | firefox | Desktop   | 20                  |