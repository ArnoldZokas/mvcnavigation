# MvcNavigation

## Overview
MvcNavigation is a library that simplifies menu navigation, breadcrumb navigation and xml sitemaps in ASP.NET MVC3 apps.

## Quickstart
**1. Install:** Add NuGet package [MvcNavigation](http://nuget.org/packages/mvcnavigation) to your project.

**2. Configure:** Add this code somewhere in your application start logic:

```csharp
var sitemap = new Node<HomeController>(
				c => c.Index(),	// this is your root node (probably your Home page)
					new Node<HomeController>(c => c.About()) // this is a child node
				);

NavigationConfiguration.Initialise(sitemap);
```

**3. Display:** Add `@Html.Menu()` to your view to display a menu.

## Installation
### Requirements
This library has a dependency on .NET 4.0 (due to use of dynamics) and targets ASP.NET MVC 3.

*.NET 3.5 and ASP.NET MVC 2 are not supported.*

### How to install via NuGet
The quickest way to install is via NuGet. If don't know what NuGet is, have a quick look at [NuGet Overview](http://docs.nuget.org/docs/start-here/overview).

To install MvcNavigation, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console):

```powershell
PM> Install-Package MvcNavigation
```

Alternatively, you can install this package using the [NuGet Package Manager Dialog](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog).

### How to build locally

You need to have .NET 4.0 and Powershell 2.0 installed on you machine:

1. Pull latest source from `git://github.com/ArnoldZokas/MvcNavigation.git`
2. Go to **/build** folder and run `build-release.bat`
3. Go to **/build_output** folder to get binaries and C#/Razor views

## Advanced topics
### Caching and performance

MvcNavigation does not cache anything internally and relies on OutputCache (you do use OutputCache, don't you?).<br />
If you need fine-grained control over caching, put your menu/breadcrumb into a child action and use OutputCache.

## License
Copyright © 2012, Arnold Zokas<br />
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of the organisation nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.



<hr />
<hr />

 - configuration
	- how to configure sitemap
	- title defaults and fallback
	- how to configure css classes (default css classes)
 - advanced usages
	- multiple/named menus
	- multiple/named breadcrumbs
	- multi-level menus
	- how to change rendering output
	- how to use dynamic node
	- how to use areas (area fallback)
	- how to use in child views
 - extensibility API
	- how to extend INode
	- how to create html helper extensions
 - contribute
	- feature suggestions
	- bug reports
 - credits
	- credit MvcSiteMapProvider author and contributors
	- credit code reviewers