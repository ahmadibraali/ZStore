using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ZStore.Application.Interfaces;
using ZStore.Core;
using ZStore.DTO;

namespace ZStore.MVC.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository userRepository;
		private readonly IMapper mapper;
		private readonly UserManager<CustomUser> userManager;
		private readonly SignInManager<CustomUser> signInManager;

		public UserController(IUserRepository _userRepository, IMapper _mapper
			, UserManager<CustomUser> _userManager
			, SignInManager<CustomUser> _signInManager)
        {
			userRepository = _userRepository;
			mapper = _mapper;
			userManager = _userManager;
			signInManager = _signInManager;
		}
		public IActionResult Index()
		{
			return View();
		}
		public async Task<IActionResult> Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginUserViewModel userLogin)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByNameAsync(userLogin.userName);
				if (user != null)
				{
					var result = await signInManager.PasswordSignInAsync(user, userLogin.Password, userLogin.RememberMe, false);
					if (result.Succeeded)
						return RedirectToAction("Index", "Home");
				}
			}
			return View(userLogin);
		}
		public IActionResult Register()
		{
			RegisteredUserViewModel userViewModel = new RegisteredUserViewModel();

			return View(userViewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisteredUserViewModel _userVM)
		{
			if (ModelState.IsValid)
			{

				//var usr = await _userServices.Create(user);
				var user = mapper.Map<CustomUser>(_userVM);
				IdentityResult result = await userManager.CreateAsync(user, _userVM.PasswordHash);
				if (result.Succeeded)
				{
					var id = await userManager.GetUserIdAsync(user);
					var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Name, user.UserName),
							new Claim(ClaimTypes.NameIdentifier,id)
						};

					//await _userManager.AddToRoleAsync(user, "Admin");
					//await _signInManager.SignInAsync(user,false);
					await signInManager.SignInWithClaimsAsync(user, false, claims);
					return RedirectToAction("login");
				}
				else
				{
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError("", item.Description);
					}
				}
			}
			
			return View(_userVM);
		}
		public async Task<IActionResult> LogOut()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Login");
		}


	}
}
