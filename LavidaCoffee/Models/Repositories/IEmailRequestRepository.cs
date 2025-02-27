using System;

namespace LavidaCoffee.Models;

public interface IEmailRequestRepository
{
    Task<IEnumerable<EmailRequest>> GetAllEmailRequestsAsync();
    Task<EmailRequest?> GetEmailRequestByIdAsync(int emailRequestId);
    void CreateEmailRequest(EmailRequest emailRequest);
    IEnumerable<EmailRequest> requestsForCurrentPage(int page);
}
