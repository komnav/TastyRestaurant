namespace Infrastructure.Repositories
{
    public interface IUpdateRolesRepository
    {
        Task<int> UpdateAsync(string userName, string roles);
    }
}
