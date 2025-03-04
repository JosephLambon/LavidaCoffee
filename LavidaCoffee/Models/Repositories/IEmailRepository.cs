using System;

namespace LavidaCoffee.Models;

public interface IEmailRepository
{
    Task<IEnumerable<Email>> GetAllEmailsAsync();
    Task<Email?> GetEmailByIdAsync(int emailRequestId);
    Task CreateEmailAsync(Email emailRequest);
    Task<int> GetAllEmailsCountAsync();
    Task<IEnumerable<Email>> GetEmailsPagedAsync(int? pageNumber, int pageSize);
	Task<IEnumerable<Email>> GetEmailsPagedAndSortedAsync(string sortBy, int? pageNumber, int pageSize);
}
