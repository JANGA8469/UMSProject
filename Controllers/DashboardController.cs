using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UMSProject.ViewModels;

namespace UMSProject.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            DashboardViewModel model = new DashboardViewModel
            {
                TotalUsers = _userManager.Users.Count(),
                ActiveUsers = _userManager.Users.Count(),
                TotalRoles = _roleManager.Roles.Count()
            };

            return View(model);
        }
    }
}