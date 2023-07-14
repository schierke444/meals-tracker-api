using Comments.Entities;

namespace Comments.Commons.Interfaces;
public interface IUsersCommentsService
{
    Task<UsersComments> CreateUsersRecord(string Id, string Username, CancellationToken cancellationToken = default);
}