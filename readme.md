# MvcNavigation

## Overview
MvcNavigation is a library that simplifies menu navigation, breadcrumb navigation and xml sitemaps in ASP.NET MVC3 apps.

## Quickstart
**1. Install:** Add NuGet package [MvcNavigation](http://nuget.org/packages/mvcnavigation) to your project.

**2. Configure:** Add this code somewhere in your application start logic:

```C#
var sitemap = new Node<HomeController>(
				c => c.Index(),	// this is your root node (probably your Home page)
					new Node<HomeController>(c => c.About()) // this is a child node
				);

NavigationConfiguration.Initialise(sitemap);
```

**3. Display:** Add `@Html.Menu()` to your view to display a menu.




installation
	- requirements
		- .NET 4
		- MVC 3
	- nuget
	- download from GH
	- compile yourself
		- VS 2010
 - quickstart
	- configure
	- update global.asax
	- add Html.Menu
	- add Html.Breadcrumb
	- add return NewXmlSitemapResults
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
 - caching
	- we do no caching - do it yourself
 - contribute
	- feature suggestions
	- bug reports
 - credits
	- credit MvcSiteMapProvider author and contributors
	- credit code reviewers
 - license