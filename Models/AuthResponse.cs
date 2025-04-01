using System.ComponentModel.DataAnnotations;

namespace MathAPI.Models
{
    public class AuthResponse
    {
        [Required]
        public string Token { get; set; }
        
        public AuthResponse(string token){
            this.Token = token;
        }
    }
}