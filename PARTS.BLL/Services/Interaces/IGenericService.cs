namespace PARTS.BLL.Services.Interaces
{
    public interface IGenericService<TEntity, TRequest, TResponse>
        where TEntity : class
        where TRequest : class
        where TResponse : class
    {
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<TResponse>> GetAllAsync();
        Task<TResponse> GetByIdAsync(Guid id);
        Task PostAsync(TRequest request);
        Task UpdateAsync(TRequest request);
    }
}