﻿using System.Web;
using System.Web.Optimization;

namespace RockScissorPaper
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/global").Include(
                        "~/Scripts/Lib/jquery-{version}.js",
                        "~/Scripts/Lib/Api.js",
                        "~/Scripts/Lib/underscore.js",
                        "~/Scripts/Roshambo/Roshambo.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.unobtrusive*",
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Lib/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                "~/Content/Css/Site.css",
                "~/Content/Css/Wrapper960Static.css"));

          
        }
    }
}