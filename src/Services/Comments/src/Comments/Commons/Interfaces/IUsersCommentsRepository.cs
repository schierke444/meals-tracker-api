using Comments.Entities;

namespace Comments.Commons.Interfaces;

public interface IUsersCommentsRepository
{
    Task<UsersComments?> GetUserById(string Id);
    Task Add(UsersComments entity);
    void Remove(UsersComments entity);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}