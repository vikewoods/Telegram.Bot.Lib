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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using System.Net.Http;
using Telegram.Bot.Lib.Model;

namespace Telegram.Bot.Lib
{
    public class TelegramBot : ITelegramBot
    {
        private readonly string _token;
        private const string BaseApiUrl = @"https://api.telegram.org/";
        private const string ApiUrl = "https://api.telegram.org/bot{0}/{1}";
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
    }
}