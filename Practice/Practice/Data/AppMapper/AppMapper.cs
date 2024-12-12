using AutoMapper;
using Practice.Data.Dto;
using Practice.Data.Model;

namespace Practice.Data.AppMapper
{
    public class AppMapper : Profile
    {
        public AppMapper()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserRegisterDto, User>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<User, AuthorDto>()
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.Posts, opt => opt.MapFrom(src => src.Posts.Count));

            CreateMap<PostCreateDto, Post>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.SubCommentsCount, opt => opt.MapFrom(src => src.ChildComments == null ? 0 : src.ChildComments.Count));

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.HasLike, opt => opt.MapFrom(src => src.Likes.Count > 0));

            CreateMap<Post, PostWithCommentsDto>()
                .ForMember(dest => dest.LikesCount, opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.HasLike, opt => opt.MapFrom(src => src.Likes.Count > 0))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom(src => src.Comments.Count))
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }
}