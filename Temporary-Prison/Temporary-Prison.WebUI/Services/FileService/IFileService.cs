using System.Web;

namespace Temporary_Prison.Services.FileService
{
    public interface IFileService
    {
        bool UploadFile(HttpPostedFileBase fileBase, string fileName);
        void RemoveFile(string fileName);
    }
}
