using System.Web.Mvc;

namespace vadim_final_326960382_91338._16.Areas.Staff
{

    /// <summary>
    /// staff area routing 
    /// </summary>
    public class StaffAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Staff";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Staff_default",
                "Staff/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Staff_Free",
                "staff/worker/free/{id}",
                new { action = "Free", id = UrlParameter.Optional }
            );

            context.MapRoute(
               "Staff_Edit",
               "staff/worker/edit/{id}",
               new { action = "Edit", id = UrlParameter.Optional }
           );

            context.MapRoute(
              "Staff_Price",
              "staff/worker/price/{id}",
              new { action = "Price", id = UrlParameter.Optional }
          );
        }
    }
}