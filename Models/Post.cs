using BlogProject.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogProject.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Blog Name")]
        public int BlogId { get; set; }

        public string BlogUserId { get; set; }

        [Required]
        [StringLength(75, ErrorMessage = "The {0} muat be at least {2} and no more than {1} characters long", MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} muat be at least {2} and no more than {1} characters long", MinimumLength = 5)]
        public string Abstract { get; set; }
        [Required]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public ReadyStatus ReadyStatus { get; set; }

        public string Slug { get; set; }

        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }

        [NotMapped] //Not to be used by the Database
        public IFormFile Image { get; set; }

        //Navigation Properties
        public virtual Blog Blog { get; set; } //parent
        public virtual BlogUser BlogUser { get;set; } //parent

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>(); //childs
        
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();



    }
}
