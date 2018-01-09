using System.Configuration;
using Temporary_Prison.Infrastructure;

namespace Temporary_Prison.SiteConfigService
{
    public class ConfigService : IConfigService
    {
        public string PhotoImagePath
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
                return int.Parse(ConfigurationManager.AppSettings["PrisonerPhotoHeight"]);
            }

            set
            {
                if (value != default(int))
                    ConfigurationManager.AppSettings["PrisonerPhotoHeight"] =
                        value.ToString();
                else ConfigurationManager.AppSettings["PrisonerPhotoHeight"] = DefaultConfig.DefaultPhotoHeight.ToString();
            }
        }

        public int PhotoWidth
        {

            get { return int.Parse(ConfigurationManager.AppSettings["PrisonerPhotoWidth"]); }

            set
            {
                if (value != default(int))
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoWidth"] =
                        value.ToString();
                }
                else
                {
                    ConfigurationManager.AppSettings["PrisonerPhotoWidth"] = DefaultConfig.DefaultPhotoHeight.ToString();
                }
            }

        }
    }
}