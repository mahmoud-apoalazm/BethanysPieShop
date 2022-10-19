namespace BethanysPieShop.Models
{
    public interface ICatogeryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
