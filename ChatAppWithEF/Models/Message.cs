using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAppWithEF.Models
{
    public class Message
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
      //  public DateTime CreatedDate { get; set; }
    }
}
