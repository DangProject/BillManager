using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Model;
using BillManager.Client.ServiceContracts;
using BillManager.Desktop.Interfaces;
using Core.ServiceModel.Contracts;
using Core.UI;
using Core.Extensions;
using BillManager.Desktop.Enums;
using BillManager.Client.Enums;
using BillManager.Client.Helpers;
using Core;
using Core.Interfaces;

namespace BillManager.Desktop
{
    public class PaymentController : ServiceClientConsumerBase, IPaymentController
    {
        IServiceFactory serviceFactory;
        IEntityMapper mapper;
        public PaymentController(IServiceFactory serviceFactory, IEntityMapper mapper)
        {
            this.serviceFactory = serviceFactory;
            this.mapper = mapper;
        }

        public IEnumerable<Bill> GetCurrentBills()
        {
            IList<Payment> payments = new List<Payment>();
            IList<Bill> bills = new List<Bill>();

            //Temp comment
            UsingServiceClient(serviceFactory.CreateServiceClient<IPaymentService>(), paymentService =>
            {
                paymentService.GetAllPaymentsByAccountForMonths(DateTime.Now.AddMonths(-1).FirstDayOfMonth(),
                                                                DateTime.Now.AddMonths(2).FirstDayOfMonth(),
                                                                SessionData.Instance.Account.AccountId)
                                                               .OrderBy(p => p.BillId).ThenByDescending(p => p.PaymentMonthApplied)
                                                               .ForEach(p => payments.Add(mapper.Map<Business.Entities.Payment, Payment>(p)));
            });

            UsingServiceClient(serviceFactory.CreateServiceClient<IAccountService>(), accountServiceClient =>
            {
                accountServiceClient.GetAllActiveBills(SessionData.Instance.Account.AccountId)
                    .ForEach(b => bills.Add(mapper.Map<Business.Entities.Bill, Bill>(b)));

                bills.ForEach(b => 
                {
                    b.PayOptions.ForEach(p => p.Bill = b);
                    b.PayOptions = b.PayOptions.OrderBy(p => p.IsPrimary ? 0 : 1).ToList();
                });
            });

            foreach (Bill b in bills)
                UpdateBill(b, payments.FirstOrDefault(p => p.BillId.Equals(b.BillId)));

            return bills.OrderBy(b => b.BillStatus).ThenBy(b =>
            {
                int days = b.DueDate.Subtract(DateTime.Today.Date).Days;
                if (b.BillStatus == BillStatus.Paid)
                    return -days;
                else
                    return days;
            });
        }

        DateTime now;
        DateTime currentMonthDueDate;
        DateTime lastMonth;
        public void UpdateBill(Bill bill, Payment payment)
        {
            now = DateTime.Now;
            lastMonth = DateTime.Now.AddMonths(-1);
            bill.IsPaid = false;
            bill.PaidDate = default(DateTime);
            bill.LastPayment = default(Payment);

            currentMonthDueDate = BillHelper.ValidateDate(now.Year, now.Month, bill.DayDueInMonth.Value);
            Tuple<bool, DateTime> validate = BillHelper.DetermineIsBillThisMonth(bill);
            if (validate.Item1)
            {
                if (payment != default(Payment))
                {
                    bill.LastPayment = payment;                    
                    switch (DeterminePaymentMonth(payment.PaymentMonthApplied))
                    {
                        case PaymentMonth.LastMonth:
                            bill.DueDate = currentMonthDueDate;
                            break;
                        case PaymentMonth.CurrentMonth:
                            Tuple<bool, DateTime> verdict = ValidateNextMonthsBillDateIsInRange(bill.DayDueInMonth.Value);

                            if (verdict.Item1)
                                bill.DueDate = verdict.Item2;
                            else
                            {
                                //bill.DueDate = currentMonthDueDate;
                                bill.DueDate = verdict.Item2;
                                bill.IsPaid = true;
                                bill.PaidDate = payment.DatePaid;
                            }

                            break;
                        case PaymentMonth.NextMonth:
                            bill.DueDate = GetNextMonthsDueDate(payment.PaymentMonthApplied.Month, bill.DayDueInMonth.Value);
                            bill.IsPaid = true;
                            bill.PaidDate = payment.DatePaid;
                            break;
                    }
                }
                else
                {
                    if (bill.CommenceDate <= DateTime.Today)
                    {
                        DateTime lastMonthDueDate = BillHelper.ValidateDate(lastMonth.Year, lastMonth.Month, bill.DayDueInMonth.Value);
                        bill.DueDate = lastMonthDueDate >= bill.CommenceDate ? lastMonthDueDate : currentMonthDueDate;
                    }
                    else
                    {
                        DateTime dueDate = BillHelper.ValidateDate(bill.CommenceDate.Year, bill.CommenceDate.Month, bill.DayDueInMonth.Value);
                        bill.DueDate = dueDate.Date < bill.CommenceDate.Date ? dueDate.AddMonths(1) : dueDate;
                    }
                }
            }
            else
            {
                // No bill this month
                bill.DueDate = validate.Item2;
            }
        }
        
        Tuple<bool, DateTime> ValidateNextMonthsBillDateIsInRange(int day)
        {
            DateTime nextMonthBillDate = GetNextMonthsDueDate(DateTime.Now.Month, day);
            DateTime limit = DateTime.Today.AddDays(21);

            if (nextMonthBillDate <= limit)
                return new Tuple<bool, DateTime>(true, nextMonthBillDate);
            else
                return new Tuple<bool, DateTime>(false, nextMonthBillDate);
        }

        DateTime GetNextMonthsDueDate(int month, int day)
        {
            DateTime date = BillHelper.ValidateDate(DateTime.Now.Year, month, day);
            return date.AddMonths(1);
        }

        PaymentMonth DeterminePaymentMonth(DateTime paymentDate)
        {
            int currentYearMonth = DateTime.Today.YearMonth();

            if (paymentDate.YearMonth() < currentYearMonth)
                return PaymentMonth.LastMonth;
            else
                return paymentDate.YearMonth() == currentYearMonth ? PaymentMonth.CurrentMonth : PaymentMonth.NextMonth;            
        }
    }
}
















//Payment payment;
//DateTime now = DateTime.Now;
//DateTime currentMonthDueDate;
//DateTime lastMonth = DateTime.Now.AddMonths(-1);
//foreach (Bill b in bills)
//{
//    Tuple<bool, DateTime> validate = BillHelper.DetermineIsBillThisMonth(b);
//    if (validate.Item1)
//    {
//        payment = payments.FirstOrDefault(p => p.BillId.Equals(b.BillId));

//        if (payment != default(Payment))
//        {
//            currentMonthDueDate = BillHelper.ValidateDate(now.Year, now.Month, b.DayDueInMonth);
//            switch (DeterminePaymentMonth(payment.PaymentMonthApplied))
//            {
//                case PaymentMonth.LastMonth:
//                    b.DueDate = currentMonthDueDate;
//                    break;
//                case PaymentMonth.CurrentMonth:
//                    Tuple<bool, DateTime> verdict = ValidateNextMonthsBillDateIsInRange(b.DayDueInMonth);

//                    if (verdict.Item1)
//                        b.DueDate = verdict.Item2;
//                    else
//                    {
//                        b.DueDate = currentMonthDueDate;
//                        b.IsPaid = true;
//                        b.PaidDate = payment.DatePaid;
//                    }

//                    break;
//                case PaymentMonth.NextMonth:
//                    b.DueDate = GetNextMonthsDueDate(b.DayDueInMonth);
//                    b.IsPaid = true;
//                    b.PaidDate = payment.DatePaid;
//                    break;
//            }
//        }
//        else
//        {
//            if (b.CommenceDate.Date <= DateTime.Today.Date)
//                b.DueDate = BillHelper.ValidateDate(lastMonth.Year, lastMonth.Month, b.DayDueInMonth);
//            else
//            {
//                DateTime dueDate = BillHelper.ValidateDate(b.CommenceDate.Year, b.CommenceDate.Month, b.DayDueInMonth);
//                b.DueDate = dueDate.Date < b.CommenceDate.Date ? dueDate.AddMonths(1) : dueDate;
//            }
//        }
//    }
//    else
//    {
//        // No bill this month
//        b.DueDate = validate.Item2;
//    }
//}