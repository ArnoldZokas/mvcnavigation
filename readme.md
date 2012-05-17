# MvcNavigation
## Overview
MvcNavigation is a library that simplifies menu navigation, breadcrumb navigation and xml sitemaps in ASP.NET MVC3 apps.

## Quickstart
**1. Install:** Add NuGet package [MvcNavigation](http://nuget.org/packages/mvcnavigation) to your project.

**2. Configure:** Add this code somewhere in your application start logic:

```csharp
var sitemap = new Node<HomeController>(
				c => c.Index(),	// this is your root node (most likely your Home page)
					new Node<HomeController>(c => c.About()) // this is a child node
				);

NavigationConfiguration.Initialise(sitemap);
```

**3. Display:** Add `@Html.Menu()` to your view to display a menu.

Visit [MvcNavigation sample app](http://mvcnavigation.apphb.com) for examples of what you can create.

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

## Basic usage
### Configuration
MvcNavigation is designed to be simple and easy to use:

* **Requires minimal configuration:** just describe your sitemap
* **Strongly-typed configuration:** no magic strings, refactor-friendly
* **Sensible defaults:** which you can override (but only if you need to)
* **Test-friendly:** you can test any part of your sitemap configuration

#### 1) Basic configuration
Do this once in you application initialisation code:

```csharp
public class MvcApplication : System.Web.HttpApplication
{
	protected void Application_Start()
	{
		// initialise MvcNavigation
		var sitemap = new Node<YourController>(
			c => c.YourAction(),	// this is your root node (most likely your Home page)
				new Node<YourController>(c => c.AnotherAction()) // this is a child node
			);
		
		NavigationConfiguration.Initialise(sitemap);
		
		// do other important stuff
	}
}
```

#### 2) Link text
When generating menus and breadcrumbs, MvcNavigation must decide on what text should be used for links.

The algorithm is:

* Use custom text when specified, *else*
* Use [ActionNameAttribute](http://msdn.microsoft.com/en-us/library/system.web.mvc.actionnameattribute.aspx) name when specified, *else*
* Default to action name

To specify custom link text, configure your sitemap using a diffrent `Node<TController>` constructor:

```csharp
var sitemap = new Node<YourController>(c => c.YourAction(),	"Custom link text");
```

#### 3) "Selected" CSS class
When generating menus and breadcrumbs, MvcNavigation will automatically add css marker class to links that match current request path.

The default CSS class is "selected".

This can be overriden using:

```csharp
public class MvcApplication : System.Web.HttpApplication
{
	protected void Application_Start()
	{
		// set custom css marker class
		NavigationConfiguration.SelectedNodeCssClass = "your-css-class";
		
		// do other important stuff
	}
}
```

### Menu
`Menu()` is an HtmlHelper extension method that uses configured sitemap to generate HTML navigation.

Given configuration:

```csharp
protected void Application_Start()
{
	// initialise MvcNavigation
	var sitemap = new Node<YourController>(
		c => c.YourAction(),
			new Node<YourController>(c => c.AnotherAction())
		);
		
	NavigationConfiguration.Initialise(sitemap);
	
	// initialise routes
	var routes = RouteTable.Routes;
	routes.MapRoute("a", "", new { controller = "YourController", action = "YourAction" });
	routes.MapRoute("b", "another-action", new {controller = "YourController", action = "AnotherAction"});
}
```

Result of:

```html
@Html.Menu();
```

Will be:

```html
<ul>
	<li><a href="/">YourAction</a></li>
	<li><a href="/another-action">AnotherAction</a></li>
</ul>
```

`Menu()` can be used in child views (including nested child views).

Visit [MvcNavigation sample app](http://mvcnavigation.apphb.com) for live examples.

### Breadcrumb
`Breadcrumb()` is an HtmlHelper extension method that uses configured sitemap to generate HTML breadcrumb navigation.

Given configuration:

```csharp
protected void Application_Start()
{
	// initialise MvcNavigation
	var sitemap = new Node<YourController>(
		c => c.YourAction(),
			new Node<YourController>(c => c.AnotherAction())
		);
		
	NavigationConfiguration.Initialise(sitemap);
	
	// initialise routes
	var routes = RouteTable.Routes;
	routes.MapRoute("a", "", new { controller = "YourController", action = "YourAction" });
	routes.MapRoute("b", "another-action", new {controller = "YourController", action = "AnotherAction"});
}
```

Result of:

```html
@Html.Breadcrumb();
```

Will be (depending on request path of course):

```html
<a href="/">YourAction</a>
<a href="/another-action">AnotherAction</a>
```

`Breadcrumb()` can be used in child views (including nested child views).

Visit [MvcNavigation sample app](http://mvcnavigation.apphb.com) for live examples.

### Sitemap.xml
MvcNavigation has capability to generate sitemap.xml based on configured navigation structure.

Conventions:

* `priority` is auto-calculated based on node depth: root node has priority 1.0, level-1 node has priority 0.9 and so on&hellip;
* `lastmod` is not set: search engines don't use this consistenly and its usefulness is debatable
* `changefreq` is not set: search engines don't use this consistenly and its usefulness is debatable
* sitemap.xml is generated using default sitemap: currently there is no API for generating sitemap.xml from a named sitemap

There are two ways to generate sitemap.xml:
#### 1) Use XmlSitemapController
[XmlSitemapController](https://github.com/ArnoldZokas/MvcNavigation/blob/master/source/MvcNavigation/Sitemap/XmlSitemapController.cs) is a basic controller implementation with one action: "Sitemap".

To use it, simply configure an additional route in your application start logic:

```csharp
protected void Application_Start()
{
	// add route for sitemap.xml
	var routes = RouteTable.Routes;
	routes.MapRoute("Xml Sitemap", "sitemap.xml", new { controller = "XmlSitemap", action = "Sitemap" });
}
```

#### 2) Create your own controller/action and return XmlSitemapResult

This is useful when you want to set your own action filters.

Add a new action:

```csharp
[OutpuCache, Authorize, etc…]
public ActionResult Sitemap()
{
	return new XmlSitemapResult(Url);	// you need to pass an instance of UrlHelper
}
```

Configure additional route in your application start logic:

```csharp
protected void Application_Start()
{
	// add route for sitemap.xml
	var routes = RouteTable.Routes;
	routes.MapRoute("Sitemap", "sitemap.xml", new { controller = "YourController", action = "Sitemap" });
}
```

Visit [MvcNavigation sample app](http://mvcnavigation.apphb.com) for a live example.

## Advanced topics
### Multiple/named configurations
#### 1) Configuration
TODO

#### 2) Named menu
TODO

#### 3) Named breadcrumb
TODO

#### 4) Sitemap.xml
Currently, there is no API for generating sitemap.xml from a named configuration.

### Multi-level menu
TODO / include code sample

### How to change HTML output
In you MVC application, go to folder [./Views/Shared/DisplayTemplates](https://github.com/ArnoldZokas/MvcNavigation/tree/master/source/MvcNavigation.DisplayTemplates/Views/Shared/DisplayTemplates).

There you will find three C#/Razor templates installed as part of MvcNavigation:

* **MvcNavigationMenuRoot**
* **MvcNavigationMenuNode**
* **MvcNavigationBreadcrumb**

Go in and make your changes.

### How to configure configuration at runtime with DynamicNode
Sometimes certain areas of navigation configuration are only know at runtime (e.g. product catories).
In such cases, you must implement abstract class `DynamicNode<TController>` and override `CreateChildNodes` method.

Here is a sample implementation:

```csharp
public class ProductCategoryNode<TController> : DynamicNode<TController> where TController : IController
{
	public ProductCategoryNode(Expression<Action<TController>> action) : base(action)
	{
	}

	public ProductCategoryNode(Expression<Action<TController>> action, string title) : base(action, title)
	{
	}

	public override IList<INode> CreateChildNodes()
	{
		return new List<INode>
			{
				new Node<ProductController>(c => c.Category(1), title: "Category 1"),
				new Node<ProductController>(c => c.Category(2), title: "Category 2"),
				new Node<ProductController>(c => c.Category(3), title: "Category 3")
			};
	}
}

public class MvcApplication : HttpApplication
{
	protected void Application_Start()
	{
		var sitemap = new Node<YourController>(c => c.Action1(),
							new ProductCategoryNode<ProductController>(c => c.Categories())
						);

		NavigationConfiguration.Initialise(sitemap);
		
		// configure routes, etc
	}
}
```

When configuring navigation dynamically using data from an external data source (such as a database), pay close attention to performance and cache output where appropriate.

### How to configure MvcNavigation when using Areas
```csharp
public class YourAreaRegistration : AreaRegistration
{
	public override string AreaName
	{
		get { return "AreaName"; }
	}

	public override void RegisterArea(AreaRegistrationContext context)
	{
		context.MapRoute("Route1", "path1", new { controller = "YourController", action = "Action1" });
		context.MapRoute("Route2", "path2", new { controller = "YourController", action = "Action2" });
	}
}

public class MvcApplication : HttpApplication
{
	protected void Application_Start()
	{
		AreaRegistration.RegisterAllAreas();

		var sitemap = new Node<YourController, YourAreaRegistration>(c => c.Action1(),
							new Node<YourController, YourAreaRegistration>(c => c.Action2())
						);

		NavigationConfiguration.Initialise(sitemap);
	}
}
```

### Caching and performance
MvcNavigation does not cache anything internally and relies on OutputCache (you do use OutputCache, don't you?).<br />
If you need fine-grained control over caching, put your menu/breadcrumb into a child action and use OutputCache.

### Extensibility
MvcNavigation is designed to be extensible:

* **Html rendering is handled by display templates:** you have full control over what HTML gets rendered
* **Html rendering is facilitated by extension methods:** you can add your own too
* **Most of the API is public/protected and non-sealed:** inherit, implement, override any logic you need

If I have locked down a useful part of the API by mistake, please let me know I and will open it up in the next release.

### Versioning
MvcNavigation is versioned in compliance with [Semantic Versioning specification](http://semver.org/).

## How to contribute
### Feature suggestions
If you have an idea on how to make MvcNavigation better - [let me know](https://github.com/ArnoldZokas/MvcNavigation/issues).<br />
I can't promise I will build your feature, but I promise I will consider it.

### Bug fixes
I have done my best to ensure every release is bug-free.<br />
If you find a bug, [submit an issue](https://github.com/ArnoldZokas/MvcNavigation/issues) and I will get it fixed as soon as possible.

## Credits
This project was inspired by Maarten Balliauw's [MvcSiteMapProvider](https://github.com/maartenba/MvcSiteMapProvider).<br />
I started prototyping a new configuration API and ended up creating a whole new project. Because I am a developer.

## License (New BSD)
Copyright (C) 2012, Arnold Zokas<br />
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
- Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
- Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
- Neither the name of the organisation nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.