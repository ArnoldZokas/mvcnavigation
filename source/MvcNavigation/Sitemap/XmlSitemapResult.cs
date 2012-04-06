// # Copyright © 2012, Arnold Zokas
// # All rights reserved. 

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MvcNavigation.Sitemap
{
	public class XmlSitemapResult : FileResult
	{
		const string MimeType = "application/xml";
		static readonly XNamespace Namespace = XNamespace.Get("http://www.sitemaps.org/schemas/sitemap/0.9");
		readonly INode _sitemap;
		readonly UrlHelper _urlHelper;

		public XmlSitemapResult(UrlHelper urlHelper) : base(MimeType)
		{
			_urlHelper = urlHelper;
			_sitemap = NavigationConfiguration.DefaultSitemap;

			if (_sitemap == null)
				throw new InvalidOperationException("MvcNavigation is not initialised.");
		}

		protected override void WriteFile(HttpResponseBase response)
		{
			var rootElement = new XElement(Namespace + "urlset");

			var navigationItems = FlattenTree(_sitemap);
			foreach (var navigationItem in navigationItems)
			{
				rootElement.Add(new XElement(Namespace + "url",
				                             new XElement(Namespace + "loc", _urlHelper.RouteAbsoluteUrl(navigationItem.Item1)),
				                             new XElement(Namespace + "priority", navigationItem.Item2.ToString("0.0"))
				                	)
					);
			}

			var xmlSitemap = new XDocument();
			xmlSitemap.Declaration = new XDeclaration("1.0", "utf-16", standalone: null);
			xmlSitemap.Add(rootElement);
			xmlSitemap.Save(response.Output);

			response.End();
		}

		static IEnumerable<Tuple<INode, double>> FlattenTree(INode rootNode)
		{
			var nodes = new List<Tuple<INode, double>> { new Tuple<INode, double>(rootNode, 1.0f) };

			FlattenNode(nodes, rootNode, 0.9f);

			return nodes;
		}

		static void FlattenNode(ICollection<Tuple<INode, double>> nodes, INode rootNode, double priority)
		{
			foreach (var childNode in rootNode.Children)
			{
				nodes.Add(new Tuple<INode, double>(childNode, priority));
				FlattenNode(nodes, childNode, priority - 0.1);
			}
		}
	}
}