using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCC.Pharm.DataAccess.Entities
{
    /// <summary>
    /// Лекарственный препарат.
    /// </summary>
    [Table("Medications")]
    public class Medication
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Внешний ключ на <see cref="Entities.Substance"/>.
        /// </summary>
        [ForeignKey("Substance")]
        public int SubstanceId { get; set; }

        /// <summary>
        /// Действующее вещество.
        /// </summary>
        public Substance Substance { get; set; }
        
        /// <summary>
        /// Наименование.
        /// </summary>
        [Column("Name")]
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Цена за единицу.
        /// </summary>
        [Column("Price")]
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Количество.
        /// </summary>
        [Column("Quantity")]
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// История изменений.
        /// </summary>
        public ICollection<ChangeHistory> ChangesHistory { get; set; }
    }
}