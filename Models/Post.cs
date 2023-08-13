using System.ComponentModel.DataAnnotations;

namespace BlogProject.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "Blog Name")]
        public int BlogId { get; set; }

        public string AuthorId { get; set; }

    }
}
