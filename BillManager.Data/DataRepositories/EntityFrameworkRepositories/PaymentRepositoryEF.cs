using BillManager.Business.Entities;
using BillManager.Data.AbstractRepositories;
using BillManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using System.ComponentModel.Composition;

namespace BillManager.Data.DataRepositories.EntityFrameworkRepositories
{
    [Export(typeof(IPaymentRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PaymentRepositoryEF : EntityFrameworkRepository<Payment>, IPaymentRepository
    {
        protected override Payment AddEntity(BillManagerContext context, Payment entity)
        {
            return context.Payments.Add(entity);
        }

        protected override Payment GetEntity(BillManagerContext context, int id)
        {
            return context.Payments.Find(id);
        }

        protected override Payment UpdateEntity(BillManagerContext context, Payment entity)
        {
            return context.Payments.FirstOrDefault(p => p.PaymentId == entity.PaymentId);
        }

        protected override IEnumerable<Payment> GetEntities(BillManagerContext context)
        {
            return context.Payments.AsEnumerable();
        }

        public IEnumerable<Payment> GetAllPaymentsByBill(int billId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        join p in context.Payments on b.BillId equals p.BillId
                        where b.BillId == billId
                        select p).ToArray();
            }
        }

        public IEnumerable<Payment> GetPaymentsByBill(DateTime fromDate, DateTime toDate, int billId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        join p in context.Payments on b.BillId equals p.BillId
                        where b.BillId == billId
                        where p.DatePaid >= fromDate && p.DatePaid <= toDate
                        select p).ToArray();
            }
        }

        public IEnumerable<Payment> GetPaymentsByBillForMonth(DateTime month, int billId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from p in context.Payments
                        where p.BillId == billId
                        where p.PaymentMonthApplied.Month == month.Month
                        where p.PaymentMonthApplied.Year == month.Year
                        select p).ToArray();
            }
        }

        public IEnumerable<Payment> GetAllPaymentsByAccountForMonth(DateTime month, int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from p in context.Payments
                        join b in context.Bills on p.BillId equals b.BillId
                        where b.AccountId == accountId                        
                        where p.PaymentMonthApplied.Month == month.Month
                        where p.PaymentMonthApplied.Year == month.Year
                        select p).ToArray();
            }
        }

        public IEnumerable<Payment> GetAllPaymentsByAccountForMonths(DateTime fromMonth, DateTime toMonth, int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from p in context.Payments
                        join b in context.Bills on p.BillId equals b.BillId
                        where b.AccountId == accountId
                        where p.PaymentMonthApplied >= fromMonth && p.PaymentMonthApplied < toMonth
                        select p).ToArray();
            }
        }

        public Payment GetCurrentMonthPaymentByBill(int billId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        join p in context.Payments on b.BillId equals p.BillId
                        where b.BillId == billId && p.DatePaid.Month == DateTime.Now.Month
                        select p).FirstOrDefault();
            }
        }

        public IEnumerable<Payment> GetCurrentMonthPaymentsByAccount(int accountId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        join p in context.Payments on b.BillId equals p.BillId
                        where b.AccountId == accountId
                        where p.DatePaid.Month == DateTime.Now.Month
                        select p).ToArray();
            }
        }

        public int GetTotalPaymentCountByBill(int billId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        join p in context.Payments on b.BillId equals p.BillId
                        where b.BillId == billId
                        select p).Count();
            }
        }

        public int GetPaymentCountByBill(DateTime fromDate, DateTime toDate, int billId)
        {
            using (BillManagerContext context = new BillManagerContext())
            {
                return (from b in context.Bills
                        join p in context.Payments on b.BillId equals p.BillId
                        where b.BillId == billId
                        where p.DatePaid >= fromDate && p.DatePaid <= toDate
                        select p).Count();
            }
        }
    }
}
