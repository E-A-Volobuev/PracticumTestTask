using PracticumTestTask.Model.Model;
using System;
using System.Collections.Generic;

namespace PracticumTestTask.Model.Dto
{
    public class RequestDto
    {
        public Guid Id { get; set; }
        /// <summary>
        /// url сайта
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// время запроса
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// статистика повторений слов
        /// </summary>
        public List<Word> Words { get; set; } = new List<Word>();
    }
}
