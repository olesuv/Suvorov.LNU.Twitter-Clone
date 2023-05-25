using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Suvorov.LNU.TwitterClone.Models.Database;

namespace Suvorov.LNU.TwitterClone.Models.Frontend
{
    public class CreatePostRequest
    {
        [MaxLength(250)]
        [Required(ErrorMessage = "Please type something.")]
        public string? TextContent { get; set; }

        public byte[]? ImageContent { get; set; }

        public DateTime? PostDate { get; set; }

        public int? LikesAmount { get; set; }

        public virtual User? User { get; set; }

        public virtual List<Like>? Likes { get; set; }

        public virtual List<Comment>? Comments { get; set; }

        public virtual List<PostTag>? Tags { get; set; }
    }
}
