using ProductMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.DataModel
{
    public interface IProductRespository
    {
        Product GetProductBySku(string sku);
        List<Product> GetProductList(SearchCriteria criteria, PagingInfo pagingInfo = null);
        int SaveProduct(Product product);
    }
}
