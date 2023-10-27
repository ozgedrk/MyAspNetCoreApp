namespace MyAspNetCoreApp.Web.Models
{
    public class ProductRepository
    {
        private static List<Product> _products = new List<Product>()
        {

               //new Product { Id = 1, Name = "Bilgisayar", Price = 32000, Stock = 25 },
               //new Product { Id = 2, Name = "Klavye", Price = 550, Stock = 478 },
               //new Product { Id = 3, Name = "Telefon", Price = 72000, Stock = 125 }

        };
        public List<Product> GetAll() => _products;

        public void Add(Product newProduct) => _products.Add(newProduct);

        public void Remove(int id)
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == id);


            if (hasProduct == null)
            {
                throw new Exception($"Bu id({id})`ye ait urun bulunmamaktadir.");

            }
            _products.Remove(hasProduct);

        }

        public void Update(Product updateProduct)
        {
            var hasProduct = _products.FirstOrDefault(p => p.Id == updateProduct.Id);

            if (hasProduct == null)
            {
                throw new Exception($"Bu id({updateProduct.Id})`ye ait urun bulunmamaktadir.");
            }

            hasProduct.Name = updateProduct.Name;
            hasProduct.Price = updateProduct.Price;
            hasProduct.Stock = updateProduct.Stock;


            var index = _products.FindIndex(p => p.Id == updateProduct.Id);

            _products[index] = hasProduct;

        }
    }

}