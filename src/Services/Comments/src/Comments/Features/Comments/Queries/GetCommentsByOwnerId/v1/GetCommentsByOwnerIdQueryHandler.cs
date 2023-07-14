using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using BuildingBlocks.Commons.Models;
using Comments.Commons.Interfaces;
using Comments.Features.Comments.Dtos;
using Comments.Features.Comments.Interfaces;

namespace Comments.Features.Comments.Queries.GetCommentsByOwnerId.v1;

sealed class GetCommentsByOwnerIdQueryHandler : IQueryHandler<GetCommentsByOwnerIdQuery, PaginatedResults<CommentDetailsDto>>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IUsersCommentsRepository _usersCommentsRepository;
    public GetCommentsByOwnerIdQueryHandler(ICommentsRepository commentsRepository, IUsersCommentsRepository usersCommentsRepository)
    {
        _commentsRepository = commentsRepository;
        _usersCommentsRepository = usersCommentsRepository;
    }
    public async Task<PaginatedResults<CommentDetailsDto>> Handle(GetCommentsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersCommentsRepository.GetUserById(request.OwnerId)
            ?? throw new NotFoundException($"User with Id '{request.OwnerId}' was not found.");

        var results = await _commentsRepository.GetCommentsByOwnerId(
            user.UserId.ToString(),
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );

        return results;
    }
}