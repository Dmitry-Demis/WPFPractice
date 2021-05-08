using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFPractice.Model
{
    public class Parametres
    {
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string NameOfParametre { get; set; }

    }
}
