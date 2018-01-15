using System.Configuration;
using Temporary_Prison.Business.Infrastructure;

namespace Temporary_Prison.Business.SiteConfigService
{
    public class ConfigService : IConfigService
    {
        public string PrisonerPhotoPath
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

        public int MaxPhotoSize
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

        public int PhotoHeight
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
                    ConfigurationManager.AppSettings["PrisonerAvatarHeight"] = DefaultConfig.DefaultPhotoHeight.ToString();
                }
            }
        }

        public int PhotoWidth
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
                    ConfigurationManager.AppSettings["PrisonerAvatarWidth"] = DefaultConfig.DefaultPhotoHeight.ToString();
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

        public string DefaultPhotoOfPrisonerPath
        {
            get
            {
                return ConfigurationManager.AppSettings["defaultPhotoOfPrisonerPath"];
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    ConfigurationManager.AppSettings["defaultPhotoOfPrisonerPath"] = value;
                }
                else
                {
                    ConfigurationManager.AppSettings["defaultPhotoOfPrisonerPath"] = DefaultConfig.DefaultPhotoOfPrisonerPath;
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
                    ConfigurationManager.AppSettings["no-avatar"] = DefaultConfig.DefaultNoAvatar;
                }
            }
        }

    
    }
}