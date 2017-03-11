using AutoMapper;

namespace HomeMadeFood.Web.Common.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}