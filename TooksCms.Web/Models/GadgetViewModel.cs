﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace TooksCms.Web.Models
{
    public class GadgetViewModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string InnerView { get; set; }
        public object Model { get; set; }
    }
}