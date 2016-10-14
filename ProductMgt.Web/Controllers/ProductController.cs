using AutoMapper;
using ProductMgt.Config;
using ProductMgt.Entity;
using ProductMgt.Service;
using ProductMgt.ViewModel;
using ProductMgt.Web.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMgt.Web.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        private static int PAGE_SIZE = Convert.ToInt32(AppConfig.Instance.PageSize);
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("index")]
        public ViewResult Index(string mtoken = null)
        {
            return View();
        }

        [Route("list")]
        public ViewResult List(SearchCriteria criteria, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();
            if (criteria != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<SearchCriteria, SearchCriteriaViewModel>());
                model.CurrentCriteria = Mapper.Map<SearchCriteria, SearchCriteriaViewModel>(criteria);

                UpdateProductListViewModel(model, criteria, page);
            }
            return View(model);
        }

        [Route("list")]
        [HttpPost]
        public ViewResult List(ProductsListViewModel model)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SearchCriteriaViewModel, SearchCriteria>());
            SearchCriteria criteria = Mapper.Map<SearchCriteriaViewModel, SearchCriteria>(model.CurrentCriteria);
            ControllerContext.HttpContext.Session[SearchCriteria.KEY] = criteria;

            UpdateProductListViewModel(model, criteria);
            return View(model);
        }

        [Route("create")]
        public ViewResult Create()
        {
            return View("Edit", new ProductViewModel());
        }

        [Route("edit")]
        public ViewResult Edit(string sku)
        {
            Product product = _productService.GetProductBySku(sku);

            Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductViewModel>());
            ProductViewModel model = Mapper.Map<Product, ProductViewModel>(product);
            return View(model);
        }

        [Route("edit")]
        [HttpPost]
        public ActionResult Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ProductID == 0)
                {
                    Product p = _productService.GetProductBySku(model.Sku);
                    if (p != null && p.ProductID > 0)
                    {
                        ModelState.AddModelError(string.Empty, string.Format("产品条码{0}已存在。", model.Sku));
                        return View(model);
                    }
                }

                Mapper.Initialize(cfg => cfg.CreateMap<ProductViewModel, Product>());
                Product product = Mapper.Map<ProductViewModel, Product>(model);
                _productService.SaveProduct(product);

                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(model)
            };
        }

        private void UpdateProductListViewModel(ProductsListViewModel model, SearchCriteria criteria, int page = 1)
        {
            List<Product> productList = _productService.GetProductList(criteria);
            if (productList != null && productList.Count > 0)
            {
                List<Product> productListDisplay = productList.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE).ToList();
                Mapper.Initialize(cfg => cfg.CreateMap<Product, ProductViewModel>());
                List<ProductViewModel> products = Mapper.Map<List<Product>, List<ProductViewModel>>(productListDisplay);
                model.Products = products;
                model.PageInfo = new PagingInfoViewModel(productList.Count, page, PAGE_SIZE);
            }
        }

    }
}