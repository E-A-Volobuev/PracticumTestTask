using PracticumTestTask.Model.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticumTestTask.Core.Interfaces
{
    public interface IParseService
    {

        /// <summary>
        /// получение количества слов и их повторений
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task SaveWordsAsync(string url);

        /// <summary>
        /// получение всех запросов url
        /// </summary>
        /// <returns></returns>
        Task<List<RequestDto>> GetListRequestsAsync();

        /// <summary>
        /// получение информации о конкретном запросе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RequestDto> GetCurrentRequestAsync(Guid id);

        /// <summary>
        /// удаление записи истории запросов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteRequestAsync(Guid id);
    }
}
