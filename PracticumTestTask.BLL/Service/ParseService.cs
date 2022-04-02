using HtmlAgilityPack;
using PracticumTestTask.Core.Interfaces;
using PracticumTestTask.Model.Dto;
using PracticumTestTask.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PracticumTestTask.BLL.Service
{
    public class ParseService : IParseService
    {
        private readonly IWordRepository _IwordRepository;
        private readonly IRequestUrlRepository _IrequestRepository;
        public ParseService(IWordRepository IwordRepository, IRequestUrlRepository IrequestRepository)
        {
            _IwordRepository = IwordRepository;
            _IrequestRepository = IrequestRepository;
        }
        /// <summary>
        /// скачиваем html страницу в переменную
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string Parse(string url)
        {

            WebClient wc = new WebClient();
            byte[] bytes = wc.DownloadData($"{url}");
            string webData = System.Text.Encoding.UTF8.GetString(bytes);

            var pageDoc = new HtmlDocument();
            pageDoc.LoadHtml(webData);
            var pageText = pageDoc.DocumentNode.InnerText;

            return pageText;

        }

        /// <summary>
        /// получение количества слов и их повторений, сохранение статистики в базу данных
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task SaveWordsAsync(string url)
        {
            var words = HelperParce(url);

            List<Word> listWords = new List<Word>();

            RequestUrl requestUrl = new RequestUrl();
            //проверяем ,делался ли запрос ранее
            var request = await _IrequestRepository.GetRequestByUrlAsync(url);
            if (request != null)
            {
                requestUrl = request;
                requestUrl.Time = DateTime.Now;
                await _IrequestRepository.UpdateAsync(requestUrl);

                await _IwordRepository.DeleteWordsAsync(request);
            }
            else
            {
                requestUrl.Url = url;
                requestUrl.Time = DateTime.Now;
                await _IrequestRepository.CreateAsync(requestUrl);
            }

            HelperConvert(listWords, words, requestUrl);
            await _IwordRepository.CreateAsync(listWords);

        }
        /// <summary>
        /// хелпер сохранения слов
        /// </summary>
        /// <param name="listWords"></param>
        /// <param name="words"></param>
        /// <param name="requestUrl"></param>
        private void HelperConvert(List<Word> listWords, List<Word> words, RequestUrl requestUrl)
        {
            foreach (var word in words)
            {
                if (word != null && word.Value != string.Empty)
                {
                    word.RequestUrl = requestUrl;
                    listWords.Add(word);
                }
            }
        }

        /// <summary>
        /// хелпер парсинга слов
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private List<Word> HelperParce(string url)
        {
            string result = Parse(url);
            string[] split = result.Trim().Split(new Char[] { ' ', ',', '.', '!', '?', '"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t' });

            var words = split.GroupBy(x => x)
                              .Where(x => x.Count() > 0)
                              .Select(x => new Word { Value = x.Key, Count = x.Count() }).ToList();
            return words;
        }

        /// <summary>
        /// получение всех запросов url
        /// </summary>
        /// <returns></returns>
        public async Task<List<RequestDto>> GetListRequestsAsync()
        {
            var allRequests = await _IrequestRepository.GetAllAsync();

            List<RequestDto> listDto = new List<RequestDto>();

            foreach (var s in allRequests)
            {
                RequestDto dto = ConvertDtoHelper(s);
                if (dto != null)
                    listDto.Add(dto);
            }
            return listDto;
        }

        /// <summary>
        /// получение информации о конкретном запросе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RequestDto> GetCurrentRequestAsync(Guid id)
        {
            var request = await _IrequestRepository.ExistsAsync(id);

            if (request)
            {
                var currentRequest = await _IrequestRepository.GetCurrentRequestAsync(id);
                RequestDto dto = ConvertDtoHelper(currentRequest);

                return dto;
            }
            else
                return null;
        }


        /// <summary>
        /// преобразование сущности в дто
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private RequestDto ConvertDtoHelper(RequestUrl request)
        {
            RequestDto dto = new RequestDto();

            if (request != null)
            {
                dto.Id = request.Id;
                dto.Url = request.Url;
                dto.Words = request.Words;
                dto.Time = request.Time.ToString("F");

                return dto;
            }

            return null;
        }

        /// <summary>
        /// удаление записи истории запросов
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteRequestAsync(Guid id)
        {
            var request = await _IrequestRepository.ExistsAsync(id);

            if (request)
            {
                var currentRequest = await _IrequestRepository.GetCurrentRequestAsync(id);
                await _IwordRepository.DeleteWordsAsync(currentRequest);
                await _IrequestRepository.DeleteAsync(currentRequest);
            }
        }
    }
}

