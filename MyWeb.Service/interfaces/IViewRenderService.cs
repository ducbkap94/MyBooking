using Microsoft.AspNetCore.Mvc;

namespace MyWeb.Service.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderViewAsync(Controller controller, string viewName, object model, bool isPartial = false);
    }
}