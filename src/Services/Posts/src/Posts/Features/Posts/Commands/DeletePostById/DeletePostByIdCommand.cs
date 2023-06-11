using BuildingBlocks.Commons.CQRS;
using MediatR;

namespace Posts.Features.Posts.Commands.DeletePostById;

public record DeletePostByIdCommand(string PostId, string OwnerId) : ICommand<Unit>;