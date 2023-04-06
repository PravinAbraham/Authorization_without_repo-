using System.ComponentModel.DataAnnotations;

namespace Authorizations.Models
{
    public class JWTLogin
    {
        [EmailAddress]
        public string Email { get; set; }
        public string password { get; set; }
    }
}
