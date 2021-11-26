namespace w4sd.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetById();
        Task<IEnumerable<T>> GetAll();
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task Delete(int id);
    }
}
