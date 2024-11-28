using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Models
{
        public class User
        {
            [Key]
            public int Id { get; set; }
            public string Phone { get; set; }
            public string Name { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }

            public string IsAdmin { get; set; }

            public int CatId { get; set; }

            [NotMapped]
            public string Categorys { get; set; }
        }
  
}
