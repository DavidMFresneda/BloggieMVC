namespace Bloggie.Web.Models.View
{
    public class AddLikeRequest
    {

        public Guid UserId { get; set; }
        public Guid BlogPostId { get; set; }
    }
}
