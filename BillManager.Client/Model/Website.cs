using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.UI;
using FluentValidation;

namespace BillManager.Client.Model
{
    public class Website : ModelEntityBase
    {
        int websiteId;
        public int WebsiteId
        {
            get { return websiteId; }
            set { websiteId = value; }
        }        
        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        string urlString;
        public string UrlString
        {
            get { return urlString; }
            set { urlString = value; }
        }        
        public Uri Url
        {
            get
            {
                Uri url;
                Uri.TryCreate(urlString, UriKind.RelativeOrAbsolute, out url);
                return url;
            }
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
        int accountId;
        public int AccountId
        {
            get { return accountId; }
            set { accountId = value; }
        }

        protected override IValidator GetValidator()
        {
            return new WebsiteValidator();
        }
        class WebsiteValidator : AbstractValidator<Website>
        {
            public WebsiteValidator()
            {
                RuleFor(w => w.Name).NotEmpty().WithMessage("Name is required");
                RuleFor(w => w.UrlString).NotEmpty().WithMessage("Url is required");
                //.Must(s =>
                //    {
                //        //s == string.Empty ? true :
                //        Uri url;                        
                //        Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out url);
                //        bool r = url != default(Uri);
                //        return r;
                //    }).WithMessage("Url is not valid");
            }
        }
    }
}
