using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Strive.BusinessEntities;
//using Strive.BusinessLogic;
//using Strive.BusinessLogic.Auth;
//using Strive.Common;
//using System.Threading.Tasks;
//using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    public class AuthExternalController : ControllerBase
    {

        //[HttpGet("/Admin/LoginExternal")]
        //public IActionResult LoginExternal(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider.
        //    string redirectUrl = Url.Action(nameof(SigninExternalCallback), "Auth", new { returnUrl });
        //    Microsoft.AspNetCore.Authentication.AuthenticationProperties properties = _authManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        //    return Challenge(properties, provider);
        //}

        //[HttpPost("/Admin/RegisterExternalUser")]
        //public async Task<IActionResult> ExternalUserRegistration([FromBody] RegistrationUserDTO registrationUser)
        //{
        //    //string identityExternalCookie = Request.Cookies["Identity.External"];//do we have the cookie??

        //    if (ModelState.IsValid)
        //    {
        //        // Get the information about the user from the external login provider
        //        ExternalLoginInfo info = await _authManager.GetExternalLoginInfoAsync();

        //        if (info == null) return BadRequest("Error registering external user.");

        //        CredentialsDTO credentials = await _authService.RegisterExternalUser(registrationUser, info);
        //        return Ok(credentials);
        //    }

        //    return BadRequest();
        //}

        //// GET: api/auth/signinexternalcallback
        //[HttpGet("loginexternalcallback")]
        //public async Task<IActionResult> SigninExternalCallback(string returnUrl = null, string remoteError = null)
        //{
        //    //string identityExternalCookie = Request.Cookies["Identity.External"];//do we have the cookie??

        //    ExternalLoginInfo info = await _authManager.GetExternalLoginInfoAsync();

        //    if (info == null) return new RedirectResult($"{returnUrl}?error=externalsigninerror");

        //    // Sign in the user with this external login provider if the user already has a login.
        //    Microsoft.AspNetCore.Identity.SignInResult result =
        //        await _authManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        //    //if (result.Succeeded)
        //    //{
        //    //    CredentialsDTO credentials = _authManager.ExternalSignIn(info);
        //    //    return new RedirectResult($"{returnUrl}?token={credentials.JWTToken}");
        //    //}

        //    //if (result.IsLockedOut)
        //    //{
        //    //    return new RedirectResult($"{returnUrl}?error=lockout");
        //    //}
        //    //else
        //    //{
        //    //    // If the user does not have an account, then ask the user to create an account.

        //    //    string loginprovider = info.LoginProvider;
        //    //    string email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //    //    string name = info.Principal.FindFirstValue(ClaimTypes.GivenName);
        //    //    string surname = info.Principal.FindFirstValue(ClaimTypes.Surname);

        //    //    return new RedirectResult($"{returnUrl}?error=notregistered&provider={loginprovider}" +
        //    //        $"&email={email}&name={name}&surname={surname}");
        //    //}
        //    return null;
        //}
      
    }
}