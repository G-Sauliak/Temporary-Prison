using AutoMapper;
using Temporary_Prison.Data.MapperProfile;

namespace Temporary_Prison.Dependencies.MapperRegistry
{
    public class MapperProfiles
    {
        public static void InitialiseMappers()
        {
            Mapper.Initialize(cfg => cfg.AddProfiles(new[] { typeof(DataMapper) }));
        }
    }
}
