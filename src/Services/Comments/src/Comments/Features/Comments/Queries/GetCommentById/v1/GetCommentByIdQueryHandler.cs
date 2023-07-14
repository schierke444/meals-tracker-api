using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Exceptions;
using Comments.Features.Comments.Dtos;
using Comments.Features.Comments.Interfaces;

namespace Comments.Features.Comments.Queries.GetCommentById.v1;

sealed class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDetailsDto>
{
    private readonly ICommentsRepository _commentsRepository;
    public GetCommentByIdQueryHandler(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }
    public async Task<CommentDetailsDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _commentsRepository.GetCommentById(request.CommentId)
            ?? throw new NotFoundException($"Comment with Id '{request.CommentId}' was not found.");

        return result;
    }
}