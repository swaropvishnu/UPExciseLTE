using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UPExciseLTE
{
    public class Menu
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public int? parentId { get; set; }
        //public int parentId { get; set; }
        public bool isActive { get; set; }
        public string NavURL { get; set; }
        public string Icon { get; set; }
        public List<Menu> List { get; set; } 
    }
}