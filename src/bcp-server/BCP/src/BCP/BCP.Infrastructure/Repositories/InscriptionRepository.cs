using BCP.Application.Interfaces;
using BCP.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace BCP.Infrastructure.Repositories
{
    public class InscriptionRepository : BaseRepository, IInscriptionRepository
    {
        private readonly ILogger<BaseRepository> _logger;

        public InscriptionRepository(AppDbContext dbContext, ILogger<BaseRepository> logger)
            : base(dbContext, logger)
        {
            _logger = logger;
        }

    }
}
