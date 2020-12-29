using System;
using System.ComponentModel.DataAnnotations;

namespace Desorganizze.Dtos
{
    public class TransferBetweenAccountsDto
    {
        [Required(ErrorMessage = "You must informe a source account")]
        public Guid SourceAccountId { get; set; }

        [Required(ErrorMessage = "You must informe a recipient account")]
        public Guid RecipientAccountId { get; set; }

        [Required(ErrorMessage = "You must informe the amount")]
        [Range(0.0, double.MaxValue, ErrorMessage = "You must informe a positive amount")]
        public decimal Amount { get; set; }
    }
}
