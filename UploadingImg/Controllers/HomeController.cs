using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadingImg.Models;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace UploadingImg.Controllers
{
    public class HomeController : Controller
    {
        string CS = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Image Image)
        {
            if(Image.file != null)
            {
                string fileName = Path.GetFileName(Image.file.FileName);
                string _fileName = DateTime.Now.ToString("yymmssff") + fileName;
                string extension = Path.GetExtension(Image.file.FileName);
                string path = Path.Combine(Server.MapPath("~/images/"), _fileName);
                //Tratamento do Model//
                Image.NameFile = "~/images/" + _fileName;
                Image.Id = Guid.NewGuid();
                //Tratamento do Model//

                if (IsVerificationExtension(extension))
                {
                    Image.file.SaveAs(path);
                    Save(Image);
                }
            }
            return RedirectToAction("Photo", new { id = Image.Id });
        }
        public ActionResult Photo(Guid? Id)
        {
            string currentDomain = Request.Url.ToString();
            ViewBag.CurrentDomain = currentDomain;
            Image Model = new Image();
            DataTable TableImage = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(CS))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Image where Id = @Id";
                SqlDataAdapter sqlData = new SqlDataAdapter(query, sqlCon);
                sqlData.SelectCommand.Parameters.AddWithValue("@Id", Id);
                sqlData.Fill(TableImage);
            }
            if (TableImage.Rows.Count == 1)
            {
                Model.Id = Guid.Parse(TableImage.Rows[0][0].ToString());
                Model.NameFile = TableImage.Rows[0][1].ToString();
                return View(Model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        public bool IsVerificationExtension(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpeg": return true; break;
                case ".jpg" : return true; break;
                case ".png": return true; break;
                default: return false;
            }
        }
        public void Save(Image model)
        {
            using (SqlConnection sqlCon = new SqlConnection(CS))
            {
                sqlCon.Open();
                string query = "INSERT INTO Image VALUES(@Id, @fileName)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Id", model.Id);
                sqlCmd.Parameters.AddWithValue("@fileName", model.NameFile);
                sqlCmd.ExecuteNonQuery();
            }
        }
    }
}