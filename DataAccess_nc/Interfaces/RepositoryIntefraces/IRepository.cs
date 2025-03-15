namespace DomainLayer.Interfaces.RepositoryIntefraces
{
    public interface IRepository<TObject>
    {
        int Add(TObject? obj);
        int Update(TObject obj);
        int Delete(TObject obj);
        TObject? GetById(int id);

        bool CheckExist(int id);
    }
}