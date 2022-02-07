using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Routing.Contracts;
using System.Reflection;

namespace BasicWebServer.Server.Controllers
{
    public static class RoutingTableExtensions
    {

        public static IRoutingTable MapGet<TController>(
            this IRoutingTable routnigTable, string path,
            Func<TController, Response> controllerFunction)
            where TController : Controller
            => routnigTable.MapGet(path, request => controllerFunction(
                  CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>(
           this IRoutingTable routnigTable, string path,
           Func<TController, Response> controllerFunction)
           where TController : Controller
           => routnigTable.MapPost(path, request => controllerFunction(
                 CreateController<TController>(request)));

        private static TController CreateController<TController>(Request request)
            => (TController)Activator
            .CreateInstance(typeof(TController), new[] { request });

        private static Controller CreateController(Type controllerType,Request request)
        {
            var controller =(Controller)Request.ServiceCollection.CreateInstase(controllerType);

            controllerType
                .GetProperty("Request",BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(controller, request);
            return controller;
        }
    }
}