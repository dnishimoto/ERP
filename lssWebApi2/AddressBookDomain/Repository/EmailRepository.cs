using ERP_Core2.AbstractFactory;
using System;
using System.Linq;
using System.Threading.Tasks;
using ERP_Core2.Services;
using ERP_Core2.AccountPayableDomain;
using lssWebApi2.entityframework;
using Microsoft.EntityFrameworkCore;

namespace ERP_Core2.AddressBookDomain
{


    public class EmailView
    {
        public EmailView() { }
        public EmailView(Emails email)
        {
            this.AddressId = email.AddressId;
            this.EmailId = email.EmailId;
            this.EmailText = email.Email;
            this.LoginEmail = email.LoginEmail ?? false;
            this.Password = email.Password;
        }
        public long AddressId { get; set; }
        public long EmailId { get; set; }
        public string EmailText { get; set; }
        public bool LoginEmail { get; set; }
        public string Password { get; set; }
    }

    public class EmailRepository : Repository<Emails> {
        private ApplicationViewFactory applicationViewFactory;

        ListensoftwareDBContext _dbContext;
        public EmailRepository(DbContext db) : base(db)
        {
            _dbContext = (ListensoftwareDBContext)db;
            applicationViewFactory = new ApplicationViewFactory();
        }
        public async Task<CreateProcessStatus> CreateEmail(EmailView emailView)
        {
            try
            {
                var query = await (from e in _dbContext.Emails
                                   where e.Email == emailView.EmailText
                                   select e).FirstOrDefaultAsync<Emails>();
                if (query == null)
                {
                    Emails email = new Emails();
                    applicationViewFactory.MapEmailEntity(ref email, emailView);
                    AddObject(email);
                    return CreateProcessStatus.Insert;
                }
                return CreateProcessStatus.AlreadyExists;
            }
            catch (Exception ex)
            { throw new Exception(GetMyMethodName(), ex); }
        }

    }
}
