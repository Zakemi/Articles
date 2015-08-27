using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Services.Models;

namespace Articles.Models
{
    public class AdminPageModel
    {
        public ItemModel NewItem { get; set; }
        public string[] taggles { get; set; }
    }
}