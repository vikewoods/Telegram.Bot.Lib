// Copyright(C) 2015 by Vik Ewoods <vik.ewoods@gmail.com>
// 
// This file is part of Telegram.Bot.Lib.
// 
// Project:   Telegram.Bot.Lib
// File:      TelegramBot.cs
// User:      vikew
// Date:      20:05 19/11/2015
// 
// Telegram.Bot.Lib is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Telegram.Bot.Lib is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Telegram.Bot.Lib. If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using RestSharp.Deserializers;
using Telegram.Bot.Lib.Model;

namespace Telegram.Bot.Lib
{
    public class TelegramBot : ITelegramBot
    {
        private readonly string _token;
        private const string BaseApiUrl = @"https://api.telegram.org/";
        private readonly RestClient _restClient;

        #region Telegram Bot Initialization
        /// <exception cref="ArgumentNullException"><paramref name="token" /> is <see langword="null" />.</exception>
        public TelegramBot(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token), "Token can't be blank!");
            }

            _token = token;
            _restClient = new RestClient(BaseApiUrl);
        }
        public string BuildRequest(string method)
        {
            return $"bot{_token}/{method}";
        }
        #endregion

        #region Telegram Bot POST and GET requests async with RestSharp
        private async Task<T> ExecutePostRequestAsync<T>(IRestRequest request)
        {
            var result = await _restClient.ExecutePostTaskAsync<BotResponse<T>>(request).ConfigureAwait(false);

            if (result.ResponseStatus != ResponseStatus.Completed)
                throw new Exception("Response throw exception: " + result.ResponseStatus, result.ErrorException);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException("Http request throw exception: " + result.StatusCode);

            if (!result.Data.Ok)
                throw new Exception("Telegram API throw an error!");

            return result.Data.Result;
        }
        private async Task<T> ExecuteGetRequestAsync<T>(IRestRequest request)
        {
            var result = await _restClient.ExecuteGetTaskAsync<BotResponse<T>>(request).ConfigureAwait(false);

            if (result.ResponseStatus != ResponseStatus.Completed)
                throw new Exception("Response throw exception: " + result.ResponseStatus, result.ErrorException);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new HttpRequestException("Http request throw exception: " + result.StatusCode);

            if (!result.Data.Ok)
                throw new Exception("Telegram API throw an error!");

            return result.Data.Result;
        }
        protected static void ThrowOutOfRangeExceptionIfNotInRange(string param, int value, int @from, int to)
        {
            if ((value < @from) || (value > to))
                throw new ArgumentOutOfRangeException(param,
                    $"Argument must be in {@from} – {(to == int.MaxValue ? "∞" : to.ToString())} range");
        }
        #endregion

        
        public Task<User> GetMe()
        {
            return ExecuteGetRequestAsync<User>(new RestRequest(BuildRequest("getMe"), Method.GET));
        }

        public async Task<List<Update>> GetUpdates(int offset = 0, int limit = 100, int timeout = 0)
        {
            var request = new RestRequest(BuildRequest("getUpdates"), Method.GET);

            if (offset != 0)
                request.AddQueryIntParameter("offset", offset);

            if (limit != 0)
                request.AddQueryIntParameter("limit", limit);

            if (timeout != 100)
                request.AddQueryIntParameter("timeout", timeout);

            var uri = _restClient.BuildUri(request);
            var sb = new StringBuilder();

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);

                var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
                using (var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead))
                {
                    using (var body = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(body))
                        while (!reader.EndOfStream)
                            sb.Append(reader.ReadLine());
                }
            }

            var deserial = new JsonDeserializer();
            var apiResponse = deserial.Deserialize<BotResponse<List<Update>>>(new RestResponse { Content = sb.ToString() });

            return apiResponse.Result;
        }

        public Task<Message> SendMessage(int chatId, string text, bool? disableWebPagePreview, int? replyToMessageId, ReplyMarkup replyMarkup)
        {
            var request = new RestRequest(BuildRequest("sendMessage"), Method.POST);

            request.AddQueryIntParameter("chat_id", chatId);

            if (disableWebPagePreview.HasValue)
                request.AddQueryParameter("disable_web_page_preview", disableWebPagePreview.Value.ToString());

            if (replyToMessageId.HasValue)
                request.AddQueryIntParameter("reply_to_message_id", replyToMessageId.Value);

            request.AddParameter("text", text);

            if (replyMarkup != null)
                request.AddParameter("reply_markup", JsonConvert.SerializeObject(replyMarkup));

            return ExecutePostRequestAsync<Message>(request);
        }
        public Task<Message> SendMessage(string chatId, string text, bool? disableWebPagePreview, int? replyToMessageId, ReplyMarkup replyMarkup)
        {
            var request = new RestRequest(BuildRequest("sendMessage"), Method.POST);

            request.AddQueryParameter("chat_id", chatId);

            if (disableWebPagePreview.HasValue)
                request.AddQueryParameter("disable_web_page_preview", disableWebPagePreview.Value.ToString());

            if (replyToMessageId.HasValue)
                request.AddQueryIntParameter("reply_to_message_id", replyToMessageId.Value);

            request.AddParameter("text", text);

            if (replyMarkup != null)
                request.AddParameter("reply_markup", JsonConvert.SerializeObject(replyMarkup));

            return ExecutePostRequestAsync<Message>(request);
        }

        public Task<Message> ForwardMessage(int chatId, int fromChatId, int messageId)
        {
            var request = new RestRequest(BuildRequest("forwardMessage"), Method.GET);

            request.AddQueryIntParameter("chat_id", chatId);
            request.AddQueryIntParameter("from_chat_id", fromChatId);
            request.AddQueryIntParameter("message_id", messageId);

            return ExecuteGetRequestAsync<Message>(request);
        }
        public Task<Message> ForwardMessage(string chatId, int fromChatId, int messageId)
        {
            var request = new RestRequest(BuildRequest("forwardMessage"), Method.GET);

            request.AddQueryParameter("chat_id", chatId);
            request.AddQueryIntParameter("from_chat_id", fromChatId);
            request.AddQueryIntParameter("message_id", messageId);

            return ExecuteGetRequestAsync<Message>(request);
        }
        public Task<Message> ForwardMessage(string chatId, string fromChatId, int messageId)
        {
            var request = new RestRequest(BuildRequest("forwardMessage"), Method.GET);

            request.AddQueryParameter("chat_id", chatId);
            request.AddQueryParameter("from_chat_id", fromChatId);
            request.AddQueryIntParameter("message_id", messageId);

            return ExecuteGetRequestAsync<Message>(request);
        }


    }
}