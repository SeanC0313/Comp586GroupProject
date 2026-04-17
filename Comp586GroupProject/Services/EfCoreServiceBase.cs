using Comp586GroupProject.Data;
using Microsoft.EntityFrameworkCore;

namespace Comp586GroupProject.Services
{
    public abstract class EfCoreServiceBase
    {
        private readonly IDbContextFactory<DatabaseContext> _factory;

        protected EfCoreServiceBase(IDbContextFactory<DatabaseContext> factory)
        {
            _factory = factory;
        }

        protected async Task<TResult> WithDbAsync<TResult>(Func<DatabaseContext, Task<TResult>> action)
        {
            await using var db = await _factory.CreateDbContextAsync();
            return await action(db);
        }
    }
}
