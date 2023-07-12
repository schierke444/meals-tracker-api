using BuildingBlocks.Commons.CQRS;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Posts.Features.Posts.Dtos;

namespace Posts.Features.Posts.Commands.UpdatePost.v1;

public record UpdatePostCommand(string PostId, JsonPatchDocument<UpdatePostDto> UpdatePost) : ICommand<Unit>;