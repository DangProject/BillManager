using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Enums;
using BillManager.Client.Helpers;
using Core.UI;
using FluentValidation;

namespace BillManager.Client.Model
{    
    public class Bill : ModelEntityBase
    {
        public Bill()
        {
            payOptions = new List<PayOption>();
        }        
        int billId;     
        public int BillId
        {
            get { return billId; }
            set { billId = value; }
        }        
        string name;
        public string Name
        {
            get { return name; }
            set 
            {
                if (name != value)
                    name = value;
            }
        }        
        string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string accountNum;
        public string AccountNum
        {
            get { return accountNum; }
            set { accountNum = value; }
        }
        DateTime commenceDate = DateTime.Today;
        public DateTime CommenceDate
        {
            get { return commenceDate; }
            set { commenceDate = value; }
        }
        BillKind billKind;
        public BillKind BillKind
        {
            get { return billKind; }
            set { billKind = value; }
        }
        bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        BillFrequency billFrequency;
        public BillFrequency BillFrequency
        {
            get { return billFrequency; }
            set { billFrequency = value; }
        }
        int? dayDueInMonth;
        public int? DayDueInMonth
        {
            get { return dayDueInMonth; }
            set 
            {
                if (dayDueInMonth != value)
                    dayDueInMonth = value;
            }
        }
        bool autopayIsEnrolled;
        public bool AutopayIsEnrolled
        {
            get { return autopayIsEnrolled; }
            set { autopayIsEnrolled = value; }
        }
        decimal? initialBalance;
        public decimal? InitialBalance
        {
            get { return initialBalance; }
            set { initialBalance = value; }
        }        
        string phoneNum;
        public string PhoneNum
        {
            get { return phoneNum; }
            set { phoneNum = value; }
        }
        int accountId;
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }
        Category category;
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }
        ICollection<PayOption> payOptions;
        public ICollection<PayOption> PayOptions
        {
            get { return payOptions; }
            set { payOptions = value; }
        }       


        # region non persisted properties

        public int DaysLeft
        {
            get { return DueDate.Date.Subtract(DateTime.Today.Date).Days; }
        }

        public BillStatus BillStatus
        {
            get
            {
                if (commenceDate.Date > DateTime.Today.Date)
                    return BillStatus.CantStart;
                else if (!BillHelper.DetermineIsBillThisMonth(this).Item1)
                    return BillStatus.NoBillThisMonth;
                else if (IsPaid)
                    return BillStatus.Paid;
                else
                    return DueDate.Date < DateTime.Today.Date ? BillStatus.Late : BillStatus.NotPaid;
            }
        }

        public decimal? RemainingBalance { get; set; }  // this will be calculated by a service        
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime PaidDate { get; set; }
        public Payment LastPayment { get; set; }

        public bool NoBill
        {
            get
            {
                BillStatus bs = BillStatus;
                return (bs == BillStatus.Late || bs == BillStatus.NotPaid) ? false : true;
            }
        }

        public void NotifyPropertyChanges()
        {
            FirePropertyChanged("NoBill");
            FirePropertyChanged("DueDate");
            AnimateTransition = true;
        }

        bool animateTransition;
        public bool AnimateTransition
        {
            get { return animateTransition; }
            set 
            {
                if (animateTransition != value)
                {
                    animateTransition = value;
                    FirePropertyChanged("AnimateTransition");
                }
            }
        }
        public void ResetAnimation()
        {
            AnimateTransition = false;
        }
        # endregion

        protected override IValidator GetValidator()
        {
            return new BillValidator();
        }

        class BillValidator : AbstractValidator<Bill>
        {
            public BillValidator()
            {
                RuleFor(b => b.Name).NotEmpty().WithMessage("Bill name is required");
                RuleFor(b => b.BillKind).NotEmpty().WithMessage("Bill type is required");
                RuleFor(b => b.BillFrequency).NotEmpty().WithMessage("Bill frequency is required");
                RuleFor(b => b.DayDueInMonth).NotNull().WithMessage("Day due is required");                
            }
        }
    }
}
