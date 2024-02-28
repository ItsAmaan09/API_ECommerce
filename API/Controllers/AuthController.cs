using Microsoft.AspNetCore.Mvc;
using Manager;
using Models;
namespace API.Controllers
{


	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		[HttpPost]
		[Route("Login")]
		public IActionResult Authenticate(AuthenticationRequest authenticationRequest)
		{
			try
			{
				UserManager userManager = new UserManager();
				var authInfo = userManager.IsUserNameExists(authenticationRequest.userName);
				if (authInfo == null || !userManager.IsPasswordMatch(authInfo.PasswordHash, authenticationRequest.password))
				{
					return Unauthorized(new { message = "Invalid credentials" });
				}
				return Ok(authInfo);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
	}
}
