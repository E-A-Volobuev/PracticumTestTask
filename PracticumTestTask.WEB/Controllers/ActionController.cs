using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticumTestTask.Core.Interfaces;
using PracticumTestTask.Model.Dto;
using PracticumTestTask.Model.Model;
using PracticumTestTask.WEB.Models.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PracticumTestTask.WEB.Controllers
{
    public class ActionController : Controller
    {
        private readonly IParseService _service;
        private readonly ILogger _logger;
        public ActionController(IParseService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger("FileLogger");
            _logger = logger;
        }

        /// <summary>
        /// получение статистики уникальных слов
        /// </summary>
        /// <param name = "url" ></ param >
        /// < returns ></ returns >
        [HttpGet]
        public async Task<ActionResult> Process(string url)
        {
            try
            {

                if (url != null && url != string.Empty)
                {
                    await _service.SaveWordsAsync(url);
                    return Ok("успешно");
                }
                else
                    return BadRequest("некорректный url");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("В методе Process возникло исключение:" + ex.Message + " время: " + DateTime.Now.ToString("F"));
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// получение истории всех запросов url
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<RequestDto>>> GetAllRequests()
        {
            try
            {
                var list = await _service.GetListRequestsAsync();
                return list;
            }
            catch (Exception ex)
            {
                _logger.LogInformation("В методе GetAllRequests возникло исключение:" + ex.Message + " время: " + DateTime.Now.ToString("F"));
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// получение информации о запросе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Word>>> GetRequestUrl(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    RequestDto dto = await _service.GetCurrentRequestAsync(id);
                    return dto.Words;
                }
                else
                    return BadRequest("Некорректный запрос");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("В методе GetRequestUrl возникло исключение:" + ex.Message + " время: " + DateTime.Now.ToString("F"));
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// удаление записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteRequest(Guid id)
        {
            try
            {
                if (id != Guid.Empty)
                {
                    await _service.DeleteRequestAsync(id);
                    return Ok();
                }
                else
                    return BadRequest("Некорректный запрос");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("В методе DeleteRequest возникло исключение:" + ex.Message + " время: " + DateTime.Now.ToString("F"));
                return BadRequest(ex.Message);
            }
        }
    }
}
