using System;
using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class TransactionDto
    {
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
        [Required]
        public byte Type { get; set; }
        [Required]
        public Guid AccountId { get; set; }
    }
}
