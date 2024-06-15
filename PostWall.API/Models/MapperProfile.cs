using AutoMapper;
using PostWall.API.Models.DTO.Comment;
using PostWall.API.Models.DTO.Media;
using PostWall.API.Models.DTO.Post;
using PostWall.API.Models.DTO.Tag;
using PostWall.API.Models.DTO.User;
using PostWall.API.Models.EF;

namespace PostWall.API.Models;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        #region Post
        CreateMap<CreatePostDTO, Post>().
            ForMember(dest => dest.UserId, opt => opt.Ignore()).
            ForMember(dest => dest.ApplicationUser, opt => opt.Ignore());
        CreateMap<UpdatePostDTO, Post>();
        CreateMap<Post, PostListDTO>();
        CreateMap<Post, PostDetailsDTO>();
        #endregion
        #region Comment
        CreateMap<CreateCommentDTO, Comment>().
            ForMember(dest => dest.UserId, opt => opt.Ignore()).
            ForMember(dest => dest.ApplicationUser, opt => opt.Ignore());
        CreateMap<Comment, CommentDetailsDTO>();
        #endregion
        #region Media
        CreateMap<CreateMediaDTO, Media>()
            .ForMember(dest => dest.Post, opt => opt.Ignore());
        CreateMap<Media, MediaDTO>();
        #endregion
        #region Tag
        CreateMap<CreateTagDTO, Tag>();        
        CreateMap<Tag, TagDTO>();
        #endregion
        #region User
        CreateMap<ApplicationUser, UserDetailsDTO>();
        #endregion
    }
}
