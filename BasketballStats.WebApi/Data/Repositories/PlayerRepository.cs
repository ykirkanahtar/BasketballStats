﻿using System.Linq;
using BasketballStats.WebApi.Models;
using CustomFramework.Data.Contracts;
using CustomFramework.Data.Repositories;
using CustomFramework.Data.Utils;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BasketballStats.WebApi.Data.Repositories
{
    public class PlayerRepository : BaseRepository<Player, int>, IPlayerRepository
    {
        public PlayerRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Player> GetByIdWithIncludeAsync(int playerId)
        {
            return await (from p in GetAll()
                          where p.Id == playerId
                          select p).Include(p => p.Stats).ThenInclude(p => p.Match)
                                   .Include(p => p.Stats).ThenInclude(p => p.Team)
                                   .FirstOrDefaultAsync();
        }

        public async Task<ICustomList<Player>> GetAllAsync()
        {
            return await GetAll().ToCustomList();
        }
    }
}