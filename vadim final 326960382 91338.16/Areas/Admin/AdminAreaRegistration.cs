using System.Web.Mvc;

namespace vadim_final_326960382_91338._16.Areas.Admin
{   
    /// <summary>
    ///Admin area router 
    /// </summary>
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "ManageRent", action = "Index", id = UrlParameter.Optional }
            );

           // context.MapRoute(
           //    "Admin_Index",
           //    "admin/managerent/index/{id}",
           //    new { action = "Index", id = UrlParameter.Optional }
           //);
        }
    }
}