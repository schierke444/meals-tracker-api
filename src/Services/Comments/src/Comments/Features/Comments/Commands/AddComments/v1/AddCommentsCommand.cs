using BuildingBlocks.Commons.CQRS;

namespace Comments.Features.Comments.Commands.AddComments.v1;

public sealed record AddCommentsCommand(string PostId, string Content) : ICommand<Guid>;