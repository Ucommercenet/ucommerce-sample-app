# How to Setup the Sample App

The uCommerce sample app has multiple configuration options and ways to control what parts of the app is shown/enabled.


## Configure the UIs

### Tab

The tab is the UI with the most configuration options in sample app, below is the configuration of tab's configuration object that controls what is displayed on the tab and whether to display the tab or not.

{CODE-START:xml /}
<configuration>
	<components>
		<!-- Shows how you can control the value of properties on an object through castle windsor config -->
		<component
				id="SampleApp.TabConfiguration"
				service="SampleApp.Extensions.Configuration.TabConfiguration, SampleApp.Extensions"
				type="SampleApp.Extensions.Configuration.TabConfiguration, SampleApp.Extensions">
			<parameters>
				<ShowTab>true</ShowTab>
				<ShowUCommerceVersion>true</ShowUCommerceVersion>
				<ShowShemaVersion>true</ShowShemaVersion>
			</parameters>
		</component>
	</components>
</configuration>
{CODE-END /}

The tab configuration object has three options that you can configure, the first is whether to show the tab or not.
The second option is whether you want the tab to display the version of uCommerce that is installed and the last option is whether the tabs should display the database's schema version.

Another way to disable the tab is to find the tab.config file and add ".disabled" this will let uCommerce know that you don't want use the config file, the tab.config can be found under the website at uCommerce/Apps/SampleApp/Configuration.

### Tree Nodes

As for the tree nodes, you can disable them all together by using the trick as for the tab, simple by adding the ".disabled" to the TreeNode.config which can be found in the same location as the tab.config file.

You can control where the tree nodes appear through the TreeNode.config file, as you can see below in the "supportTypes" you can add new "item" to the "list" and by doing so the tree node will appear where you tell it to and vice versa when you remove an "item" from the "list".
The following link shows the different node types that can be used as the value of an "item", [Tree node type documentation](http://docs.ucommerce.net/ucommerce/v7.0/extending-ucommerce/extending-ucommerce-admin/tree/tree-nodeTypes.html)

{CODE-START:xml /}
<configuration>
	<components>
		<!--  Adds the AboutNodeProvider to the TreeServiceShell -->
		<partial-component id="TreeServiceShell">
			<parameters>
				<tasks>
					<list>
						<item insert="last">${SampleApp.SettingNodes.AboutNodeProvider}</item>
					</list>
				</tasks>
			</parameters>
		</partial-component>

		<!-- This is the configuration of the AboutNodeProvider -->
		<!-- Which provides a new node to the Settings section and a root node -->
		<component
				id ="SampleApp.SettingNodes.AboutNodeProvider"
				service="Ucommerce.Tree.ITreeContentProvider, Ucommerce"
				type="SampleApp.Extensions.UI.Tree.AboutNodeProvider, SampleApp.Extensions" >
			<parameters>
				<nodeName>About</nodeName>
				<supportTypes>
					<list>
						<item>settings</item>
						<item>apps</item>
					</list>
				</supportTypes>
			</parameters>
		</component>
	</components>
</configuration>
{CODE-END /}

### Buttons

The only thing you can configure on the buttons is whether to display them or not. the way to disable the buttons is to find the TabButtons.config file and add ".disabled" this will let uCommerce know that you don't want use the config file, the TabButtons.config can be found under the website at uCommerce/Apps/SampleApp/Configuration.

## Shoe Product Definition

On the product definition you can configure whether or not uCommerce should try to recreate it if it has been deleted, and agian the way to disable the buttons is to find the Initialize.config file and add ".disabled" this will let uCommerce know that you don't want use the config file, the TabButtons.config can be found under the website at uCommerce/Apps/SampleApp/Configuration.
After you disable the config file and delete the Shoe product definition then it shouldn't reappear.