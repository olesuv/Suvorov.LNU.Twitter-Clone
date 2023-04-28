using Suvorov.LNU.TwitterClone.Models.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Suvorov.LNU.TwitterClone.Models.Frontend
{
    public class CreatePostRequest
    {
        [MaxLength(100)]
        [Required(ErrorMessage = "No text content!")]
        public string? TextContent { get; set; }

        public byte[]? ImageContent { get; set; }

        public DateTime? PostDate { get; set; }

        public virtual User? User { get; set; }
    }
}
