using BuildingBlocks.Commons.CQRS;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetPosts;

public record GetPostsQuery : IQuery<IEnumerable<PostsDto>>; 
