using BuildingBlocks.Commons.CQRS;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetPostById.v1;

public record GetPostByIdQuery(string PostId): IQuery<PostDetailsDto>;
