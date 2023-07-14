using BuildingBlocks.Commons.Interfaces;
using BuildingBlocks.Commons.Models;
using Comments.Entities;
using Comments.Features.Comments.Dtos;

namespace Comments.Features.Comments.Interfaces;

public interface ICommentsRepository : IWriteRepository<Comment>, IReadRepository<Comment>
{
    Task<CommentDetailsDto?> GetCommentById(string CommentId);
    Task<PaginatedResults<CommentDetailsDto>> GetCommentsByPostId(string postId, string? sortColumn, string? sortOrder, int page, int pageSize);
    Task<PaginatedResults<CommentDetailsDto>> GetCommentsByOwnerId(string ownerId, string? sortColumn, string? sortOrder, int page, int pageSize);
}