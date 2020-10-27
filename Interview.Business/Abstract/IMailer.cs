using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Business.Abstract
{
   public interface IMailer
   {
       Task SendEmailAsync(string email, string subject, string body);

   }
}
