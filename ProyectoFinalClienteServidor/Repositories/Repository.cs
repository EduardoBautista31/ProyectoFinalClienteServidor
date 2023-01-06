using Microsoft.EntityFrameworkCore;

namespace ProyectoFinalClienteServidor.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }
        public DbSet<T> Get()
        {
            return context.Set<T>();
        }
        public T? Get(object id)
        {
            return context.Find<T>(id);
        }
        public void Insert(T entidad)
        {
            context.Add(entidad);
            context.SaveChanges();
        }

        public void Update(T entidad)
        {
           context.Remove(entidad);
            context.SaveChanges();
            context.Add(entidad);
            context.SaveChanges();
        }

        public void Delete(T entidad)
        {
            context.Remove(entidad);
            context.SaveChanges();
        }
    }
}
