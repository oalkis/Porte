using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Porte.CaseStudy.Models
{
    public class Box
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BOX_ID { get; set; }
        public int WEIGHT { get; set; }
        public int PART_COUNT { get; set; }
    }
}
