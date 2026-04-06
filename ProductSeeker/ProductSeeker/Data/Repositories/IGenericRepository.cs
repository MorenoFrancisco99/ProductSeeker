namespace ProductSeeker.Data.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> Create(T newModel);
    Task<T>? Update(T updatedModel);
    Task<T>? Delete(T deletedModel);
    Task<IEnumerable<T>>? DeleteAll();

}