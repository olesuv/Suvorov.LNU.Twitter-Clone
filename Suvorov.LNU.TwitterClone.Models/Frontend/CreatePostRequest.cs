using Suvorov.LNU.TwitterClone.Models.Database;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Suvorov.LNU.TwitterClone.Models.Frontend
{
    public class CreatePostRequest
    {
        [Required]
        [StringLength(50)]
        public string TextContent { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
