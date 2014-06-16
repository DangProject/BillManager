
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Business.Entities;

namespace BillManager.Client.Model
{
    public class Account
    {
        public Account()
        {
            //favoriteLinks = new List<FavoriteLink>();
        }
        int accountId;
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }
        string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        ICollection<FavoriteLink> favoriteLinks;
        public ICollection<FavoriteLink> FavoriteLinks
        {
            get { return favoriteLinks; }
            set { favoriteLinks = value; }
        }
    }
}
