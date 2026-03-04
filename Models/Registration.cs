using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKool.Models
{
    public class Registration
    {
        public int Id { get; set; }

        public int TrainingId { get; set; }
        public Training Training { get; set; }

        public string ApplicationUserId { get; set; }


        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Staatus { get; set; } 
    }
}