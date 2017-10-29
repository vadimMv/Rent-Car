using System.Web.Mvc;
using System.Web.Routing;

namespace vadim_final_326960382_91338._16
{
    /// <summary>
    /// router configuration 
    /// </summary>
    public class RouteConfig
    {

        /// <summary>
        /// common routes definition
        /// </summary>
        /// <param name="routes">routes collection</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //   name: "AccessDenied",
            //   url: "home/accessdenied/{id}",
            //   defaults: new { controller = "Home", action = "AccessDenied", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
               name: "UserLogin",
               url: "user/login/{id}",
               defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional }
           );

            //routes.MapRoute(
            //    name: "User",
            //    url: "user/all/{id}",
            //    defaults: new { controller = "User", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "UserCreate",
                url: "user/create/{id}",
                defaults: new { controller = "User", action = "Create", id = UrlParameter.Optional }
            );

            // routes.MapRoute(
            //    name: "UserGet",
            //    url: "user/details/{id}",
            //    defaults: new { controller = "User", action = "Details", id = UrlParameter.Optional }
            //);

            //  routes.MapRoute(
            //    name: "UserDel",
            //    url: "user/delete/{id}",
            //    defaults: new { controller = "User", action = "Delete", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "UserUpd",
            //    url: "user/update/{id}",
            //    defaults: new { controller = "User", action = "Edit", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
               name: "Rent",
               url: "rent/order/{id}",
               defaults: new { controller = "Rent", action = "Order", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "Success",
            url: "rent/success/{id}",
            defaults: new { controller = "Rent", action = "Success", id = UrlParameter.Optional }
        );

            routes.MapRoute(
            name: "JsonCarDate",
            url: "cars/cardata/{id}",
            defaults: new { controller = "Cars", action = "CarData", id = UrlParameter.Optional }
      );
            routes.MapRoute(
            name: "JsonBranch",
            url: "cars/branches/{id}",
            defaults: new { controller = "Cars", action = "Branches", id = UrlParameter.Optional }
     );
            routes.MapRoute(
            name: "JsonUserDate",
            url: "user/data/{id}",
           defaults: new { controller = "User", action = "UserData", id = UrlParameter.Optional }
    );
            routes.MapRoute(
            name: "JsonCarsDate",
            url: "cars/rentalcardata/{id}",
            defaults: new { controller = "Cars", action = "RentalCarData", id = UrlParameter.Optional }
    );
            routes.MapRoute(
            name: "JsonModel",
            url: "cars/carmodel/{id}",
            defaults: new { controller = "Cars", action = "CarModel", id = UrlParameter.Optional }
    );
        }
    }
}
