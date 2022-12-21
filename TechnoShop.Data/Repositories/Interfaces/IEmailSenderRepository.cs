using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoShop.Entities.EmailSenderEntity;

namespace TechnoShop.Data.Repositories.Interfaces
{
    public interface IEmailSenderRepository
    {
        List<EmailSender> GetEmailSenders();  
    }
}
