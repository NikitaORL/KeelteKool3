using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKool.Models
{
    public class Training
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public DateTime AlgusKuupaev { get; set; }
        public DateTime LoppKuupaev { get; set; }

        public decimal Hind { get; set; }
        public int MaxOsalejaid { get; set; }

     
        public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    }
}