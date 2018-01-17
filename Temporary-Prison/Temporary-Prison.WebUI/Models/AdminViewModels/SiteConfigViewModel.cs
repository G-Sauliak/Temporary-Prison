using System.ComponentModel.DataAnnotations;

namespace Temporary_Prison.Models
{
    public class SiteConfigViewModel
    {
        [Display(Name = "Max allowed image size (in kb)")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? MaxPhotoSize { get; set; }

        [Display(Name = "Acceptable image types")]
        public string AllowedPhotoTypes { get; set; }

        [Display(Name = "Post pohto of prisoner height")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PhotoHeight { get; set; }

        [Display(Name = "Post pohto of prisoner width")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PhotoWidth { get; set; }

        [Display(Name = "Prisoner paged size")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? PrisonerPagedSize { get; set; }

        [Display(Name = "User paged size")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? UserPagedSize { get; set; }

        [Display(Name = "Post photo of prisoner folder")]
        public string PhotoPath { get; set; }

        [Display(Name = "Avatar image height")]
        [RegularExpression(@"\d*", ErrorMessage = "Please enter a numeric value")]
        public int? AvatarHeight { get; set; }

        [Display(Name = "Avatar image width")]
        public int? AvatarWidth { get; set; }

        [Display(Name = "Avatar image folder")]
        public string AvatarPath { get; set; }

        [Display(Name = "Content path")]
        public string ContentPath { get; set; }

        [Display(Name = "Defaul pthoto of prisoner")]
        public string DefaultPhotoOfPrisoner{ get; set; }

        [Display(Name = "Defaul avatar of prisoner")]
        public string DefaultNoAvatar { get; set; }
    }
}
