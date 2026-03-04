using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeelteKool.Models
{

    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Nimi { get; set; }

        public string Kvalifikatsioon { get; set; }
        public string FotoPath { get; set; }

        // связь с аккаунтом
        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }

}