using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MyWeb.Service.Interfaces;

namespace MyWeb.Service.Services
{
    public class ViewRenderService : IViewRenderService
    {
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public ViewRenderService(
            ICompositeViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider)
        {
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
        }

        public async Task<string> RenderViewAsync(Controller controller, string viewName, object model, bool isPartial = false)
        {
            var actionContext = new ActionContext(controller.HttpContext, controller.RouteData, controller.ControllerContext.ActionDescriptor);

            var viewResult = isPartial
                ? _viewEngine.FindView(actionContext, viewName, false)
                : _viewEngine.GetView(null, viewName, false);

            if (!viewResult.Success)
            {
                throw new InvalidOperationException($"Không tìm thấy view: {viewName}");
            }

            var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            using var sw = new StringWriter();
            var viewContext = new ViewContext(
                actionContext,
                viewResult.View,
                viewDictionary,
                new TempDataDictionary(controller.HttpContext, _tempDataProvider),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return sw.ToString();
        }
    }
}