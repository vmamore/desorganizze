using System;
using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class TransactionDto
    {
        /// <summary>
        /// Amount of the transaction
        /// </summary>
        /// <example>0.01 - double.MaxValue</example>
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }


        /// <summary>
        /// Type of the transaction
        /// </summary>
        /// <example>0 - Add</example>
        /// <example>1 - Subtract</example>
        [Required]
        public byte Type { get; set; }
    }
}
