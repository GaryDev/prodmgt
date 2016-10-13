using ProductMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.Service
{
    public interface IProductService
    {
        Product GetProductBySku(string sku);
        List<Product> GetProductList(SearchCriteria criteria, PagingInfo pagingInfo = null);
        void SaveProduct(Product product);
    }
}
