using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Porte.CaseStudy.Models
{

    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PART_ID { get; set; }
        public int BOX_ID { get; set; }
        public int PART_NUMBER { get; set; }
        public int PART_WEIGHT { get; set; }
        public int PART_COST { get; set; }
    }
}
