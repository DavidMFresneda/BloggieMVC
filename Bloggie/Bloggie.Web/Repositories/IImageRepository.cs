namespace Bloggie.Web.Repositories
{
    public interface IImageRepository
    {

        Task<string> UploadImageAsync(IFormFile file);

    }
}
