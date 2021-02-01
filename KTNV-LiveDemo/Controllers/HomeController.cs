using KTNV_LiveDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KTNV_LiveDemo.Controllers
{
    public class HomeController : Controller
    {
        private string default_image = "/images/200x200.png";
        private string default_filefolder = "/Content/files/";
        private string default_imagefolder = "/Content/anh/";
        public ActionResult Index()
        {
            try
            {
                var listFileName = new DirectoryInfo(Server.MapPath(default_filefolder)).GetFiles().Select(o => o.Name).ToList();
                //string[] lines = System.IO.File.ReadAllLines(Server.MapPath("~/Content/danhsachfiledinhkem.txt"));
                var listFile = new List<string>();
                if (listFileName != null && listFileName.Count > 0)
                {
                    foreach (string line in listFileName)
                    {
                        var f = default_filefolder + line;
                        listFile.Add(f);
                        // Use a tab to indent each line of the file.

                    }
                }

                string[] lineAttend = System.IO.File.ReadAllLines(Server.MapPath("~/Content/danhsachthamdu.txt"));
                var listAttend = new List<Attend>();
                if (lineAttend != null && lineAttend.Length > 0)
                {
                    foreach (string line in lineAttend)
                    {
                        string[] subs = line.Split(';');
                        if (subs != null && subs.Length > 2)
                        {
                            var file = new Attend
                            {
                                AttendName = subs[0],
                                Ranking = String.IsNullOrEmpty(subs[1]) ? "..." : subs[1],
                                AttendImage = String.IsNullOrEmpty(subs[2]) ? default_image : subs[2]
                            };
                            listAttend.Add(file);
                        }
                        // Use a tab to indent each line of the file.
                    }
                }

                var general = new GeneralModel
                {
                    attends = listAttend,
                    files = listFile
                };

                //Người chủ trì
                string host = System.IO.File.ReadAllText(Server.MapPath("~/Content/chutri.txt"));
                if (host != null && host.Length > 0)
                {
                    var listTT = host.Split(';');
                    if (listTT != null && listTT.Count() >1)
                    {
                        var nguoichutri = listTT[0];
                        var chucvu = listTT[1];
                        var anh = listTT[2];
                        ViewBag.Nguoichutri = nguoichutri;
                        ViewBag.Chucvuchutri = String.IsNullOrEmpty(chucvu) ? "..." : chucvu;
                        ViewBag.Anhnguoichutri = String.IsNullOrEmpty(anh) ? default_image : anh;

                    }

                }

                //Tiêu đề
                string lineTitle = System.IO.File.ReadAllText(Server.MapPath("~/Content/danhsachtieude.txt"));
                if (lineTitle != null && lineTitle.Length > 0)
                {
                    ViewBag.TenHoiNghi = lineTitle;
                }
                string noidung = System.IO.File.ReadAllText(Server.MapPath("~/Content/noidung.txt"));
                if (noidung != null && noidung.Length > 0)
                {
                    ViewBag.Noidung = noidung;
                }
                
                return View(general);
            }
            catch
            {
                return View();
            }
            
        }

        #region[Tiêu đề, nội dung]
        //Admin tiêu đề, nội dung
        public ActionResult Title()
        {
            ViewBag.title = "Tiêu đề, nội dung";
            string lineTitle = System.IO.File.ReadAllText(Server.MapPath("~/Content/danhsachtieude.txt"));
            var tempObject = new Title();
            if (lineTitle != null && lineTitle.Length > 0)
            {
                    tempObject.TitleName = lineTitle;
            }

            string noidung = System.IO.File.ReadAllText(Server.MapPath("~/Content/noidung.txt"));
            tempObject.TitleContent = noidung;
            ViewBag.ShowSuccess = "";
            return View(tempObject);
        }
        [HttpPost]
        public ActionResult Title(Title title)
        {
            try
            {
                var pathTieude = Server.MapPath("~/Content/danhsachtieude.txt");
                var pathNoidung = Server.MapPath("~/Content/noidung.txt");
                System.IO.File.WriteAllText(pathTieude, title.TitleName);
                System.IO.File.WriteAllText(pathNoidung, title.TitleContent);
                ViewBag.ShowSuccess = "true";
                return View();
            }
            catch
            {
                ViewBag.ShowSuccess = "false";
                return View();
            }
            
        }
        #endregion

        #region[Tham du]
        public ActionResult Attend()
        {
            ViewBag.title = "Danh sách tham dự";
            string chutriText = System.IO.File.ReadAllText(Server.MapPath("~/Content/chutri.txt"));
            var chutri = new Attend();
            if (chutriText !=null && chutriText.Length > 0)
            {
                string[] subChutri = chutriText.Split(';');
                if (subChutri != null && subChutri.Length > 2)
                {
                    chutri.AttendName = subChutri[0];
                    chutri.Ranking = subChutri[1];
                    chutri.AttendImage = String.IsNullOrEmpty(subChutri[2]) ? default_image : subChutri[2];
                }
            }
            

            string[] lineAttend = System.IO.File.ReadAllLines(Server.MapPath("~/Content/danhsachthamdu.txt"));
            var listAttend = new List<Attend>();
           
            if (lineAttend != null && lineAttend.Length > 0)
            {
                foreach (string line in lineAttend)
                {
                    string[] subs = line.Split(';');
                    if (subs != null && subs.Length > 2)
                    {
                        var file = new Attend
                        {
                            AttendName = subs[0],
                            Ranking = subs[1],
                            AttendImage = String.IsNullOrEmpty(subs[2]) ? default_image : subs[2]
                        };
                        listAttend.Add(file);
                    }
                    // Use a tab to indent each line of the file.
                }
                while (listAttend.Count < 15)
                {
                    listAttend.Add(new Attend { AttendName = "", AttendImage = "" });
                }
            }
            //ViewBag.ListImage = getListImage();
            var attends = new Attends {
                 host = chutri,
                 attend1 = listAttend[0],
                attend2 = listAttend[1],
                attend3 = listAttend[2],
                attend4 = listAttend[3],
                attend5 = listAttend[4],
                attend6 = listAttend[5],
                attend7 = listAttend[6],
                attend8 = listAttend[7],
                attend9 = listAttend[8],
                attend10 = listAttend[9],
                attend11 = listAttend[10],
                attend12 = listAttend[11],
                attend13 = listAttend[12],
                attend14 = listAttend[13],
                attend15 = listAttend[14],
                listImage = getListImage()
            };
           
            ViewBag.ShowSuccess = "";
            return View(attends);
           
        }


        [HttpPost]
        public ActionResult Attend(Attends attends)
        {
            try
            {
                var pathChutri = Server.MapPath("~/Content/chutri.txt");
                var pathThamdu = Server.MapPath("~/Content/danhsachthamdu.txt");
                System.IO.File.WriteAllText(pathChutri, attends.host.AttendName + ";" + attends.host.Ranking + ";" + default_imagefolder + attends.host.AttendImage);
                StringBuilder sb = new StringBuilder();
                sb.Append(attends.attend1.AttendName + ";" + attends.attend1.Ranking + ";" + default_imagefolder + attends.attend1.AttendImage); sb.AppendLine();
                sb.Append(attends.attend2.AttendName + ";" + attends.attend2.Ranking + ";" + default_imagefolder + attends.attend2.AttendImage); sb.AppendLine();
                sb.Append(attends.attend3.AttendName + ";" + attends.attend3.Ranking + ";" + default_imagefolder + attends.attend3.AttendImage); sb.AppendLine();
                sb.Append(attends.attend4.AttendName + ";" + attends.attend4.Ranking + ";" + default_imagefolder + attends.attend4.AttendImage); sb.AppendLine();
                sb.Append(attends.attend5.AttendName + ";" + attends.attend5.Ranking + ";" + default_imagefolder + attends.attend5.AttendImage); sb.AppendLine();
                sb.Append(attends.attend6.AttendName + ";" + attends.attend6.Ranking + ";" + default_imagefolder + attends.attend6.AttendImage); sb.AppendLine();
                sb.Append(attends.attend7.AttendName + ";" + attends.attend7.Ranking + ";" + default_imagefolder + attends.attend7.AttendImage); sb.AppendLine();
                sb.Append(attends.attend8.AttendName + ";" + attends.attend8.Ranking + ";" + default_imagefolder + attends.attend8.AttendImage); sb.AppendLine();
                sb.Append(attends.attend9.AttendName + ";" + attends.attend9.Ranking + ";" + default_imagefolder + attends.attend9.AttendImage); sb.AppendLine();
                sb.Append(attends.attend10.AttendName + ";" + attends.attend10.Ranking + ";" + default_imagefolder + attends.attend10.AttendImage); sb.AppendLine();
                sb.Append(attends.attend11.AttendName + ";" + attends.attend11.Ranking + ";" + default_imagefolder + attends.attend11.AttendImage); sb.AppendLine();
                sb.Append(attends.attend12.AttendName + ";" + attends.attend12.Ranking + ";" + default_imagefolder + attends.attend12.AttendImage); sb.AppendLine();
                sb.Append(attends.attend13.AttendName + ";" + attends.attend13.Ranking + ";" + default_imagefolder + attends.attend13.AttendImage); sb.AppendLine();
                sb.Append(attends.attend14.AttendName + ";" + attends.attend14.Ranking + ";" + default_imagefolder + attends.attend14.AttendImage); sb.AppendLine();
                sb.Append(attends.attend15.AttendName + ";" + attends.attend15.Ranking + ";" + default_imagefolder + attends.attend15.AttendImage); sb.AppendLine();
                System.IO.File.WriteAllText(pathThamdu, sb.ToString());
                ViewBag.ShowSuccess = "true";
                return RedirectToAction("Attend");
            }
            catch
            {
                ViewBag.ShowSuccess = "false";
                return RedirectToAction("Attend");
            }
            
        }
        #endregion
        private List<SelectListItem> getListImage()
        {
            var listFileName = new DirectoryInfo(Server.MapPath(default_imagefolder)).GetFiles().Select(o => o.Name).ToList();
            var ts = listFileName.Select(x => new SelectListItem { Text = x, Value = x })
             .ToList();
            return ts;
        }

        private List<SelectListItem> getList()
        {
            var listFileName = new DirectoryInfo(Server.MapPath(default_filefolder)).GetFiles().Select(o => o.Name).ToList();
            var ts = listFileName.Select(x => new SelectListItem { Text = x, Value = x })
             .ToList();
            return ts;
        }

        #region[FileUpload]
        public ActionResult FileUpload()
        {
            ViewBag.title = "Upload dữ liệu";
            string[] lineFile = System.IO.File.ReadAllLines(Server.MapPath("~/Content/danhsachfiledinhkem.txt"));
            var listFile = new List<ThumbnailFile>();
  
            if (lineFile != null && lineFile.Length > 0)
            {
                foreach (string line in lineFile)
                {
                    string[] subs = line.Split(';');
                    if (subs != null && subs.Length > 1)
                    {
                        var file = new ThumbnailFile
                        {
                            FileName = subs[0],
                            FileDescription = subs[2],
                            Url = subs[1]
                        };
                        listFile.Add(file);
                    }
                    // Use a tab to indent each line of the file.
                }
                while (listFile.Count < 9)
                {
                    listFile.Add(new ThumbnailFile { FileName = "", Url = "", FileDescription = "" });
                }
            }
            
            
            var allfiles = new Files
            {
                file1 = listFile[0],
                file2 = listFile[1],
                file3 = listFile[2],
                file4 = listFile[3],
                file5 = listFile[4],
                file6 = listFile[5],
                file7 = listFile[6],
                file8 = listFile[7],
                file9 = listFile[8],
                fileNames = getList()
            };
            ViewBag.ShowSuccess = "";
            
            return View(allfiles);
        }

        [HttpPost]
        public ActionResult FileUpload(Files files)
        {
            try
            {
                var pathDinhkem = Server.MapPath("~/Content/danhsachfiledinhkem.txt");
                
                StringBuilder sb = new StringBuilder();
                sb.Append(files.file1.FileName + ";" + default_filefolder + files.file1.Url+";"+ files.file1.FileDescription); sb.AppendLine();
                sb.Append(files.file2.FileName + ";" + default_filefolder + files.file2.Url + ";" + files.file2.FileDescription); sb.AppendLine();
                sb.Append(files.file3.FileName + ";" + default_filefolder + files.file3.Url + ";" + files.file3.FileDescription); sb.AppendLine();
                sb.Append(files.file4.FileName + ";" + default_filefolder + files.file4.Url + ";" + files.file4.FileDescription); sb.AppendLine();
                sb.Append(files.file5.FileName + ";" + default_filefolder + files.file5.Url + ";" + files.file5.FileDescription); sb.AppendLine();
                sb.Append(files.file6.FileName + ";" + default_filefolder + files.file6.Url + ";" + files.file6.FileDescription); sb.AppendLine();
                sb.Append(files.file7.FileName + ";" + default_filefolder + files.file7.Url + ";" + files.file7.FileDescription); sb.AppendLine();
                sb.Append(files.file8.FileName + ";" + default_filefolder + files.file8.Url + ";" + files.file8.FileDescription); sb.AppendLine();
                sb.Append(files.file9.FileName + ";" + default_filefolder + files.file9.Url + ";" + files.file9.FileDescription); sb.AppendLine();
                System.IO.File.WriteAllText(pathDinhkem, sb.ToString());
                ViewBag.ShowSuccess = "true";
                //files.fileNames = getList();
                return RedirectToAction("FileUpload");
            }
            catch
            {
                ViewBag.ShowSuccess = "false";
                return RedirectToAction("FileUpload");
            }
            
        }
        #endregion
    }
}