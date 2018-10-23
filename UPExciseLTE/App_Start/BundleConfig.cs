using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace UPExciseLTE
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            //// Use the development version of Modernizr to develop with and learn from. Then, when you're
            //// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/Jquery1").Include(
            //    "~/Scripts/bower_components/jquery/jquery.min.js",
            //    "~/Scripts/md5.js"));

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            bundles.Add(new StyleBundle("~/Content/cssMain").Include(
                      "~/Content/style.css",
                      "~/Content/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/plugins/simple-line-icons/simple-line-icons.min.css"));


            bundles.Add(new StyleBundle("~/Content/PublicPage").Include(
                      "~/Content/demo.css",
                      "~/Content/header-second-bar.css",
                      "~/Content/assets/pages/css/login-3.min.css"));


            bundles.Add(new StyleBundle("~/Content/cssLog").Include(
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/global/plugins/select2/css/select2.min.css",
                      "~/Content/assets/global/plugins/select2/css/select2-bootstrap.min.css",
                      "~/Content/assets/global/css/components-md.min.css",
                      "~/Content/assets/global/css/plugins-md.min.css",
                        "~/Content/assets/pages/css/login-5.min.css"
                        //,"~/Content/assets/pages/css/login.min.css"
                        ));


            bundles.Add(new StyleBundle("~/Content/cssBlank").Include(
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/global/plugins/datatables/datatables.min.css",
                      "~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css",
                      "~/Content/assets/global/plugins/bootstrap-datepicker/css/bootstrap-datepicker3.min.css",
                      "~/Content/assets/global/plugins/fancybox/source/jquery.fancybox.css",
                      "~/Content/assets/global/css/components-md.min.css",
                      "~/Content/assets/global/css/plugins-md.min.css",
                      "~/Content/assets/layouts/layout2/css/layout.min.css",
                      "~/Content/assets/layouts/layout2/css/themes/blue.min.css",
                      "~/Content/assets/layouts/layout2/css/custom.min.css",
                      "~/Content/assets/global/plugins/select2/css/select2.min.css",
                      "~/Content/assets/global/plugins/select2/css/select2-bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/cssError").Include(
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/global/css/components-md.min.css",
                      "~/Content/assets/global/css/plugins-md.min.css",
                      "~/Content/assets/pages/css/error.min.css"));

            bundles.Add(new StyleBundle("~/Content/cssFirstTimelogin").Include(
                      "~/Content/assets/global/plugins/font-awesome/css/font-awesome.min.css",
                      "~/Content/assets/global/plugins/simple-line-icons/simple-line-icons.min.css",
                      "~/Content/assets/global/plugins/bootstrap/css/bootstrap.min.css",
                      "~/Content/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/Content/assets/global/css/components-md.min.css",
                      "~/Content/assets/global/css/plugins-md.min.css",
                      "~/Content/assets/pages/css/lock-2.min.css"));


            bundles.Add(new StyleBundle("~/Content/cssDash").Include(
                      "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.css",
                      "~/Content/ assets/global/plugins/morris/morris.css",
                      "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.css" ,
                      "~/Content/assets/global/plugins/jqvmap/jqvmap/jqvmap.css"));

            bundles.Add(new StyleBundle("~/Content/ReportCss").Include(
                     "~/content/assets/global/plugins/datatables/datatables.min.css",
                     "~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.css"));



            /////////////////////////////////////////////////////////////////////


            bundles.Add(new ScriptBundle("~/bundles/bootstrapMain").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Content/plugins/jquery-1.11.0.min.js",
                      "~/Content/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/plugins/jquery.youtube.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapLog1").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Content/assets/global/plugins/jquery.min.js",
                      "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/global/plugins/js.cookie.min.js",
                      "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Content/assets/global/plugins/jquery.blockui.min.js",
                      "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapLog2").Include(                      
                      "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js",
                      "~/Content/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                      "~/Content/assets/global/plugins/select2/js/select2.full.min.js",
                      "~/Content/assets/global/plugins/backstretch/jquery.backstretch.min.js",
                      "~/Content/assets/global/scripts/app.min.js",
                      "~/Content/assets/pages/scripts/login-5.min.js"
                       //,"~/Content/pages/scripts/login.min.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapBlank").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Content/assets/global/plugins/jquery.min.js",
                      "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/global/plugins/js.cookie.min.js",
                      "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Content/assets/global/plugins/jquery.blockui.min.js",
                      "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/Content/assets/global/plugins/jquery-inputmask/jquery.inputmask.bundle.min.js",
                      "~/Content/assets/global/plugins/jquery.input-ip-address-control-1.0.min.js",
                      "~/Content/assets/global/scripts/app.min.js",
                      //"~/Content/assets/pages/scripts/dashboard.min.js",
                      "~/Content/assets/pages/scripts/dashboard.js",
                      "~/Content/assets/layouts/layout2/scripts/layout.min.js",
                      "~/Content/assets/layouts/layout2/scripts/demo.min.js",
                      "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js",
                      "~/Content/assets/layouts/global/scripts/quick-nav.min.js",
                      "~/Content/assets/pages/scripts/form-input-mask.min.js",
                      "~/Scripts/Menu.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapError").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Content/assets/global/plugins/jquery.min.js",
                      "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/global/plugins/js.cookie.min.js",
                      "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Content/assets/global/plugins/jquery.blockui.min.js",
                      "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/Content/assets/global/scripts/app.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapFirstTimelogin").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Content/assets/global/plugins/jquery.min.js",
                      "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                      "~/Content/assets/global/plugins/js.cookie.min.js",
                      "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/Content/assets/global/plugins/jquery.blockui.min.js",
                      "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/Content/assets/global/plugins/backstretch/jquery.backstretch.min.js",
                      "~/Content/assets/global/scripts/app.min.js",
                      "~/Content/assets/pages/scripts/lock-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapAngular").Include(
                      "~/Content/assets/global/plugins/angularjs/angular.min.js",
                      "~/Content/assets/global/plugins/angularjs/angular-sanitize.min.js",
                      "~/Content/assets/global/plugins/angularjs/angular-touch.min.js",
                      "~/Content/assets/global/plugins/angularjs/plugins/angular-ui-router.min.js",
                      "~/Content/assets/global/plugins/angularjs/plugins/ocLazyLoad.min.js",
                      "~/Content/assets/global/plugins/angularjs/plugins/ui-bootstrap-tpls.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrapAngularM").Include(
                      "~/Content/assets/global/scripts/app.min.js" ,
                      "~/Content/assets/layouts/layout2/scripts/layout.min.js" ,
                      "~/Content/assets/layouts/global/scripts/quick-sidebar.min.js",
                      "~/Content/assets/layouts/global/scripts/quick-nav.min.js",
                      "~/Content/assets/layouts/layout2/scripts/demo.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapDash").Include(
                  "~/Content/assets/global/plugins/moment.min.js" ,
                  "~/Content/assets/global/plugins/bootstrap-daterangepicker/daterangepicker.min.js",
                  "~/Content/assets/global/plugins/morris/morris.min.js" ,
                  "~/Content/assets/global/plugins/morris/raphael-min.js" ,
                  "~/Content/assets/global/plugins/counterup/jquery.waypoints.min.js" ,
                  "~/Content/assets/global/plugins/counterup/jquery.counterup.min.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/amcharts.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/serial.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/pie.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/radar.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/themes/light.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/themes/patterns.js" ,
                  "~/Content/assets/global/plugins/amcharts/amcharts/themes/chalk.js" ,
                  "~/Content/assets/global/plugins/amcharts/ammap/ammap.js",
                  "~/Content/assets/global/plugins/amcharts/ammap/maps/js/indiaLow.js",
                  "~/Content/assets/global/plugins/amcharts/amstockcharts/amstock.js" ,
                  "~/Content/assets/global/plugins/fullcalendar/fullcalendar.min.js" ,
                  "~/Content/assets/global/plugins/horizontal-timeline/horizontal-timeline.js",
                  "~/Content/assets/global/plugins/flot/jquery.flot.min.js",
                  "~/Content/assets/global/plugins/flot/jquery.flot.resize.min.js" ,
                  "~/Content/assets/global/plugins/flot/jquery.flot.categories.min.js" ,
                  "~/Content/assets/global/plugins/jquery-easypiechart/jquery.easypiechart.min.js",
                  "~/Content/assets/global/plugins/jquery.sparkline.min.js" ,
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/jquery.vmap.js",
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.russia.js" ,
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.world.js" ,
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.europe.js" ,
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.germany.js" ,
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/maps/jquery.vmap.usa.js" ,
                  "~/Content/assets/global/plugins/jqvmap/jqvmap/data/jquery.vmap.sampledata.js" ));

            bundles.Add(new ScriptBundle("~/bundles/ReportJs").Include(
                       //  "~/Content/plugins/jquery.min.js",
                  "~/Content/assets/global/scripts/datatable.js",
                "~/Content/assets/global/plugins/datatables/datatables.min.js"));
            //"~/content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
            //"~/content/assets/pages/scripts/table-datatables-rowreorder.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/gmap3").Include(
                     "~/Scripts/gmap3.js"));


            bundles.Add(new ScriptBundle("~/Content/PublicMain1").Include(
                //"~/Scripts/jquery-{version}.js",
                "~/Content/plugins/jquery.min.js",
                "~/Content/assets/global/plugins/bootstrap/js/bootstrap.min.js",
                "~/Content/assets/global/plugins/js.cookie.min.js",
                "~/Content/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Content/assets/global/plugins/jquery.blockui.min.js",
                "~/Content/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));

            bundles.Add(new ScriptBundle("~/Content/PublicMain2").Include(
                "~/Content/assets/global/plugins/jquery-validation/js/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/Content/PublicMain3").Include(
                "~/Content/assets/global/plugins/datatables/datatables.min.js",
                "~/Content/assets/global/plugins/datatables/plugins/bootstrap/datatables.bootstrap.js",
                "~/Content/assets/global/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js",
                "~/Content/assets/global/plugins/bootstrap-maxlength/bootstrap-maxlength.min.js",
                "~/Content/assets/global/plugins/fancybox/source/jquery.fancybox.pack.js",
                "~/Content/assets/global/plugins/jquery-validation/js/additional-methods.min.js",
                "~/Content/assets/global/plugins/backstretch/jquery.backstretch.min.js",
                "~/Content/assets/global/plugins/select2/js/select2.full.min.js",
                "~/Content/assets/pages/scripts/ecommerce-products.min.js",
                "~/Content/assets/global/scripts/app.min.js",
                "~/Content/assets/pages/scripts/login.min.js"));

            BundleTable.EnableOptimizations = false;

        }
    }
}