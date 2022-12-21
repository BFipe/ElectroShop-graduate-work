using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Data.Repositories.Interfaces;
using TechnoShop.Entities.EmailSenderEntity;

namespace TechnoShop.Data.Repositories
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailSenderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<EmailSender> GetEmailSenders()
        {
            return _dbContext.EmailSenders.ToList();
        }
    }
}
