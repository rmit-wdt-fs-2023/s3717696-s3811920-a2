using System;
using MCBA_Web.Models;

namespace MCBA_Web.ViewModels
{
	public class TransactionViewModel
	{
        public int AccountNumber { get; set; }
        public Account Account { get; set; }

        public int? DestinationAccountNumber { get; set; }
        public Account DestinationAccount { get; set; }

        public decimal Amount { get; set; }

        public string Comment { get; set; }
    }
}

