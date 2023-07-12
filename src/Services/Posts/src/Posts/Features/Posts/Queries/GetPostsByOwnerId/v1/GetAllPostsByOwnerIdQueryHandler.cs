using BuildingBlocks.Commons.CQRS;
using BuildingBlocks.Commons.Models;
using BuildingBlocks.Events.Users;
using BuildingBlocks.Services;
using MassTransit;
using Posts.Features.Posts.Dtos;
using Posts.Features.Posts.Interfaces;

namespace Posts.Features.Posts.Queries.GetPostsByOwnerId.v1;

sealed class  GetPostsByOwnerIdQueryHandler : IQueryHandler<GetPostsByOwnerIdQuery, PaginatedResults<PostDetailsDto>>
{
    private readonly IPostRepository _postRepository;
    private readonly IRequestClient<GetUserByIdRecord> _client;
    public GetPostsByOwnerIdQueryHandler(IPostRepository postRepository, IRequestClient<GetUserByIdRecord> client)
    {
        _postRepository = postRepository;
        _client = client;
    }
    public async Task<PaginatedResults<PostDetailsDto>> Handle(GetPostsByOwnerIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _client.GetResponse<GetUserByIdResult>(new GetUserByIdRecord(request.OwnerId));

        var results = await _postRepository.GetPagedPostListByOwnerId(
            user.Message.Id.ToString(), 
            request.sortColumn, 
            request.sortOrder, 
            request.page, 
            request.PageSize);
        
        return results;
    }
}
