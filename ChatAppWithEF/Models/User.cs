using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppWithEF.Models
{
    public class User
    {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; }
            public string Username { get; set; }
            public string NewUsername { get; set; }
            public string Password { get; set; }
            public DateTime CreatedDate { get; set; }
        
    }
}
