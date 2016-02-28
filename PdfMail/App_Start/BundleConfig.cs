using System.Web.Optimization;

namespace PdfMail
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/Styles/app").Include(
				"~/Content/Site.css",
				"~/Content/toastr.min.css",
				"~/Content/nprogress.css"
				));

			bundles.Add(new ScriptBundle("~/Scripts/app").Include(
				"~/Scripts/vendor/jquery-1.12.1.min.js",
				"~/Scripts/vendor/jspdf.min.js",
				"~/Scripts/vendor/toastr.min.js",
				"~/Scripts/vendor/nprogress.js",
				"~/Scripts/app/pdfmail.app.js"
				));
		}
	}
}
