using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace TooksCms.ServiceLayer.Models
{
    public class ImageListItem : SelectListItem
    {
        public ImageListItem() { }
        public ImageListItem(SelectListItem item)
        {
            this.Selected = item.Selected;
            this.Text = item.Text;
            this.Value = item.Value;
        }

        public string BackgroundImage { get; set; }
    }
}
