using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppWithEF.Models
{
    public class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
