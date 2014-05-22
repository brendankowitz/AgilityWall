using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgilityWall.Core.Contracts;
using Task = System.Threading.Tasks.Task;

namespace AgilityWall.Core.Repositories
{
    public interface IBoardRepository
    {
        Task<IEnumerable<Board>> Fetch(Predicate<Board> filter);
        Task<Board> Get(Guid id);
        Task Save(Board board);
        Task Delete(Board board);
    }
}