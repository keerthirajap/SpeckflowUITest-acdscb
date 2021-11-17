Feature: 04_StaticScreens
![04_StaticScreens]
Link to a feature: [StaticScreens](InfinityMatrix.Niraiya.UITests/Features/04_StaticScreens.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@StaticScreens @Parallel
Scenario: 04_10 Navigate to Static Pages
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked in Static Page
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And We were able to get Static Page total dimention
	And We were able to set the Static Page Screen color mode from AppSetting
	And  Static Page Local storage value for theme 'theme' is 'dark'
	And Take Static Pages page screenshot with name prefix <ScenarioName>
	And The Static Page is scrolled step by step to end by millisecond delay <Millisecond> by <PageScrollStep>

	Examples:
		| Environment         | Browser | Device    | ScenarioName            | PageURL-AppSettings             | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | Niraiya Site Map        | URL:sitemap-URL                 | 500         | 10                  | 500            |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | Niraiya Site Map        | URL:sitemap-URL                 | 500         | 10                  | 500            |
		| 03_chrome-iPad      | chrome  | iPad      | Niraiya Site Map        | URL:sitemap-URL                 | 500         | 10                  | 500            |
		| 04_chrome-Desktop   | chrome  | Desktop   | Niraiya Site Map        | URL:sitemap-URL                 | 500         | 10                  | 500            |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | Niraiya Site Map        | URL:sitemap-URL                 | 200         | 20                  | 500            |
		| 06_firefox-Desktop  | firefox | Desktop   | Niraiya Site Map        | URL:sitemap-URL                 | 200         | 20                  | 500            |
		| 07_chrome-GalaxyS5  | chrome  | Galaxy S5 | Niraiya bot             | URL:Niraiyabot-URL              | 500         | 10                  | 200            |
		| 08_chrome-iPhoneX   | chrome  | iPhone X  | Niraiya bot             | URL:Niraiyabot-URL              | 500         | 10                  | 200            |
		| 09_chrome-iPad      | chrome  | iPad      | Niraiya bot             | URL:Niraiyabot-URL              | 500         | 10                  | 200            |
		| 10_chrome-Desktop   | chrome  | Desktop   | Niraiya bot             | URL:Niraiyabot-URL              | 500         | 10                  | 200            |
		| 11_firefox-GalaxyS5 | firefox | Galaxy S5 | Niraiya bot             | URL:Niraiyabot-URL              | 200         | 20                  | 200            |
		| 12_firefox-Desktop  | firefox | Desktop   | Niraiya bot             | URL:Niraiyabot-URL              | 200         | 20                  | 200            |
		| 13_chrome-GalaxyS5  | chrome  | Galaxy S5 | EmailVerified           | URL:EmailVerified-URL           | 500         | 10                  | 200            |
		| 14_chrome-iPhoneX   | chrome  | iPhone X  | EmailVerified           | URL:EmailVerified-URL           | 500         | 10                  | 200            |
		| 15_chrome-iPad      | chrome  | iPad      | EmailVerified           | URL:EmailVerified-URL           | 500         | 10                  | 200            |
		| 16_chrome-Desktop   | chrome  | Desktop   | EmailVerified           | URL:EmailVerified-URL           | 500         | 10                  | 200            |
		| 17_firefox-GalaxyS5 | firefox | Galaxy S5 | EmailVerified           | URL:EmailVerified-URL           | 200         | 20                  | 200            |
		| 18_firefox-Desktop  | firefox | Desktop   | EmailVerified           | URL:EmailVerified-URL           | 200         | 20                  | 200            |
		| 19_chrome-GalaxyS5  | chrome  | Galaxy S5 | InValidVerificationId   | URL:InValidVerificationId-URL   | 500         | 10                  | 200            |
		| 20_chrome-iPhoneX   | chrome  | iPhone X  | InValidVerificationId   | URL:InValidVerificationId-URL   | 500         | 10                  | 200            |
		| 21_chrome-iPad      | chrome  | iPad      | InValidVerificationId   | URL:InValidVerificationId-URL   | 500         | 10                  | 200            |
		| 22_chrome-Desktop   | chrome  | Desktop   | InValidVerificationId   | URL:InValidVerificationId-URL   | 500         | 10                  | 200            |
		| 23_firefox-GalaxyS5 | firefox | Galaxy S5 | InValidVerificationId   | URL:InValidVerificationId-URL   | 200         | 20                  | 200            |
		| 24_firefox-Desktop  | firefox | Desktop   | InValidVerificationId   | URL:InValidVerificationId-URL   | 200         | 20                  | 200            |
		| 25_chrome-GalaxyS5  | chrome  | Galaxy S5 | WebsiteUnderMaintenance | URL:WebsiteUnderMaintenance-URL | 500         | 10                  | 200            |
		| 26_chrome-iPhoneX   | chrome  | iPhone X  | WebsiteUnderMaintenance | URL:WebsiteUnderMaintenance-URL | 500         | 10                  | 200            |
		| 27_chrome-iPad      | chrome  | iPad      | WebsiteUnderMaintenance | URL:WebsiteUnderMaintenance-URL | 500         | 10                  | 200            |
		| 28_chrome-Desktop   | chrome  | Desktop   | WebsiteUnderMaintenance | URL:WebsiteUnderMaintenance-URL | 500         | 10                  | 200            |
		| 29_firefox-GalaxyS5 | firefox | Galaxy S5 | WebsiteUnderMaintenance | URL:WebsiteUnderMaintenance-URL | 200         | 20                  | 200            |
		| 30_firefox-Desktop  | firefox | Desktop   | WebsiteUnderMaintenance | URL:WebsiteUnderMaintenance-URL | 200         | 20                  | 200            |
		| 31_chrome-GalaxyS5  | chrome  | Galaxy S5 | PageUnderConstruction   | URL:PageUnderConstruction-URL   | 500         | 10                  | 200            |
		| 32_chrome-iPhoneX   | chrome  | iPhone X  | PageUnderConstruction   | URL:PageUnderConstruction-URL   | 500         | 10                  | 200            |
		| 33_chrome-iPad      | chrome  | iPad      | PageUnderConstruction   | URL:PageUnderConstruction-URL   | 500         | 10                  | 200            |
		| 34_chrome-Desktop   | chrome  | Desktop   | PageUnderConstruction   | URL:PageUnderConstruction-URL   | 500         | 10                  | 200            |
		| 35_firefox-GalaxyS5 | firefox | Galaxy S5 | PageUnderConstruction   | URL:PageUnderConstruction-URL   | 200         | 20                  | 200            |
		| 36_firefox-Desktop  | firefox | Desktop   | PageUnderConstruction   | URL:PageUnderConstruction-URL   | 200         | 20                  | 200            |

@Parallel
Scenario: 04_20 Validate to LoadingIndicator Pages
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And We were able to get Static Page total dimention
	And We were able check the LoadingIndicator text

	Examples:
		| Environment         | Browser | Device    | ScenarioName     | PageURL-AppSettings      | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | LoadingIndicator | URL:LoadingIndicator-URL | 500         | 10                  | 500            |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | LoadingIndicator | URL:LoadingIndicator-URL | 500         | 10                  | 500            |
		| 03_chrome-iPad      | chrome  | iPad      | LoadingIndicator | URL:LoadingIndicator-URL | 500         | 10                  | 500            |
		| 04_chrome-Desktop   | chrome  | Desktop   | LoadingIndicator | URL:LoadingIndicator-URL | 500         | 10                  | 500            |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | LoadingIndicator | URL:LoadingIndicator-URL | 200         | 20                  | 500            |
		| 06_firefox-Desktop  | firefox | Desktop   | LoadingIndicator | URL:LoadingIndicator-URL | 200         | 20                  | 500            |

@Parallel
Scenario: 04_30 Validate AjaxErrorPage Page
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked in Static Page
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And We were able to get Static Page total dimention
	And We were able to set the Static Page Screen color mode from AppSetting
	And  Static Page Local storage value for theme 'theme' is 'dark'
	And Raise ajax exception at server by clicking 'btnGetAjaxError' button
	And Validate AjaxErrorPage Page by popup display
	And Validate Request Id against database log - AjaxErrorPage

	Examples:
		| Environment         | Browser | Device    | ScenarioName  | PageURL-AppSettings   | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | AjaxErrorPage | URL:AjaxErrorPage-URL | 500         | 10                  | 500            |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | AjaxErrorPage | URL:AjaxErrorPage-URL | 500         | 10                  | 500            |
		| 03_chrome-iPad      | chrome  | iPad      | AjaxErrorPage | URL:AjaxErrorPage-URL | 500         | 10                  | 500            |
		| 04_chrome-Desktop   | chrome  | Desktop   | AjaxErrorPage | URL:AjaxErrorPage-URL | 500         | 10                  | 500            |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | AjaxErrorPage | URL:AjaxErrorPage-URL | 200         | 20                  | 500            |
		| 06_firefox-Desktop  | firefox | Desktop   | AjaxErrorPage | URL:AjaxErrorPage-URL | 200         | 20                  | 500            |

@Parallel
Scenario: 04_40 Validate Error Page
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	When Accept cookie button is clicked in Static Page
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And We were able to get Static Page total dimention
	And We were able to set the Static Page Screen color mode from AppSetting
	And  Static Page Local storage value for theme 'theme' is 'dark'
	And Validate Request Id against database log - ErrorPage

	Examples:
		| Environment         | Browser | Device    | ScenarioName | PageURL-AppSettings | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | ErrorPage    | URL:ErrorPage-URL   | 500         | 10                  | 500            |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | ErrorPage    | URL:ErrorPage-URL   | 500         | 10                  | 500            |
		| 03_chrome-iPad      | chrome  | iPad      | ErrorPage    | URL:ErrorPage-URL   | 500         | 10                  | 500            |
		| 04_chrome-Desktop   | chrome  | Desktop   | ErrorPage    | URL:ErrorPage-URL   | 500         | 10                  | 500            |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | ErrorPage    | URL:ErrorPage-URL   | 200         | 20                  | 500            |
		| 06_firefox-Desktop  | firefox | Desktop   | ErrorPage    | URL:ErrorPage-URL   | 200         | 20                  | 500            |

@Parallel
Scenario: 04_50 Validate Raw HTML Page 1
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And The Static Page is scrolled step by step to end by millisecond delay <Millisecond> by <PageScrollStep>
	And The Static Page wait for few seconds 2500

	Examples:
		| Environment         | Browser | Device    | ScenarioName     | PageURL-AppSettings      | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | sitemap-xml      | URL:sitemapxml-URL       | 500         | 10                  | 500            |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | sitemap-xml      | URL:sitemapxml-URL       | 500         | 10                  | 500            |
		| 03_chrome-iPad      | chrome  | iPad      | sitemap-xml      | URL:sitemapxml-URL       | 500         | 10                  | 500            |
		| 04_chrome-Desktop   | chrome  | Desktop   | sitemap-xml      | URL:sitemapxml-URL       | 500         | 10                  | 500            |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | sitemap-xml      | URL:sitemapxml-URL       | 200         | 20                  | 500            |
		| 06_firefox-Desktop  | firefox | Desktop   | sitemap-xml      | URL:sitemapxml-URL       | 200         | 20                  | 500            |
		| 07_chrome-GalaxyS5  | chrome  | Galaxy S5 | sitemap-blog.xml | URL:sitemap-blog.xml-URL | 500         | 10                  | 500            |
		| 08_chrome-iPhoneX   | chrome  | iPhone X  | sitemap-blog.xml | URL:sitemap-blog.xml-URL | 500         | 10                  | 500            |
		| 09_chrome-iPad      | chrome  | iPad      | sitemap-blog.xml | URL:sitemap-blog.xml-URL | 500         | 10                  | 500            |
		| 10_chrome-Desktop   | chrome  | Desktop   | sitemap-blog.xml | URL:sitemap-blog.xml-URL | 500         | 10                  | 500            |
		| 11_firefox-GalaxyS5 | firefox | Galaxy S5 | sitemap-blog.xml | URL:sitemap-blog.xml-URL | 200         | 20                  | 500            |
		| 12_firefox-Desktop  | firefox | Desktop   | sitemap-blog.xml | URL:sitemap-blog.xml-URL | 200         | 20                  | 500            |
		| 13_chrome-GalaxyS5  | chrome  | Galaxy S5 | sitemap-pin.xml  | URL:sitemap-pin.xml-URL  | 500         | 10                  | 500            |
		| 14_chrome-iPhoneX   | chrome  | iPhone X  | sitemap-pin.xml  | URL:sitemap-pin.xml-URL  | 500         | 10                  | 500            |
		| 15_chrome-iPad      | chrome  | iPad      | sitemap-pin.xml  | URL:sitemap-pin.xml-URL  | 500         | 10                  | 500            |
		| 16_chrome-Desktop   | chrome  | Desktop   | sitemap-pin.xml  | URL:sitemap-pin.xml-URL  | 500         | 10                  | 500            |
		| 17_firefox-GalaxyS5 | firefox | Galaxy S5 | sitemap-pin.xml  | URL:sitemap-pin.xml-URL  | 200         | 20                  | 500            |
		| 18_firefox-Desktop  | firefox | Desktop   | sitemap-pin.xml  | URL:sitemap-pin.xml-URL  | 200         | 20                  | 500            |

@Parallel
Scenario: 04_60 Validate Raw HTML Page 2
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And The Static Page is scrolled step by step to end by millisecond delay <Millisecond> by <PageScrollStep>
	And The Static Page wait for few seconds 2500

	Examples:
		| Environment         | Browser | Device    | ScenarioName | PageURL-AppSettings | Millisecond | PageLoadedInSeconds | PageScrollStep |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | robots.txt   | URL:robots.txt-URL  | 500         | 10                  | 500            |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | robots.txt   | URL:robots.txt-URL  | 500         | 10                  | 500            |
		| 03_chrome-iPad      | chrome  | iPad      | robots.txt   | URL:robots.txt-URL  | 500         | 10                  | 500            |
		| 04_chrome-Desktop   | chrome  | Desktop   | robots.txt   | URL:robots.txt-URL  | 500         | 10                  | 500            |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | robots.txt   | URL:robots.txt-URL  | 200         | 20                  | 500            |
		| 06_firefox-Desktop  | firefox | Desktop   | robots.txt   | URL:robots.txt-URL  | 200         | 20                  | 500            |
		| 07_chrome-GalaxyS5  | chrome  | Galaxy S5 | SitemapUrls  | URL:SitemapUrls-URL | 500         | 10                  | 500            |
		| 08_chrome-iPhoneX   | chrome  | iPhone X  | SitemapUrls  | URL:SitemapUrls-URL | 500         | 10                  | 500            |
		| 09_chrome-iPad      | chrome  | iPad      | SitemapUrls  | URL:SitemapUrls-URL | 500         | 10                  | 500            |
		| 10_chrome-Desktop   | chrome  | Desktop   | SitemapUrls  | URL:SitemapUrls-URL | 500         | 10                  | 500            |
		| 11_firefox-GalaxyS5 | firefox | Galaxy S5 | SitemapUrls  | URL:SitemapUrls-URL | 200         | 20                  | 500            |
		| 12_firefox-Desktop  | firefox | Desktop   | SitemapUrls  | URL:SitemapUrls-URL | 200         | 20                  | 500            |
		| 13_chrome-GalaxyS5  | chrome  | Galaxy S5 | rss          | URL:rss-URL         | 500         | 10                  | 500            |
		| 14_chrome-iPhoneX   | chrome  | iPhone X  | rss          | URL:rss-URL         | 500         | 10                  | 500            |
		| 15_chrome-iPad      | chrome  | iPad      | rss          | URL:rss-URL         | 500         | 10                  | 500            |
		| 16_chrome-Desktop   | chrome  | Desktop   | rss          | URL:rss-URL         | 500         | 10                  | 500            |
		| 17_firefox-GalaxyS5 | firefox | Galaxy S5 | rss          | URL:rss-URL         | 200         | 20                  | 500            |
		| 18_firefox-Desktop  | firefox | Desktop   | rss          | URL:rss-URL         | 200         | 20                  | 500            |

@Parallel
Scenario: 04_70 Validate Raw HTML Page 3
	Given Get Static Page URL from AppSetting for <ScenarioName> and <PageURL-AppSettings>
	And The Static Page is loaded for browser <Browser> for device <Device>
	Then The Static Page loaded with in <PageLoadedInSeconds> seconds
	And The Static Page is scrolled step by step to end by millisecond delay <Millisecond> by <PageScrollStep>
	And The Static Page wait for few seconds <WaitFor>

	Examples:
		| Environment         | Browser | Device    | ScenarioName              | PageURL-AppSettings                     | Millisecond | PageLoadedInSeconds | PageScrollStep | WaitFor |
		| 01_chrome-GalaxyS5  | chrome  | Galaxy S5 | GetNiraiyaAdDisplayStatus | URLStatic:GetNiraiyaAdDisplayStatus-URL | 500         | 10                  | 500            | 2500    |
		| 02_chrome-iPhoneX   | chrome  | iPhone X  | GetNiraiyaAdDisplayStatus | URLStatic:GetNiraiyaAdDisplayStatus-URL | 500         | 10                  | 500            | 2500    |
		| 03_chrome-iPad      | chrome  | iPad      | GetNiraiyaAdDisplayStatus | URLStatic:GetNiraiyaAdDisplayStatus-URL | 500         | 10                  | 500            | 2500    |
		| 04_chrome-Desktop   | chrome  | Desktop   | GetNiraiyaAdDisplayStatus | URLStatic:GetNiraiyaAdDisplayStatus-URL | 500         | 10                  | 500            | 2500    |
		| 05_firefox-GalaxyS5 | firefox | Galaxy S5 | GetNiraiyaAdDisplayStatus | URLStatic:GetNiraiyaAdDisplayStatus-URL | 200         | 20                  | 500            | 2500    |
		| 06_firefox-Desktop  | firefox | Desktop   | GetNiraiyaAdDisplayStatus | URLStatic:GetNiraiyaAdDisplayStatus-URL | 200         | 20                  | 500            | 2500    |
		| 07_chrome-GalaxyS5  | chrome  | Galaxy S5 | GetNiraiyaJobsStatus      | URLStatic:GetNiraiyaJobsStatus-URL      | 500         | 10                  | 500            | 2500    |
		| 08_chrome-iPhoneX   | chrome  | iPhone X  | GetNiraiyaJobsStatus      | URLStatic:GetNiraiyaJobsStatus-URL      | 500         | 10                  | 500            | 2500    |
		| 09_chrome-iPad      | chrome  | iPad      | GetNiraiyaJobsStatus      | URLStatic:GetNiraiyaJobsStatus-URL      | 500         | 10                  | 500            | 2500    |
		| 10_chrome-Desktop   | chrome  | Desktop   | GetNiraiyaJobsStatus      | URLStatic:GetNiraiyaJobsStatus-URL      | 500         | 10                  | 500            | 2500    |
		| 11_firefox-GalaxyS5 | firefox | Galaxy S5 | GetNiraiyaJobsStatus      | URLStatic:GetNiraiyaJobsStatus-URL      | 200         | 20                  | 500            | 2500    |
		| 12_firefox-Desktop  | firefox | Desktop   | GetNiraiyaJobsStatus      | URLStatic:GetNiraiyaJobsStatus-URL      | 200         | 20                  | 500            | 2500    |
		| 13_chrome-GalaxyS5  | chrome  | Galaxy S5 | Elmah                     | URLStatic:Elmah-URL                     | 500         | 10                  | 500            | 8000    |
		| 14_chrome-iPhoneX   | chrome  | iPhone X  | Elmah                     | URLStatic:Elmah-URL                     | 500         | 10                  | 500            | 8000    |
		| 15_chrome-iPad      | chrome  | iPad      | Elmah                     | URLStatic:Elmah-URL                     | 500         | 10                  | 500            | 8000    |
		| 16_chrome-Desktop   | chrome  | Desktop   | Elmah                     | URLStatic:Elmah-URL                     | 500         | 10                  | 500            | 8000    |
		| 17_firefox-GalaxyS5 | firefox | Galaxy S5 | Elmah                     | URLStatic:Elmah-URL                     | 200         | 20                  | 500            | 8000    |
		| 18_firefox-Desktop  | firefox | Desktop   | Elmah                     | URLStatic:Elmah-URL                     | 200         | 20                  | 500            | 8000    |