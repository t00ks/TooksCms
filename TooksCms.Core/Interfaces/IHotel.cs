using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TooksCms.Core.Interfaces
{
    public interface IHotel : IInterfacingBase
    {
        int HotelId { get; set; }
        string Name { get; set; }
        string Website { get; set; }
        string Number { get; set; }
        string Address { get; set; }
        string Image { get; set; }
        decimal Latitude { get; set; }
        decimal Longitude { get; set; }
    }
}
