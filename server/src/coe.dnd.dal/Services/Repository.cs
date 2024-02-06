using coe.dnd.dal.Contexts;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using Microsoft.EntityFrameworkCore;

namespace coe.dnd.dal.Services;

public class Repository<T> : IRepository<T> where T : Entity
{
    #region Fields

    private readonly DndOrganiserContext _context;
    private readonly DbSet<T> _dbSet;

    #endregion

    #region Constructors

    public Repository(DndOrganiserContext context)
    {
        _context = context;
        _dbSet   = context.Set<T>();
    }

    #endregion

    #region Get methods

    public virtual IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual T Get(int id)
    {
        return _dbSet.FirstOrDefault(c => c.Id == id);
    }

    public virtual async Task<T> GetAsync(int id)
    {
        return await _dbSet.FirstOrDefaultAsync(c => c.Id == id);
    }

    #endregion

    #region CRUD methods

    public virtual bool Add(T entity)
    {
        _context.Add(entity);
        return Save();
    }

    public virtual bool Update(T entity)
    {
        _context.Update(entity);
        return Save();
    }

    public virtual bool Delete(T entity)
    {
        _context.Remove(entity);
        return Save();
    }

    public virtual bool Save()
    {
        return _context.SaveChanges() > 0;
    }

    #endregion
}