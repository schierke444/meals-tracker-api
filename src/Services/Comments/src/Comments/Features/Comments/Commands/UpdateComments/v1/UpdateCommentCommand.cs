using BuildingBlocks.Commons.CQRS;
using Comments.Features.Comments.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace Comments.Features.Comments.Commands.UpdateComments.v1;

public sealed record UpdateCommentCommand(string CommentId, JsonPatchDocument<UpdateCommentDto> UpdateComment) : ICommand<Unit>;