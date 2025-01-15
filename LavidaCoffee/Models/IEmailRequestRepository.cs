using System;

namespace LavidaCoffee.Models;

public interface IEmailRequestRepository
{
    IEnumerable<EmailRequest> AllEmailRequests {  get; }
    EmailRequest? GetEmailRequestById(int emailRequestId);
}
