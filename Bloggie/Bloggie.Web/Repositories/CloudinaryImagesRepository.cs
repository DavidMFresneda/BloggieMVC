
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Web.Repositories
{
    public class ClodinaryImagesRepository : IImageRepository
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _config;

        public ClodinaryImagesRepository(IConfiguration config)
        {
            _config = config;
            var end_variable = _config["Cloudinary:environment_variable"];
            _cloudinary = new Cloudinary(end_variable);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var response = await UploadImage(file);

            return response;
        }


        private async Task<string> UploadImage(IFormFile file)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.Name, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            //uploadResult.JsonObj["link"] = uploadResult.JsonObj["url"];

            return uploadResult.JsonObj["url"].ToString();
        }


    }
}
