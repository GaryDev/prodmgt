using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductMgt.Entity;
using ProductMgt.DataModel;

namespace ProductMgt.Service
{
    public class ProductService : IProductService
    {
        private IProductRespository _productRespository;

        public ProductService(IProductRespository productRespository)
        {
            _productRespository = productRespository;
        }

        public Product GetProductBySku(string sku)
        {
            Product product = _productRespository.GetProductBySku(sku);
            return product;
        }

        public List<Product> GetProductList(SearchCriteria criteria, PagingInfo pagingInfo = null)
        {
            List<Product> products = _productRespository.GetProductList(criteria, pagingInfo);
            return products;
        }

        public void SaveProduct(Product product)
        {
            _productRespository.SaveProduct(product);
        }
    }
}
