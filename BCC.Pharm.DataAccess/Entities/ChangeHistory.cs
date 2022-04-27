using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCC.Pharm.Shared;

namespace BCC.Pharm.DataAccess.Entities
{
    /// <summary>
    /// История измнения параметра лекарственного препарата.
    /// </summary>
    public class ChangeHistory
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Внешний ключ на <see cref="Entities.Medication"/>.
        /// </summary>
        [ForeignKey("MedicationId")]
        public int MedicationId { get; set; }

        /// <summary>
        /// Какому припарату принадлежит изменение.
        /// </summary>
        public Medication Medication { get; set; }

        /// <summary>
        /// Как была изменен параметр.
        /// </summary>
        [Column("SourceChangeId")]
        [Required]
        public SourceChange SourceChange { get; set; }

        /// <summary>
        /// Какой параметр поменялся.
        /// </summary>
        [Column("Parameter")]
        [Required]
        public string Parameter { get; set; }

        /// <summary>
        /// Значение, которое было до изменения.
        /// </summary>
        [Column("ValueBefore")]
        public string ValueBefore { get; set; }

        /// <summary>
        /// Значение которое стало после изменения.
        /// </summary>
        [Column("ValueAfter")]
        public string ValueAfter { get; set; }

        /// <summary>
        /// Дата создания изменения.
        /// </summary>
        [Column("Created")]
        public DateTime Created { get; set; }
    }
}