using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCC.Pharm.DataAccess.Entities
{
    /// <summary>
    /// Активное вещество.
    /// </summary>
    [Table("ActiveSubstances")]
    public class Substance
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        [Column("Name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Препараты, в которых содержатся действующее вещество.
        /// </summary>
        public ICollection<Medication> Medications { get; set; }
    }
}