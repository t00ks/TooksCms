using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooksCms.Core.Bases;
using TooksCms.Core.Interfaces;

namespace TooksCms.ServiceLayer.Objects.Wedding
{
    public class Hotel : InterfacingBase, IHotel
    {
        public Hotel(IHotel data) :
            base(data, typeof(IHotel))
        {

        }

        public int HotelId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Website { get; set; }

        public string Number { get; set; }

        public string Image { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
