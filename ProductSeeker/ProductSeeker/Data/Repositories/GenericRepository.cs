using Microsoft.EntityFrameworkCore;
using ProductSeeker.Data.Context;
using ProductSeeker.Data.Interfaces;

namespace ProductSeeker.Data.Repositories;


//tomado de: https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application

/// <summary>
/// Generic repository. Basic CRUD
/// </summary>
/// <typeparam name="T">Target entity</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AplicationDBContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AplicationDBContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    
    /// <summary>
    /// Gets all the elements of a given table.
    /// </summary>
    /// <returns> an IEnumerable of elements. Null if error</returns>
    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);  
    }

    

    public virtual async Task<T?> Create(T newModel)
    {
        _dbSet.Add(newModel);
        await _context.SaveChangesAsync();
        return newModel;  
    }

    public virtual Task<T> Update(T updatedModel)
    {
        throw new NotImplementedException();
    }

    public virtual Task<T> Delete(T deletedModel)
    {
        throw new NotImplementedException();
    }

    public virtual Task<IEnumerable<T>> DeleteAll()
    {
        throw new NotImplementedException();
    }
}