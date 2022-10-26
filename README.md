# ReefXOrganiser

### About
A simple tool to help with naming and organising photos for Marine Scientific Surveys, to be submitted for analysis for biodiversity in Singapore Waters.

This project is written in C# .Net using MAUI to allow for cross-platform use of the tool. Currently, only windows x64 is supported, with MacOS following it.

### Features
- Names images and generates filenames according to submission specification
- Generates a CSV file that can be used to copy and paste and submit to the submission google docs
- Save project to return to it later

### Current Limitations
- No support for Video playback and detection
- No easy organising of the images
- Requires a standard workflow
- Terrible UI
- Project sav file is not transferrable to another PC.

### How to install
You'll also need to download and install the [windows app sdk](http://https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/downloads "windows app sdk"), select the installer. 
You can download the latest version of the reefxorganiser app from the release section.

### How to use
![](https://github.com/harizyet/ReefXOrganiser/blob/main/README-IMG/menu.png?raw=true)
To begin, select a folder that contains your images. Currently, PNG, JPG and TIFF is supported. The program will then detect and load all the images in the directory and subdirectories. Once done, click continue.

If you have a .sav file from an existing run, click load project and load the .sav file.

![](https://github.com/harizyet/ReefXOrganiser/blob/main/README-IMG/createproject.png?raw=true)
Once done, enter your details. Surveyor Name, Team, Location and Date. Once done, click continue.

![](https://github.com/harizyet/ReefXOrganiser/blob/main/README-IMG/project.png?raw=true)
In the Project page, cycle through the photos that it detected and fill in the details accordingly. Deleting an image here will not delete the source, but will be excluded when you click on export.

Once ready, click on export. Select a folder where you want the files to be saved, and the software will export the images and generate a CSV file.

![](https://github.com/harizyet/ReefXOrganiser/blob/main/README-IMG/exportfolder.png?raw=true)
It would then generate the files inside the folder.

![](https://github.com/harizyet/ReefXOrganiser/blob/main/README-IMG/csvfile.png?raw=true)
The CSV file it generates would then be sufficient for you to copy and paste into the submission google docs.

![](https://github.com/harizyet/ReefXOrganiser/blob/main/README-IMG/exportimagefolder.png?raw=true)
While the images from the source would be copied over to the destination with the images renamed appropriately.
