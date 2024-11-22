namespace Bloggie.Web.Models.View
{
    public class HomeViewModel
    {

        public IEnumerable<Models.Domain.BlogPost> BlogPosts { get; set; }
        public IEnumerable<Models.Domain.Tag> Tags { get; set; }

    }
}
