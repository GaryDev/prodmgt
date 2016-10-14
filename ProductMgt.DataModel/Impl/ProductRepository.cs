using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductMgt.Entity;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;
using ProductMgt.Config;

namespace ProductMgt.DataModel
{
    public class ProductRepository : IProductRespository
    {
        public Product GetProductBySku(string sku)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append("SELECT a.Product_id, b.Short_name, a.product_code, ")
            .Append("(SELECT TOP 1 pcd.new_price ")
            .Append(" FROM price_change_detail pcd")
            .Append(" WHERE pcd.product_id=a.Product_id")
            .Append(" ) price, ")
            .Append("b.Long_name ")
            .Append("FROM Product a(NOLOCK) ")
            .Append("LEFT JOIN  product_name b(NOLOCK) ")
            .Append("ON a.Product_id=b.Product_ID ")
            .Append("AND b.Language_ID=83 ")
            .Append("WHERE a.product_code = ltrim(rtrim(@sku)) ");

            SqlDatabase database = new SqlDatabase(AppConfig.Instance.DBConnection);
            DbCommand command = database.GetSqlStringCommand(sqlBuilder.ToString());
            command.CommandTimeout = 300;

            database.AddInParameter(command, "@sku", DbType.String, sku);

            Product product = new Product();
            using (IDataReader reader = database.ExecuteReader(command))
            {
                if (reader.Read())
                {
                    product = CreateProduct(reader);
                }
            }
            return product;
        }

        public List<Product> GetProductList(SearchCriteria criteria, PagingInfo pagingInfo = null)
        {
            SqlDatabase database = new SqlDatabase(AppConfig.Instance.DBConnection);
            DbCommand command = database.GetStoredProcCommand("Raymsp_FindProduct_Page");
            command.CommandTimeout = 300;

            database.AddInParameter(command, "@sku", DbType.String, criteria.ProductSku);
            database.AddInParameter(command, "@Short_Name", DbType.String, criteria.ProductName);
            if (pagingInfo != null)
            {
                database.AddInParameter(command, "@@Line_Count_Page", DbType.Int32, pagingInfo.ItemsPerPage);
                database.AddInParameter(command, "@Page", DbType.Int32, pagingInfo.CurrentPage);
            }

            List<Product> products = new List<Product>();
            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    Product product = CreateProduct(reader);
                    products.Add(product);
                }
            }

            return products;
        }

        private Product CreateProduct(IDataReader reader)
        {
            Product product = new Product();

            product.ProductID = Convert.ToInt32(reader["Product_id"]);
            product.Code = (reader["product_code"] as string).TrimEnd();
            product.Sku = (reader["product_code"] as string).TrimEnd();
            product.Price = Convert.ToDecimal(reader["price"]);
            product.Name = reader["Short_name"] as string;
            product.Description = reader["Long_name"] as string;

            return product;
        }

        public int SaveProduct(Product product)
        {
            SqlDatabase database = new SqlDatabase(AppConfig.Instance.DBConnection);
            DbCommand command = database.GetStoredProcCommand("Raymsp_Product_Generate");
            command.CommandTimeout = 300;

            database.AddInParameter(command, "@sku", DbType.String, product.Sku);
            database.AddInParameter(command, "@Short_Name", DbType.String, product.Name);
            database.AddInParameter(command, "@price", DbType.Decimal, product.Price);
            database.AddInParameter(command, "@Long_Name", DbType.String, product.Description);

            try
            {
                int ret = database.ExecuteNonQuery(command);
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
