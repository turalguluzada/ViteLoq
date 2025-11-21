using Microsoft.EntityFrameworkCore;
using ViteLoq.Domain.Auth.Entities;
using ViteLoq.Domain.Token.Interfaces;
using ViteLoq.Infrastructure.Persistence;

namespace ViteLoq.Infrastructure.Repositories.Auth
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ViteLoqDbContext _ctx;
        
        public RefreshTokenRepository(ViteLoqDbContext ctx) => _ctx = ctx;

        public async Task AddAsync(RefreshToken token, CancellationToken ct = default)
        {
            await _ctx.Set<RefreshToken>().AddAsync(token, ct);
        }

        public Task<RefreshToken?> GetByHashAsync(string tokenHash, CancellationToken ct = default)
        {
            return _ctx.Set<RefreshToken>().FirstOrDefaultAsync(t => t.TokenHash == tokenHash, ct);
        }

        public Task<IEnumerable<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        {
            return _ctx.Set<RefreshToken>().Where(t => t.UserId == userId).ToArrayAsync(ct).ContinueWith(t => (IEnumerable<RefreshToken>)t.Result, TaskScheduler.Current);
        }

        public Task UpdateAsync(RefreshToken token, CancellationToken ct = default)
        {
            _ctx.Set<RefreshToken>().Update(token);
            return Task.CompletedTask;
        }
    }
}