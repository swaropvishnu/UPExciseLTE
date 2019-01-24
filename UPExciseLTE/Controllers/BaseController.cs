using System.Web.Mvc;
using UPExciseLTE.Filters;


namespace UPExciseLTE.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// we inherit all controllers from this basecontroller.
        /// basicly we access usercontext data from all controllers by user variable
        /// User.FirstName + " " + User.LastName
        /// </summary>
        protected virtual new CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}