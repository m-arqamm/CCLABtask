using lab09_cc.Model;

namespace lab09_cc.Repository
{
    public interface IProductRepository
    {
            Product GetProductByID(int productId);
        
            IEnumerable<Product> GetProducts();
       
          void DeleteProduct(int productId);
        
              void InsertProduct(Product product);
     
           void UpdateProduct(Product product);
        void Save();

    }
}
