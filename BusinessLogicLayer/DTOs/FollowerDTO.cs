using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class FollowerDTO
    {
        public int UserId { get; set; }
        public int FollowsId { get; set; }
        public string Username { get; set; } 

    }
}