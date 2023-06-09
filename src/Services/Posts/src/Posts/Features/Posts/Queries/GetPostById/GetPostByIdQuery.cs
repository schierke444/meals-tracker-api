using BuildingBlocks.Commons.CQRS;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetPostById;

public record GetPostByIdQuery(string PostId, string OwnerId): IQuery<PostDetailsDto>;
