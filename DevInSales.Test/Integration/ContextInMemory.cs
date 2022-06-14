using DevInSales.Context;
using Microsoft.Extensions.DependencyInjection;

namespace DevInSales.Test.Integration
{
    public class ContextInMemory<T> where T : class
    {
        private APIWebApplicationFactory _factory;
        private SqlContext _context;
        public ContextInMemory(SqlContext context, APIWebApplicationFactory factory)
        {
            _context = context;
            _factory = factory;

        }
        public void AddInMemoryDatabase(T obj)
        {
            using (var scope = _factory.Server.Services.CreateScope())
            {
                _context = scope.ServiceProvider.GetRequiredService<SqlContext>();
                _context.Set<T>().AddAsync(obj);
                _context.SaveChangesAsync();

            }
        }
    }
}
