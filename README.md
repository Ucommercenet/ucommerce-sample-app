# uCommerce Samples App#

This is the uCommerce Samples App that shows some of the most popular extension points to uCommerce.
You can read the change log below and see what the different versions features.

## Change log for V 1.1.0.16070 ##

* NEW: Support for documentation in apps
* NEW: AssemblyInfo version now gets updated when the solution is build, the version number used is the version provided in the nuspec file.
* NEW: Now updates the build number used for the AssembyInfo files, when the version number in the nuspec file ends with an \*. e.g. "1.1.0.\*".
* NEW: Support for database migrations in apps
* NEW: Now maintains the dependencies list in the nuspec file with the dependencies present in the project.
* NOTE: The build number format is YYDDD, e.g. 16070 the two first digits are the last two digit of the year and the last three digits are the the amount of days that have past the current year.

## Change log for V 1.0.0.0 ##

* NEW: Extension to SampleApp that displays hook-in to tree works across all CMSes.
* NEW: Extension to SampleApp that hooks new tabs into SettingsTab.
* NEW: Extension to SampleApp that hooks in server side button and client side button for the search node.


### Bringing out the gimp! ###

Getting started with the Samples App is really easy. Just follow the few steps below:

* Download the Samples App
* Extract to your favorite location
* Open the .sln
* Open 'Deploy.to.uCommerce.ps1' found under the Deployment folder in the solution.
* Modify the function "GetDeploymentDirectories" so it matches the root of your website
* Build the solution (Requires Administrator prev. on VS)
* Restart the website
* ??????
* Profit

### Contribution ###

Your feedback means the world to us. If you have any comments / suggestions / questions you can create a new user at our support forum http://eureka.ucommerce.net/#!/

In there the community will get you the help needed in a jiffy!