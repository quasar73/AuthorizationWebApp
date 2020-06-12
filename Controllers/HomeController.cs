using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthorizationWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AuthorizationWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationWebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;

		public HomeController(UserManager<User> userManager, SignInManager<User> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[Authorize]
		[HttpGet]
		public IActionResult Index()
		{
			List<bool> checkList = new List<bool>();
			for (int i = 0; i < userManager.Users.Count(); i++)
				checkList.Add(false);
			TableViewModel tableViewModel = new TableViewModel()
			{
				Users = userManager.Users.ToList(),
				checkList = checkList
			};
			return View(tableViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(TableViewModel tableViewModel)
		{
			if ((await userManager.FindByEmailAsync(User.Identity.Name)).Status == true)
				return Logout();
			else
				return DoTableAction(tableViewModel).Result;
		}

		private async Task<IActionResult> DoTableAction(TableViewModel tableViewModel)
		{
			if (ModelState.IsValid)
				if (tableViewModel.OperationType == 1)
					return await UnblockUsers(tableViewModel.checkList);
				else if (tableViewModel.OperationType == 2)
					return await BlockUsers(tableViewModel.checkList);
				else if (tableViewModel.OperationType == 3)
					return await DeleteUsers(tableViewModel.checkList);
			return Index();
		}

		public IActionResult Logout()
		{
			signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		private async Task<IActionResult> UnblockUsers(List<bool> checkList)
		{
			bool isSelfBlocked = false;
			for (int i = 0; i < checkList.Count; i++)
			{
				if (checkList[i])
				{
					var user = userManager.Users.ToList()[i];
					user.Status = true;
					await userManager.UpdateAsync(user);

					if (user.UserName == User.Identity.Name)
						isSelfBlocked = true;
				}
			}
			if (isSelfBlocked)
				return Logout();
			return Index();
		}

		private async Task<IActionResult> BlockUsers(List<bool> checkList)
		{
			for (int i = 0; i < checkList.Count; i++)
				if (checkList[i])
				{
					userManager.Users.ToList()[i].Status = false;
					await userManager.UpdateAsync(userManager.Users.ToList()[i]);
				}
			return Index();
		}

		private async Task<IActionResult> DeleteUsers(List<bool> checkList)
		{
			bool isSelfDeleted;
			foreach (var user in BuildDeleteUsersList(checkList, out isSelfDeleted))
				await userManager.DeleteAsync(user);
			if (isSelfDeleted)
				return Logout();

			return Index();
		}
		
		private List<User> BuildDeleteUsersList(List<bool> checkList, out bool isSelfDeleted)
		{
			isSelfDeleted = false;
			List<User> deleteUsers = new List<User>();
			for (int i = 0; i < checkList.Count; i++)
				if (checkList[i] == true)
				{
					deleteUsers.Add(userManager.Users.ToList()[i]);
					if (userManager.Users.ToList()[i].Email == User.Identity.Name)
						isSelfDeleted = true;
				}
			return deleteUsers;
		}
	}
}
