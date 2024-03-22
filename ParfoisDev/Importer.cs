namespace ParfoisDev
{
    public static class Importer
    {
        public static void Use(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapAreaControllerRoute(name: "area_controllers", areaName: "controller",
                  pattern: "controllers/{controller}/{action}/{id?}", defaults: new { controller = "Home", action = "Index" });

        }
    }
}
