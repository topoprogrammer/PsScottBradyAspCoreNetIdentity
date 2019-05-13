using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentity.Models
{
    public class TwoFactorModel
    {
        [Required]
        public string Token { get; set; }
    }
}