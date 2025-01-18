using System;

namespace LavidaCoffee.Models;

public interface IEmailRequestRepository
{
    List<EmailRequest> AllEmailRequests();
    EmailRequest? GetEmailRequestById(int emailRequestId);
    void CreateEmailRequest(EmailRequest emailRequest);
    IEnumerable<EmailRequest> requestsForCurrentPage(int page);
}
