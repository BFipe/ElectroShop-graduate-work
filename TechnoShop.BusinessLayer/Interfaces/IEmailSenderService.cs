using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.BusinessLayer.Interfaces
{
    public interface IEmailSenderService : IEmailSender
    {
        public Task AddEmailSender(string email, string password);
        public Task DeleteEmailSender(string email);
        public Task<List<string>> GetEmailSenders();
    }
}
