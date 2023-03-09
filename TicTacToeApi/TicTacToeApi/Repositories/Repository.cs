using TicTacToeApi.DataBases;
using TicTacToeApi.Models;

namespace TicTacToeApi.Repositories
{
    public class Repository<TDbModel> : IRepository<TDbModel> where TDbModel : BaseModel
    {
        private ApplicationContext Context { get; set; }
        public Repository(ApplicationContext context)
        {
            Context = context;
        }

        public TDbModel Create(TDbModel model)
        {
            Context.Set<TDbModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            Context.SaveChanges();
        }

        public List<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }
            Context.Update(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }

        public TDbModel Get(int id)
        {
            return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }
    }
}
