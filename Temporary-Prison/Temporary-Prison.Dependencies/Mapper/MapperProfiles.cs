using AutoMapper;
using AutoMapper.Configuration;
using System;
using Temporary_Prison.Data.MapperProfile;

namespace Temporary_Prison.Dependencies.MapperRegistry
{
    public class MapperProfiles
    {
        public static void InitialiseMappers()
        {
            
           /* Mapper.Initialize(cfg => cfg.AddProfiles(new[]
            {
                typeof(DataMapper),
            }));
            */
            Configuration.AddProfile(new DataMapper());

            Mapper.Initialize(Configuration);
        }

        public static MapperConfigurationExpression Configuration { get; } = new MapperConfigurationExpression();
    }
}
