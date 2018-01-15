using Temporary_Prison.Common.Models;
using System.Web;

namespace Temporary_Prison.Business.PrisonManager
{
    public interface IPrisonManager
    {
        void AddPrisoner(Prisoner prisoner, HttpPostedFileBase postImage, out int newId);
        void EditPrisoner(Prisoner prisoner);
        void DeletePrisoner(int id);
        void ReleaseOfPrisoner(ReleaseOfPrisoner release);
        void RegisterDetention(RegistDetention registDetention);
        void EditDetention(Detention detention);
        void DeleteDetention(int id);
    }
}
