using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KTNV_LiveDemo.Models
{
   
    public class Title
    {
        public string TitleName { get; set; }
        [AllowHtml]
        public string TitleContent { get; set; }
        
    }
}