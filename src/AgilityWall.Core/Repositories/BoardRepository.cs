using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgilityWall.Core.Infrastructure;
using PortableTrello.Contracts;
using Task = System.Threading.Tasks.Task;

namespace AgilityWall.Core.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly IObjectStorageService<IEnumerable<Board>> _boardStorageService;
        private readonly string _storageKey;

        public BoardRepository(IObjectStorageService<IEnumerable<Board>> boardStorageService)
        {
            _boardStorageService = boardStorageService;
            _storageKey = _boardStorageService.CreateStorageKey("TaskBoards");
        }

        async Task<IEnumerable<Board>> GetFromStorage()
        {
            return await _boardStorageService.LoadAsync(_storageKey);
        }

        Task SaveToStorage(IEnumerable<Board> data)
        {
            return _boardStorageService.SaveAsync(_storageKey, data);
        }

        public async Task<IEnumerable<Board>> Fetch(Predicate<Board> filter)
        {
            var items = await GetFromStorage();
            if (filter != null)
                items = items.Where(x => filter(x));
            return items;
        }

        public async Task<Board> Get(string id)
        {
            return (await GetFromStorage()).SingleOrDefault(x => x.Id == id);
        }

        public async Task Save(Board board)
        {
            var items = (await GetFromStorage()).ToList();
            var existing = items.FirstOrDefault(x => x.Id == board.Id);
            if (existing != null)
            {
                var index = items.IndexOf(existing);
                items[index] = board;
            }
            else
            {
                items.Add(board);
            }
            await SaveToStorage(items);
        }

        public async Task Delete(Board board)
        {
            var items = (await GetFromStorage()).ToList();
            var existing = items.FirstOrDefault(x => x.Id == board.Id);
            if (existing != null)
            {
                var index = items.IndexOf(existing);
                items.RemoveAt(index);
            }
            await SaveToStorage(items);
        }
    }
}