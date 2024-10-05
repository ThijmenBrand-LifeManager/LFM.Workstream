using LFM.WorkStream.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LFM.WorkStream.Repository.TableRepository;

public class RepositoryBase
{
    protected readonly DatabaseContext Context;
    
    protected RepositoryBase(DatabaseContext context)
    {
        Context = context;
    }
}

public abstract class RepositoryBase<TModel>(DatabaseContext context) : RepositoryBase(context)
    where TModel : class
{
    public async Task<TModel> CreateAsync(TModel obj, CancellationToken cancellationToken = default)
    {
        Context.Add(obj);
        await Context.SaveChangesAsync(cancellationToken);

        return obj;
    }

    public async Task<TModel> UpdateAsync(TModel obj, CancellationToken cancellationToken = default)
    {
        Context.Update(obj);
        await Context.SaveChangesAsync(cancellationToken);

        return obj;
    }

    public void DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Context.Set<TModel>().Remove(Context.Set<TModel>().Find(id)!);
    }
    
    public async Task<TModel?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TModel>().FindAsync([Guid.Parse(id)], cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<TModel>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<TModel>().ToListAsync(cancellationToken);
    }
}