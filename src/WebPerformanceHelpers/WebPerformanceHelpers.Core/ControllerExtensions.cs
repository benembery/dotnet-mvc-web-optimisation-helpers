using System.IO;
using System.Web.Mvc;

namespace WebPerformanceHelpers.Core
{
    public static class RenderViewExtensions
    {
        public static string RenderVew(this ControllerContext controllerContext, [AspMvcView]string viewName, [AspMvcModelType]object model)
        {
            return RenderView(controllerContext.Controller, viewName, model, false);
        }

        public static string RenderView(this ControllerBase controller, [AspMvcView]string viewName, [AspMvcModelType]object model)
        {
            return RenderView(controller, viewName, model, false);
        }

        public static string RenderPartialView(this ControllerContext controllerContext, [AspMvcPartialView]string viewName, [AspMvcModelType]object model)
        {
            return RenderView(controllerContext.Controller, viewName, model, true);
        }

        public static string RenderPartialView(this ControllerBase controller, [AspMvcPartialView]string viewName, [AspMvcModelType]object model)
        {
            return RenderView(controller, viewName, model, true);
        }

        private static string RenderView(ControllerBase controller, string viewName, object model, bool isPartial)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            var view = GetView(controller.ControllerContext, viewName, isPartial);

            var viewData = controller.ViewData ?? new ViewDataDictionary();

            viewData.Model = model;

            return RenderView(controller.ControllerContext, view, viewData, controller.TempData);
        }

        private static ViewEngineResult GetView(ControllerContext context, string viewName, bool isPartial)
        {
            return isPartial
                ? ViewEngines.Engines.FindPartialView(context, viewName)
                : ViewEngines.Engines.FindView(context, viewName, null);
        }

        private static string RenderView(ControllerContext controllerContext, ViewEngineResult viewResult, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            if (viewResult == null || controllerContext == null)
                return string.Empty;

            if (viewData == null)
                viewData = new ViewDataDictionary();


            if (tempData == null)
                tempData = new TempDataDictionary();

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(controllerContext, viewResult.View, viewData, tempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}