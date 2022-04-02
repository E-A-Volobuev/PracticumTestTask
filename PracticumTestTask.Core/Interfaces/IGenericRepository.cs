using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticumTestTask.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Получение записи по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task CreateAsync(T item);
        /// <summary>
        /// Создание записей
        /// </summary>
        Task CreateAsync(List<T> items);

        /// <summary>
        /// Запись существует, проверка по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// удаление объекта
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task DeleteAsync(T item);

        /// <summary> Получение всех записей </summary>
        Task<IList<T>> GetAllAsync();

        /// <summary>
        /// обновление записи
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task UpdateAsync(T item);

        /// <summary>
        /// обновление записей
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task UpdateAsync(List<T> items);

    }
}
