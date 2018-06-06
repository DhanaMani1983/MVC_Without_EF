using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVC_Without_EF.Models;

namespace MVC_Without_EF.Controllers
{
    public class ProductController : Controller
    {
        string sqlConnectionString = @"Data Source = GMDSQLUAT01; Initial Catalog = GMD_DWH_Env6; Integrated Security=True";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable productDT = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                sqlConn.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter("SELECT * FROM dbo.Test1", sqlConn);
                sqlDA.Fill(productDT);
            }
                return View(productDT);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            try
            {
                // TODO: Add insert logic here
                using(SqlConnection sqlConn=new SqlConnection(sqlConnectionString))
                {
                    sqlConn.Open();
                    string sqlQuery = "INSERT INTO dbo.Test1(ProductName, Price, Quantity) VALUES (@ProductName, @Price, @Quantity)";
                    SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
                    sqlCmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                    sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                    sqlCmd.Parameters.AddWithValue("@Quantity", productModel.Quantity);
                    sqlCmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                sqlConn.Open();
                string sqlQuery = "SELECT * FROM dbo.Test1 WHERE ProductID = @ProductID";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlQuery, sqlConn);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDataAdapter.Fill(dataTable);
            }

            if (dataTable.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                productModel.ProductName = dataTable.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dataTable.Rows[0][2].ToString());
                productModel.Quantity = Convert.ToInt32(dataTable.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                sqlConn.Open();
                string sqlQuery = "UPDATE dbo.Test1 SET ProductName = @ProductName, Price = @Price, Quantity = @Quantity WHERE ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
                sqlCmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlCmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("@Quantity", productModel.Quantity);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                sqlConn.Open();
                string sqlQuery = "DELETE FROM dbo.Test1 WHERE ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(sqlQuery, sqlConn);
                sqlCmd.Parameters.AddWithValue("@ProductID", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

      
    }
}
