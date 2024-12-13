using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Models.View
{
    public class BlogDetailsViewModel
    {

        public BlogPost BlogPost { get; set; }
        public int NumLikes { get; set; }
        public string? CommentDescription { get; set; }
        public Guid UserId { get; set; }

        public IEnumerable<BlogComment> Comentarios { get; set; }

    }
}
