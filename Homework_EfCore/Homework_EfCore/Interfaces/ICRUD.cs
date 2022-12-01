namespace Homework_EfCore.Interfaces
{
    public interface ICRUDAsync<TModel, TReturn>
    {
        public Task<TReturn> Create(TModel model);
        public Task<TReturn> Get(TModel model);
        public Task<List<TReturn>> GetList();
        public Task<TReturn> Update(TModel model);
        public Task<TReturn> Delete(TModel model);
    }
}
