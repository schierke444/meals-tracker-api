using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Events.Posts;
using Comments.Features.Comments.Dtos;
using Comments.Features.Comments.Interfaces;
using MassTransit;

namespace Comments.Features.Comments.Queries.GetCommentsByPostId.v1;

sealed class GetCommentsByPostIdQueryHandler : IQueryHandler<GetCommentsByPostIdQuery, PaginatedResults<CommentDetailsDto>>
{
    private readonly ICommentsRepository _commentsRepository;
    private readonly IRequestClient<GetPostsByIdRecord> _client;
    public GetCommentsByPostIdQueryHandler(ICommentsRepository commentsRepository, IRequestClient<GetPostsByIdRecord> client)
    {
        _commentsRepository = commentsRepository;
        _client = client;
    }
    public async Task<PaginatedResults<CommentDetailsDto>> Handle(GetCommentsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _client.GetResponse<GetPostsByIdResult>(new GetPostsByIdRecord(request.postId));

        return await _commentsRepository.GetCommentsByPostId(
            post.Message.Id.ToString(),
            request.SortColumn,
            request.SortOrder,
            request.Page,
            request.PageSize
        );
    }
}