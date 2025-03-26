

using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Products_Handling
{
    public class ProductHandler
    {
        private readonly IProductRepository _productRepository;

        // delete, update, create new product required 
        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }



        public bool TrySearchProductByName(string productName,out Product ? product)
        {

            product = _productRepository.GetProductByName(productName);

            return product != null;
        }



        public bool TryGetAllProductsByMainCategory(string categoryName,out List<Product>? products)
        {
            products = _productRepository.GetAllProductsWithMedicalCategory(new MedicalCategory()
                { CategoryName = categoryName }).ToList();

            return products.Count != 0;
        }

        public bool TryGetAllProductsBySubCategory(string categoryName, out List<Product>? products)
        {
            products = _productRepository.GetAllProductsWithSubMedicalCategory(new SubMedicalCategory()
                { SubMedicalCategoryName = categoryName }).ToList();

            return products.Count != 0;
        }

        public bool IsProductAvailable(string productName)
        {
            Product? p = _productRepository.GetProductByName(productName);

            return p != null && p.IsProductAvailable();
        }
    }
}
