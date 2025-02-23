# Get build number

This is a simple tool for setting the build number, the last two numbers, in assembly property FileVersion when running MSBuild.
Whatever first two numbers are predefined in **FileVersion** is kept. These normally corresponds to major and minor version of the built file.

## How it works
If environment variable BUILD_BUILDNUMBER is set, e.g. in an Azure pipeline, then that value is used.
Otherwise a number is created from properties **BuildNumber** and **BuildRevision** - which is also created by this tool.

This override of **FileVersion** is not performed when building inside Visual Studio unless the property **SetBuildNumber** is set in the project file.
The **FileVersion** also has to be on the form x.y.0 or x.y.0.0, else the predefined numbers are kept.

**BuildNumber** is the number of days since 1th of January 2025, and **BuildRevision** is the number of seconds since midnight divided by two.
Kind of the same logic used by Visual Studio when using the x.y.* pattern in project settings and setting Deterministic to false.

The values are generated before target BeforeBuild, and in UTC time.

## Why
When building a project with a lot of files it is helpfull to keep track of the individual files that has been changed between each build.
**FileVersion** is then use for that, while AssemblyVersion is used more for keeping track of breaking vs non-breaking changes and releases.
If coupled with build tools like [GitVersion](https://gitversion.net) you get a complete automated system.