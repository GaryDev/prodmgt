using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProductMgt.ViewModel
{
    public class ProductViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Code { get; set; }

        [Required(ErrorMessage = "请输入产品名称")]
        [StringLength(50, ErrorMessage = "产品名称超出最大长度50")]
        [Display(Name = "产品名称", Order = 2)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(120, ErrorMessage = "产品描述超出最大长度120")]
        [Display(Name = "产品描述", Order = 3)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "请输入正确的价格")]
        [Display(Name = "产品价格", Order = 4)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "请输入产品条码（SKU）")]
        [Display(Name = "产品条码（SKU）", Order = 1)]
        public string Sku { get; set; }
    }
}
