using System;
using MCBA_Web.Data;
using MCBA_Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MCBA_Web.Utilities
{
    public static class MiscellaneousExtensionUtilities
    {
        public static bool HasMoreThanNDecimalPlaces(this decimal value, int n) => decimal.Round(value, n) != value;

        public static bool HasMoreThanTwoDecimalPlaces(this decimal value) => value.HasMoreThanNDecimalPlaces(2);

        public static bool HasInsufficientBalance(this Account account, decimal amount, AccountType accountType)
        {
            bool hasInsufficientBalance = false;

            if (accountType.Equals(AccountType.Savings))
                hasInsufficientBalance = (account.Balance - amount) < 0;
            else if (accountType.Equals(AccountType.Checking))
                hasInsufficientBalance = (account.Balance - amount) < 300;

            return hasInsufficientBalance;
        }

        public static decimal ServiceCharge(this TransactionType transactionType)
        {
            decimal serviceCharge = 0.00M;

            if (transactionType.Equals(TransactionType.Withdraw))
                serviceCharge = 0.05M;
            else if (transactionType.Equals(TransactionType.Transfer))
                serviceCharge = 0.10M;

            return serviceCharge;
        }
        
    }
}

