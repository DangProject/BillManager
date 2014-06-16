using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;
using BillManager.Business.ServiceContracts;
using BillManager.Data.Interfaces;
using Core.Data.Interfaces;
using Core.Exceptions;

namespace BillManager.Business.ServiceManagers
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
                     ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class PaymentServiceManager : ServiceManagerBase, IPaymentService
    {
        public PaymentServiceManager()
        {
        }
        [Import]
        IDataRepositoryFactory dataRepositoryFactory;
        public PaymentServiceManager(IDataRepositoryFactory dataRepositoryFactory)
        {
            this.dataRepositoryFactory = dataRepositoryFactory;
        }

        public Payment MakePayment(Payment payment)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.Add(payment);
            });
        }

        public IEnumerable<Payment> GetAllPaymentsByBill(int billId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetAllPaymentsByBill(billId);
            });
        }

        public IEnumerable<Payment> GetPaymentsByBill(DateTime fromDate, DateTime toDate, int billId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetPaymentsByBill(fromDate, toDate, billId);
            });
        }

        public IEnumerable<Payment> GetPaymentsByBillForMonth(DateTime month, int billId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetPaymentsByBillForMonth(month, billId);
            });
        }

        public IEnumerable<Payment> GetAllPaymentsByAccountForMonth(DateTime month, int accountId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetAllPaymentsByAccountForMonth(month, accountId);
            });
        }

        public IEnumerable<Payment> GetAllPaymentsByAccountForMonths(DateTime fromMonth, DateTime toMonth, int accountId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetAllPaymentsByAccountForMonths(fromMonth, toMonth, accountId);
            });
        }

        public Payment GetCurrentMonthPaymentByBill(int billId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetCurrentMonthPaymentByBill(billId);
            });
        }

        public IEnumerable<Payment> GetCurrentMonthPaymentsByAccount(int accountId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetCurrentMonthPaymentsByAccount(accountId);
            });
        }

        public int GetTotalPaymentCountByBill(int billId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetTotalPaymentCountByBill(billId);
            });
        }

        public int GetPaymentCountByBill(DateTime fromDate, DateTime toDate, int billId)
        {
            return FaultHandledOperation(() =>
            {
                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                return paymentRepository.GetPaymentCountByBill(fromDate, toDate, billId);
            });
        }
        
        public decimal? GetRemainingBalanceByBill(int billId)
        {
            return FaultHandledOperation(() =>
            {
                IBillRepository billRepository = dataRepositoryFactory.GetDataRepository<IBillRepository>();

                Bill bill = billRepository.Get(billId);
                if (bill == null)
                {
                    NotFoundException ex = new NotFoundException(string.Format("No bill found for id {0}", billId));
                    throw new FaultException<NotFoundException>(ex, ex.Message);
                }

                if (bill.BillKind != BillKind.Paydown)
                {
                    NotOfTypeException ex = new NotOfTypeException(string.Format("{0} bill is not of a paydown type account", bill.Name));
                    throw new FaultException<NotOfTypeException>(ex, ex.Message);
                }

                IPaymentRepository paymentRepository = dataRepositoryFactory.GetDataRepository<IPaymentRepository>();
                decimal paymentTotal = paymentRepository.GetAllPaymentsByBill(bill.BillId).Sum(p => p.Amount);

                return bill.InitialBalance - paymentTotal;
            });
        }
    }
}
