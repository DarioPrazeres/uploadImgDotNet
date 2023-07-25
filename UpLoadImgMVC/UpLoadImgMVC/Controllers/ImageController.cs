using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Configuration;
using UpLoadImgMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace UpLoadImgMVC.Controllers
{
    public class ImageController : Controller
    {
        string CS = ConfigurationManager.ConnectionStrings["DBS"].ConnectionString;
        List<Image> ImageList = new List<Image>();
        // GET: Image
        public ActionResult Index()
        {
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM TBLImage", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var Image = new Image();
                    Image.Id = Convert.ToInt32(rdr["Id"]);
                    Image.Title = rdr["Title"].ToString();
                    Image.ImageColum = rdr["Image"].ToString();
                    ImageList.Add(Image);
                }
            }
            return View(ImageList);
        }

        // GET: Image/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Image/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Image/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Image/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Image/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Image/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Image/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
