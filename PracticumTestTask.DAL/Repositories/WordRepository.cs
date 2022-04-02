using Microsoft.EntityFrameworkCore;
using PracticumTestTask.Core.Interfaces;
using PracticumTestTask.Model.Model;
using System.Linq;
using System.Threading.Tasks;

namespace PracticumTestTask.DAL.Repositories
{
    public class WordRepository : GenericRepository<Word>, IWordRepository
    {
        private readonly WordDbContext _context;
        public WordRepository(WordDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task DeleteWordsAsync(RequestUrl requestUrl)
        {
            var words = await _context.Words.Where(x => x.RequestUrlId == requestUrl.Id).ToListAsync();

            if (words != null)
                _context.Words.RemoveRange(words);
        }
    }
}
