using System;
using MCBA_Web.Models;

namespace MCBA_Web.ViewModels
{
	public class BillPayViewModel
	{
        public int BillPayID { get; set; }

        public int AccountNumber { get; set; }

        public int PayeeID { get; set; }

        public decimal Amount { get; set; }

        public DateTime ScheduleTimeUtc { get; set; }

        public PaymentPeriodType PaymentPeriod { get; set; }

        public virtual Account Account { get; set; }

        public virtual Payee Payee { get; set; }
    }
}

