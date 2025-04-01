using Firebase.Auth;
using MathAPI.Models;
using MathAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MathAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        
        FirebaseAuthProvider auth;

        public AuthController()
        {
            auth = new FirebaseAuthProvider(new FirebaseConfig(Environment.GetEnvironmentVariable("FirebaseMathApp")));
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginModel login)
        {
            try
            {
                await auth.CreateUserWithEmailAndPasswordAsync(login.Email, login.Password);

                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                string currentUserId = fbAuthLink.User.LocalId;

                if (currentUserId != null)
                {
                    return Ok(new AuthResponse(currentUserId));
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                return Unauthorized(firebaseEx.error.code + " - " + firebaseEx.error.message);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
            return View();

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            try
            {
                var fbAuthLink = await auth.SignInWithEmailAndPasswordAsync(login.Email, login.Password);
                string currentUserId = fbAuthLink.User.LocalId;

                if (currentUserId != null)
                {
                    return Ok(new AuthResponse(currentUserId));
                }

            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseErrorModel>(ex.ResponseData);
                AuthLogger.Instance.LogError(firebaseEx.error.message + " - User: " + login.Email + " - IP: " + HttpContext.Connection.RemoteIpAddress
                    + " - Browser: " + Request.Headers.UserAgent);
                return Unauthorized(firebaseEx.error.code + " - " + firebaseEx.error.message);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

            return View();
        }

        [HttpPost("Logout")]
        public IActionResult LogOut()
        {            
            return Ok();
        }
        

    }
}