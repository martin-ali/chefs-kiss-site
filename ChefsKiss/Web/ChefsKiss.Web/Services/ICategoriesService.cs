namespace ChefsKiss.Web.Services
{
    public interface ICategoriesService
    {
        T ById<T>(int id);
    }
}
