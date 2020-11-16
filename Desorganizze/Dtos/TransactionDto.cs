using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Desorganizze.Dtos
{
    public class TransactionDto
    {
        public int Amount { get; set; }
        public byte Type { get; set; }
        public int AccountId { get; set; }
    }
}
