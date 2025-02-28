using System;

namespace LavidaCoffee.Models;

public interface IEmailRepository
{
    Task<IEnumerable<Email>> GetAllEmailRequestsAsync();
    Task<Email?> GetEmailRequestByIdAsync(int emailRequestId);
    Task CreateEmailRequestAsync(Email emailRequest);
    IEnumerable<Email> requestsForCurrentPage(int page);
}
