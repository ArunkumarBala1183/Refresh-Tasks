using EmployeeManagement.Models.Application_Models;
using System.Net;

namespace EmployeeManagement.Repository.Services
{
    public interface IEmailService
    {
        Task SendEmail(EmailRequest mailContent);
    }
}
