using System;

namespace PracticumTestTask.Model.Model
{
    public class Word
    {
        public Guid Id { get; set; }
        /// <summary>
        /// значение слова
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// количество повторений
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// навигационное свойство для связи с сущностью запроса (один ко многим)
        /// </summary>
        public Guid? RequestUrlId { get; set; }
        public RequestUrl RequestUrl { get; set; }
    }
}
