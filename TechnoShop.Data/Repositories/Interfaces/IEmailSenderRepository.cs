using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.EmailSenderEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface IEmailSenderServiceRepository
    {
        public Task<List<EmailSender>> GetEmailSenders();
        public Task<EmailSender> GetEmailSender();
        public Task AddEmailSender(string email, string password);
        public Task SaveChanges();
    }
}
