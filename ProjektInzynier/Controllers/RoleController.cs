using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjektInzynier.Models;

namespace ProjektInzynier.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<string> roleNames = _roleManager.Roles.Select(q => q.Name).ToList();

            return View(roleNames);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string roleName)
        {
            var role = new IdentityRole(roleName);

            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                TempData["Message"] = $"Dodano rolę {roleName}";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["Message"] = $"Nie udało się utworzyć roli";
            }

            return View();
        }

        public async Task<IActionResult> Remove(string roleName = "")
        {
            if (!string.IsNullOrEmpty(roleName))
            {
                var role = _roleManager.Roles.SingleOrDefault(q => q.Name == roleName);

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    TempData["Message"] = $"Usunięto rolę {roleName}";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Message"] = $"Nie udało się utworzyć roli";
                }
            }
            List<string> roleNames = _roleManager.Roles.Select(q => q.Name).ToList();
            return View(roleNames);
        }

        public IActionResult Edit(string roleName)
        {
            var identityRole = _roleManager.Roles.SingleOrDefault(q => q.Name == roleName);

            return View(identityRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            var dbRole = _roleManager.Roles.SingleOrDefault(q => q.Id == model.Id);
            dbRole.Name = model.Name;
            dbRole.NormalizedName = model.Name.ToUpper();

            var result = await _roleManager.UpdateAsync(dbRole);

            if (result.Succeeded)
            {
                TempData["Message"] = "Zaktualizowano rolę";
            }
            else
            {
                TempData["Message"] = "Niezaktualizowano roli. Wystąpił błąd.";

                return View(model);
            }

            return RedirectToAction("Index");
        }

        public IActionResult AddToRole()
        {
            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();
            List<IdentityUser> identityUsers = _userManager.Users.ToList();

            var model = new AddToRoleViewModel
            {
                IdentityRoles = identityRoles,
                IdentityUsers = identityUsers
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddToRoleViewModel model)
        {
            var identityUser = _userManager.Users.SingleOrDefault(q => q.Id == model.SelectedIdentityUserId);
            var identityRole = _roleManager.Roles.SingleOrDefault(q => q.Id == model.SelectedIdentityRoleId);

            var result = await _userManager.AddToRoleAsync(identityUser, identityRole.Name);

            if (result.Succeeded)
            {
                TempData["Message"] = $"Dodano rolę {identityRole.Name} do użytkownika {identityUser.UserName}";

                return RedirectToAction("Index");
            }

            TempData["Message"] = $"Wystąpił błąd przy dodawaniu roli do użytkownika";


            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();
            List<IdentityUser> identityUsers = _userManager.Users.ToList();

            model.IdentityRoles = identityRoles;
            model.IdentityUsers = identityUsers;

            return View(model);
        }
    }
}
