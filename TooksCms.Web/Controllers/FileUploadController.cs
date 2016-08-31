using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TooksCms.Web.Controllers
{
    public class FileUploadController : BaseController
    {
        public JsonResult PostFile(string filename)
        {
            try
            {
                var length = Request.ContentLength;
                var bytes = new byte[length];
                Request.InputStream.Read(bytes, 0, length);
                // bytes has byte content here. what do do next?

                string articleId = Request.Headers["X-File-Articleid"];
                int imagecount = Convert.ToInt32(Request.Headers["X-File-Imagecount"]);

                string newFileName;
                string ext = Path.GetExtension(filename);
                string folderPath;
                string path;

                GetPath(HttpContext, ext, articleId, ref imagecount, out newFileName, out folderPath, out path);

                var url = VirtualPathUtility.ToAbsolute("~/Uploads/Images/Temp/" + newFileName + ext);
                var thumb = VirtualPathUtility.ToAbsolute("~/Uploads/Images/Temp/" + newFileName + "_thumb" + ext);

                var fileStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
                fileStream.Write(bytes, 0, length);
                fileStream.Close();

                using (Bitmap bmp = new Bitmap(path))
                {
                    double scaleX = 1, scaleY = 1;
                    const double size = 450;

                    if (bmp.Height > size) { scaleY = size / bmp.Height; }
                    if (bmp.Width > size) { scaleX = size / bmp.Width; }
                    // maintain aspect ratio by picking the most severe scale
                    double scale = Math.Min(scaleY, scaleX);

                    int newWidth = Convert.ToInt32(bmp.Width * scale);
                    int newHeight = Convert.ToInt32(bmp.Height * scale);

                    Bitmap result = new Bitmap(newWidth, newHeight);
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        g.DrawImage(bmp, 0, 0, newWidth, newHeight);
                    }
                    result.Save(folderPath + newFileName + "_thumb" + ext);
                    result.Dispose();
                }

                return Json(new
                {
                    imagePath = url,
                    position = "gal",
                    size = "s",
                    thumbnail = thumb,
                    value = newFileName + ext,
                    thumbValue = newFileName + "_thumb" + ext
                });
            }
            catch
            {
                return Json(new { error = true });
            }
        }

        private static void GetPath(HttpContextBase context, string ext, string articleId, ref int pictureCount, out string fileName, out string folderPath, out string path)
        {
            fileName = articleId + "_" + pictureCount.ToString();
            folderPath = context.Server.MapPath("~/Uploads/Images/Temp/");
            path = context.Server.MapPath("~/Uploads/Images/Temp/") + fileName + ext;

            if (System.IO.File.Exists(path))
            {
                pictureCount++;
                GetPath(context, ext, articleId, ref pictureCount, out fileName, out folderPath, out path);
            }
        }
    }
}
