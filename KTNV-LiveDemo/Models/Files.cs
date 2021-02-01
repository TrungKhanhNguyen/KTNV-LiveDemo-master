using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTNV_LiveDemo.Models
{
    public class Files
    {
        public ThumbnailFile file1 { get; set; }
        public ThumbnailFile file2 { get; set; }
        public ThumbnailFile file3 { get; set; }
        public ThumbnailFile file4 { get; set; }
        public ThumbnailFile file5 { get; set; }
        public ThumbnailFile file6 { get; set; }
        public ThumbnailFile file7 { get; set; }
        public ThumbnailFile file8 { get; set; }

        public ThumbnailFile file9 { get; set; }

        public List<SelectListItem> fileNames { get; set; }
    }
}