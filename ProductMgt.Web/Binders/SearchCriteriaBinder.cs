using ProductMgt.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductMgt.Web.Binders
{
    public class SearchCriteriaBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            SearchCriteria criteria = (SearchCriteria)controllerContext.HttpContext.Session[SearchCriteria.KEY];
            return criteria;
        }
    }
}