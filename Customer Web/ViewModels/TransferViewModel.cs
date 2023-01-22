﻿using System;
using System.ComponentModel.DataAnnotations;
using MCBA_Web.Models;

namespace MCBA_Web.ViewModels
{
<<<<<<< HEAD
	public class TransferViewModel : TransactionViewModel
=======
	public class TransferViewModel
>>>>>>> dev
    {
        public int AccountNumber { get; set; }
        public Account Account { get; set; }

        public int? DestinationAccountNumber { get; set; }
        public Account DestinationAccount { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive.")]
        public decimal Amount { get; set; }

        [StringLength(30, ErrorMessage = "Comment must be less than 30 characters.")]
        public string Comment { get; set; }
	}
}

