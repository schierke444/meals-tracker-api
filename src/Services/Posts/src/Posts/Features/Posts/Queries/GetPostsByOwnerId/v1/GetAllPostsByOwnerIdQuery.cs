using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetPostsByOwnerId.v1;

public record GetPostsByOwnerIdQuery(
    string OwnerId ,
    string? sortColumn, 
    string? sortOrder, 
    int page, 
    int PageSize
): IQuery<PaginatedResults<PostDetailsDto>>; 