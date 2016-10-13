using ProductMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgt.ViewModel
{
    public class ProductsListViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public SearchCriteriaViewModel CurrentCriteria { get; set; }
        public PagingInfoViewModel PageInfo { get; set; }

        public ProductsListViewModel()
        {
            Products = new List<ProductViewModel>();
        }
    }
}
