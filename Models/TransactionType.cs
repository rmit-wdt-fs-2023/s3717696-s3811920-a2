﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCBA_Web.Models;

public enum TransactionType
{
    Transfer,
    Withdraw,
    Deposit,
    ServiceCharge,
    BillPay
}