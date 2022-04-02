using PracticumTestTask.Model.Model;
using System;
using System.Threading.Tasks;

namespace PracticumTestTask.Core.Interfaces
{
    public interface IRequestUrlRepository : IGenericRepository<RequestUrl>
    {
        /// <summary>
        /// получение конкретного запроса со списком статистики слов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RequestUrl> GetCurrentRequestAsync(Guid id);

        /// <summary>
        /// получение запроса по url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<RequestUrl> GetRequestByUrlAsync(string url);
    }
}
