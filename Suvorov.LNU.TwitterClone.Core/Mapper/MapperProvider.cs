using AutoMapper;
using Suvorov.LNU.TwitterClone.Models.Database;
using Suvorov.LNU.TwitterClone.Models.Frontend;
using Suvorov.LNU.TwitterClone.Core.Interfaces;

namespace Suvorov.LNU.TwitterClone.Core.Mapper
{
    public class MapperProvider : IMapperProvider
    {
        private readonly IMapper _mapper;

        public MapperProvider()
        {
            _mapper = Initialize();
        }

        public IMapper GetMapper() => _mapper;

        private IMapper Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, User>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                    .ForMember(dest => dest.EmailAddress, opt => opt.MapFrom(src => src.EmailAddress))
                    .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
                    .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
                    .ForMember(dest => dest.PhoneNumber, opt => opt.Ignore())
                    .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

                cfg.CreateMap<CreatePostRequest, Post>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the Id property if it's not needed
                    .ForMember(dest => dest.TextContent, opt => opt.MapFrom(src => src.TextContent))
                    .ForMember(dest => dest.ImageContent, opt => opt.Ignore())
                    .ForMember(dest => dest.PostDate, opt => opt.MapFrom(src => DateTime.Now))
                    .ForMember(dest => dest.LikesAmount, opt => opt.MapFrom(src => 0))
                    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                    .ForMember(dest => dest.Likes, opt => opt.Ignore())
                    .ForMember(dest => dest.Comments, opt => opt.Ignore())
                    .ForMember(dest => dest.Tags, opt => opt.Ignore())
                    .ConstructUsing(src => new Post
                    {
                        TextContent = src.TextContent,
                        PostDate = DateTime.Now,
                        LikesAmount = 0,
                        User = src.User,
                    });

                cfg.CreateMap<PostTag, PostTag>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Post, opt => opt.Ignore());

                cfg.CreateMap<Like, Like>()
                    .ForMember(dest => dest.Post, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore());

                cfg.CreateMap<Comment, Comment>()
                    .ForMember(dest => dest.CommentContent, opt => opt.MapFrom(src => src.CommentContent))
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.Post, opt => opt.Ignore())
                    .ForMember(dest => dest.User, opt => opt.Ignore());
            });

            config.AssertConfigurationIsValid();
            return config.CreateMapper();
        }
    }
}
