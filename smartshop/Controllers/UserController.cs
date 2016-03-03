using System.Linq;
using System.Web.Mvc;
using VideokeRental.Models;

namespace VideokeRental.Controllers
{
    public class UserAccountController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User != null)
            {
                var context = new ApplicationDbContext();
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {
                    var user = context.Users.SingleOrDefault(u => u.UserName == username);
                    string fullName = string.Concat(new string[] { user.FullName });
                    string id = string.Concat(new string[] { user.Id });
                    string email = string.Concat(new string[] { user.Email });
                    string userName = string.Concat(new string[] { user.UserName });
                    string street = string.Concat(new string[] { user.Street });
                    string town = string.Concat(new string[] { user.Town });
                    string city = string.Concat(new string[] { user.City });
                    string role = string.Concat(new string[] { user.Role.ToString() });


                    ViewData.Add("UserId", id);
                    ViewData.Add("FullName", fullName);
                    ViewData.Add("Email", email);
                    ViewData.Add("UserName", userName);
                    //ViewData.Add("Street", street);
                    //ViewData.Add("Town", town);
                    //ViewData.Add("City",city);
                    ViewData.Add("Address", street + town + city);
                    ViewData.Add("Role", role);
                }
            }
            base.OnActionExecuted(filterContext);
        }
        public UserAccountController()
        { }
    }
}