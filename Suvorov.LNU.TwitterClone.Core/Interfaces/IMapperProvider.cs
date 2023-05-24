using AutoMapper;

namespace Suvorov.LNU.TwitterClone.Core.Interfaces
{
    public interface IMapperProvider
    {
        IMapper GetMapper();
    }
}