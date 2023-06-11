using BuildingBlocks.Commons.CQRS;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetAllPostsByOwnerId;

public record GetAllPostsByOwnerIdQuery(string OwnerId) : IQuery<IEnumerable<PostsDto>>; 
