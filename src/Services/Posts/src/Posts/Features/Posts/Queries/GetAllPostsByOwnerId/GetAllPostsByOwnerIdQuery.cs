using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Queries.GetAllPostsByOwnerId;

public record GetAllPostsByOwnerIdQuery(string? sortColumn, string? sortOrder, int page, int PageSize, string OwnerId) : IQuery<PaginatedResults<PostDetailsDto>>; 
