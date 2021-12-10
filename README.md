# SEPluginTemplate

This is a template to help you make a Space Engineers Plugin.

## Setup 

1. Download a Zip file copy of this repository. 
![Screenshot (517)](https://user-images.githubusercontent.com/80211714/122436743-12449e00-cf67-11eb-9ea0-d139216f11cc.png)

2. Extract to file to your directory that has your VS projects. Default is: `C:\Users\[Your User]\source\repos`

6. Now delete `.gitattributes` and `.gitignore` and `.git` in the root folder of the project These files are for this respository and are not required.

7. Open Visual Studio and click "Open a project or solution"

8. Select `SEPluginTemplate` then  `SEPluginTemplate.sln`

9. The project should open.

10. Right click on the project file and go to `Build Events>Post-build event command line`. Change `[Plugin Folder Location]` to the location of your plugins folder.

11. You may now edit the project. When your are done, just go to the next section called "Build your plugin."

## Build your plugin

To build your plugin, just click start and if you configured `PostBuild.bat` correctly, VS will build your plugin, move the plugin to the plugins folder, and start Space Engineers for you. You just need to enable the plugin in the plugins menu if you have SEPluginLoader installed! If you dont have SEPluginLoader installed, then head over to https://github.com/austinvaness/PluginLoader to see how to install it.

If you have issues or need help, feel free to make new issue under the issues tab of this repository!


