namespace TechXpress.Data.Models.ViewModels
{
    public class ProductDisplayModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categorys { get; set; }
        public string STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;

        public string SortBy { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}
