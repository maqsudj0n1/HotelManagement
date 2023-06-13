using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Room
{
    public class RoomCreateDto:RoomBaseDto
    {
        public Guid RoomClassId { get; set; }
    }
}
