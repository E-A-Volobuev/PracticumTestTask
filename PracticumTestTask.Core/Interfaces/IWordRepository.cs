using PracticumTestTask.Model.Model;
using System.Threading.Tasks;

namespace PracticumTestTask.Core.Interfaces
{
    public interface IWordRepository : IGenericRepository<Word>
    {
        /// <summary>
        /// удаление (ранее записанных слов в бд) при обновлении данных запроса
        /// </summary>
        /// <param name="requestUrl"></param>
        /// <returns></returns>
        Task DeleteWordsAsync(RequestUrl requestUrl);
    }
}
