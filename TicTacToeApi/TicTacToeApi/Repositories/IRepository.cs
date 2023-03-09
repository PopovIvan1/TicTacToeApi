using TicTacToeApi.Models;

namespace TicTacToeApi.Repositories
{
    public interface IRepository<TDbModel> where TDbModel : BaseModel
    {
        public List<TDbModel> GetAll();
        public TDbModel Get(int id);
        public TDbModel Create(TDbModel model);
        public TDbModel Update(TDbModel model);
        public void Delete(int id);
    }
}
