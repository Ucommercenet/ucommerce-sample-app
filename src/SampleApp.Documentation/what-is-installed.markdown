# What's Installed

The sample app installs multiple UIs and a product definition and in still article we are going to take a look at the things that are installed.

## UIs

### Tree nodes

The sample app will installed two tree nodes with the name "About" the nodes are identical and will show the same content that you can see in the right side of the picture.

![image](images/what-is-installed-1.png)

### Tab

The sample app also installs a new tab on the Setting node, which is named "About" and shows the same information as the tree nodes.

![image](images/what-is-installed-2.png)

### Buttons

The sample app installs two buttons on the search node under the settings section, the first button will run index everything in uCommerce from scratch and the second button will open the website's frontpage in the content frame.

![image](images/what-is-installed-3.png)

## Shoe Product Definition

The sample app also installs a product definition called "Shoe" which has two product definition fields "Awesomness" which is of the data type boolean and "ShortStory" which is of the data type short text.
This product definition is installed using [the initialization pipeline][1], which means that even though you delete the product definition it will be recreated everytime the application is starts up.

![image](images/what-is-installed-4.png)

[1]: http://docs.ucommerce.net/ucommerce/v7.0/extending-ucommerce/Initialize-pipeline.html