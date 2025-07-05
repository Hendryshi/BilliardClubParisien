using BCP.Application.Interfaces;
using BCP.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BCP.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly ILogger<BaseRepository> _logger;

        public UserRepository(AppDbContext dbContext, ILogger<BaseRepository> logger)
            : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
