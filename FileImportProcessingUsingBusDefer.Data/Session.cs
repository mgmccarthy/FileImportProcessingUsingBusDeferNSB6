using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FileImportProcessingUsingBusDefer.Data
{
    public class Session : ISession
    {
        public readonly DbContext Context;

        public Session(DbContext context)
        {
            this.Context = context;
            OnSessionCreated(this);
        }

        public static event EventHandler Created;

        private static void OnSessionCreated(Session session)
        {
            EventHandler handler = Created;
            if (handler != null)
                handler(session, EventArgs.Empty);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void Add<T>(T entity) where T : class
        {
            Context.Set<T>().Add(entity);
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Context.Set<T>();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
