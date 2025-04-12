

using ApplicationLayer.Dtos.Product_DTOS;
using DomainLayer;
using DomainLayer.Interfaces.RepositoryIntefraces;

namespace ApplicationLayer.Products_Handling
{
    public class ProductHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IMedicalCategory _medicalCategory;
        private readonly ISubMedicalCategory _subMedicalCategory;

        public ProductHandler(IProductRepository productRepository, 
            IMedicalCategory medicalCategory, 
            ISubMedicalCategory subMedicalCategory)
        {
            _productRepository = productRepository;
            _medicalCategory = medicalCategory;
            _subMedicalCategory = subMedicalCategory;
        }
        
        public bool TryCreateProduct(CreateProductDto product)
        {
            if (_productRepository.GetProductByName(product.ProductName) != null) return false;
            if (!_medicalCategory.CheckExist(product.ProductCategory) ||
                !_subMedicalCategory.CheckExist(product.ProductSubCategory)) return false;


            Product p = new Product()
            {
                ProductName = product.ProductName,
                ItemDescription = product.ItemDescription,
                Price = product.Price,
                Stock = product.Stock,
                ProductCategory = product.ProductCategory,
                ProductSubCategory = product.ProductSubCategory
            };

            int result = _productRepository.Add(p);

            return result != -1;
        }

        public bool TryUpdateProduct(CreateProductDto product)
        {
            Product ? p = _productRepository.GetProductByName(product.ProductName);
            if (p == null) return false;
            if (!_medicalCategory.CheckExist(product.ProductCategory) ||
                !_subMedicalCategory.CheckExist(product.ProductSubCategory)) return false;


            p.ProductName = product.ProductName;
            p.ItemDescription = product.ItemDescription;
            p.Price = product.Price;
            p.Stock = product.Stock;
            p.ProductCategory = product.ProductCategory;
            p.ProductSubCategory = product.ProductSubCategory;
            

            int result = _productRepository.Update(p);

            return result != -1;
        }

        public bool TryDeleteProduct(string productName)
        {
            Product? p = _productRepository.GetProductByName(productName);
            if (p == null) return false;

            int result = _productRepository.Delete(p);

            return result != -1;
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
