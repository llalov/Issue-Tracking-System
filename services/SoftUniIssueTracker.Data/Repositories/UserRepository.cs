using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SIT.Models;

namespace SIT.Data.Repositories
{
    public class UserRepository
    {
        internal SITDbContext context;
        internal DbSet<User> dbSet;

        public UserRepository(SITDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<User>();
        }

        public User GetById(string id)
        {
            return this.dbSet.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.dbSet.AsQueryable();
        }

        public virtual void Update(User entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
