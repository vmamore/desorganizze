namespace Desorganizze.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewTransferDto
    {
        [Required]
        public Guid WalletId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }
    }
}
