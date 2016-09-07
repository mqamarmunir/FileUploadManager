using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class UploadController : Controller
    {
        //
        // GET: /Upload/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Upload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Upload/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Upload/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (var stream = HttpContext.Request.GetBufferlessInputStream())
                using (var fileStream = System.IO.File.Create("c:\\tempfile.txt"))
                {
                    stream.CopyTo(fileStream);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult UploadFile()
        {
            foreach (string file in Request.Files)
            {
                var FileDataContent = Request.Files[file];
                if (FileDataContent != null && FileDataContent.ContentLength > 0)
                {
                    // take the input stream, and save it to a temp folder using
                    // the original file.part name posted
                    var stream = FileDataContent.InputStream;
                    var fileName = Path.GetFileName(FileDataContent.FileName);
                    var UploadPath = Server.MapPath("~/App_Data/uploads");
                    Directory.CreateDirectory(UploadPath);
                    string path = Path.Combine(UploadPath, fileName);
                    try
                    {
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                        // Once the file part is saved, see if we have enough to merge it
                        Shared.Utils UT = new Shared.Utils();
                        UT.MergeFile(path);
                    }
                    catch (IOException ex)
                    {
                        // handle
                    }
                }
            }
            return RedirectToAction("Index");
            //return new HttpResponseMessage()
            //{
            //    StatusCode = System.Net.HttpStatusCode.OK,
            //    Content = new StringContent("File uploaded.")
            //};
        }
        //
        // GET: /Upload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Upload/Edit/5
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

        //
        // GET: /Upload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Upload/Delete/5
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
