using Microsoft.EntityFrameworkCore;
using PracticumTestTask.Core.Interfaces;
using PracticumTestTask.Model.Model;
using System;
using System.Threading.Tasks;

namespace PracticumTestTask.DAL.Repositories
{
    public class RequestUrlRepository : GenericRepository<RequestUrl>, IRequestUrlRepository
    {
        private readonly WordDbContext _context;
        public RequestUrlRepository(WordDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// получение запроса со списком статистики слов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RequestUrl> GetCurrentRequestAsync(Guid id)
        {
            var allRequest = await _context.RequestUrls.Include(x => x.Words).FirstOrDefaultAsync(x => x.Id == id);
            return allRequest;
        }

        /// <summary>
        /// получение запроса по url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<RequestUrl> GetRequestByUrlAsync(string url)
        {
            var request = await _context.RequestUrls.FirstOrDefaultAsync(x => x.Url == url);
            return request;
        }

    }
}
