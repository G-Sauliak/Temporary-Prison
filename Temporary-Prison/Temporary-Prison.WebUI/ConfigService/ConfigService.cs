using System.Configuration;
using Temporary_Prison.Business.Infrastructure;

namespace Temporary_Prison.WebUI.SiteConfigService
{
    public class ConfigService : IConfigService
    {
        public string PhotoPath
        {
            get
            {
                return ConfigurationManager.AppSettings["PrisonerPhotoPath"];
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoPath"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoPath"] = DefaultConfig.DefaultPhotoPath;
                }
            }
        }

        public int ImageMaxSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MaxPhotoSize"]);
            }

            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["MaxPhotoSize"] = value.ToString();
                }

                else
                {
                    ConfigurationManager.AppSettings["MaxPhotoSize"] = DefaultConfig.DefaultMaxSize.ToString();
                }
            }
        }


        public string AllowedPhotoTypes
        {
            get
            {
                return ConfigurationManager.AppSettings["AllowedPhotoTypes"];
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["AllowedPhotoTypes"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["AllowedPhotoTypes"] = DefaultConfig.DefaultAllowedPhotoTypes;
                }
            }
        }

        public int AvatarHeight
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrisonerAvatarHeight"]);
            }

            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["PrisonerAvatarHeight"] = value.ToString();
                }
                else
                {
                    ConfigurationManager.AppSettings["PrisonerAvatarHeight"] = DefaultConfig.DefaultAvatarHeight.ToString();
                }
            }
        }

        public int AvatarWidth
        {

            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrisonerAvatarWidth"]);
            }
            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["PrisonerAvatarWidth"] = value.ToString();
                }
                else
                {
                    ConfigurationManager.AppSettings["PrisonerAvatarWidth"] = DefaultConfig.DefaultAvatarHeight.ToString();
                }
            }

        }

        public int PrisonerPagedSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrisonerPagedSize"]);
            }

            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["PrisonerPagedSize"] = value.ToString();
                }

                else
                {
                    ConfigurationManager.AppSettings["PrisonerPagedSize"] = DefaultConfig.PrisonerPagedSize.ToString();
                }
            }
        }

        public int UserPagedSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["UserPagedSize"]);
            }

            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["UserPagedSize"] = value.ToString();
                }

                else
                {
                    ConfigurationManager.AppSettings["UserPagedSize"] = DefaultConfig.UserPagedSize.ToString();
                }
            }
        }

        public string DefaultPhotoOfPrisoner
        {
            get
            {
                return ConfigurationManager.AppSettings["defaultPhotoOfPrisoner"];
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["defaultPhotoOfPrisoner"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["defaultPhotoOfPrisoner"] = DefaultConfig.DefaultPhotoOfPrisonerPath;
                }
            }
        }

        public string ContentPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ContentPath"];
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["ContentPath"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["ContentPath"] = DefaultConfig.ContentPath;
                }
            }
        }

        public string DefaultNoAvatar
        {
            get
            {
                return ConfigurationManager.AppSettings["no-avatar"];
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["no-avatar"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["no-avatar"] = DefaultConfig.DefaultNoAvatarPath;
                }
            }
        }

        public int PhotoHeight
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrisonerPhotoHeight"]);
            }

            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoHeight"] = value.ToString();
                }
                else
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoHeight"] = DefaultConfig.DefaultAvatarHeight.ToString();
                }
            }
        }

        public int PhotoWidth
        {

            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PrisonerPhotoWidth"]);
            }
            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoWidth"] = value.ToString();
                }
                else
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoWidth"] = DefaultConfig.DefaultAvatarHeight.ToString();
                }
            }

        }
        public string AvatarPath
        {
            get
            {
                return ConfigurationManager.AppSettings["AvatarPath"];
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["AvatarPath"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["AvatarPath"] = DefaultConfig.ContentPath;
                }
            }
        }
    }
}