// Copyright(C) 2015 by Vik Ewoods <vik.ewoods@gmail.com>
// 
// This file is part of Telegram.Bot.Lib.
// 
// Project:  Telegram.Bot.Lib
// File:        ITelegramBot.cs
// User:      vikew
// Date:      20:06 19/11/2015
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


using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Lib.Model;


namespace Telegram.Bot.Lib
{
    public interface ITelegramBot
    {
        /// <summary>
        /// A simple method for testing your bot's auth token. Requires no parameters.
        /// </summary>
        /// <returns>Returns basic information about the bot in form of a User object.</returns>
        Task<User> GetMe();

        /// <summary>
        /// Use this method to receive incoming updates using long polling (wiki). 
        /// 
        /// Notes
        /// 1. This method will not work if an outgoing webhook is set up.
        /// 2. In order to avoid getting duplicate updates, recalculate offset after each server response.
        /// </summary>
        /// <param name="offset">Identifier of the first update to be returned. Must be greater by one than the highest among the identifiers of previously received updates. By default, updates starting with the earliest unconfirmed update are returned. An update is considered confirmed as soon as getUpdates is called with an offset higher than its update_id.</param>
        /// <param name="limit">Limits the number of updates to be retrieved. Values between 1—100 are accepted. Defaults to 100</param>
        /// <param name="timeout">Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling</param>
        /// <returns>An Array of Update objects is returned.</returns>
        Task<List<Update>> GetUpdates(int offset = 0, int limit = 100, int timeout = 0);

        /// <summary>
        /// Use this method to send text messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>The sent Message is returned.</returns>
        Task<Message> SendMessage(int chatId, string text, bool? disableWebPagePreview, int? replyToMessageId, ReplyMarkup replyMarkup);

        /// <summary>
        /// Use this method to send text messages. On success, the sent Message is returned.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="text">Text of the message to be sent</param>
        /// <param name="disableWebPagePreview">Disables link previews for links in this message</param>
        /// <param name="replyToMessageId">If the message is a reply, ID of the original message</param>
        /// <param name="replyMarkup">Additional interface options. A JSON-serialized object for a custom reply keyboard, instructions to hide keyboard or to force a reply from the user.</param>
        /// <returns>The sent Message is returned.</returns>
        Task<Message> SendMessage(string chatId, string text, bool? disableWebPagePreview, int? replyToMessageId, ReplyMarkup replyMarkup);

        /// <summary>
        /// Use this method to forward messages of any kind.
        /// </summary>
        /// <param name="chatId">Unique identifier for the target chat or username of the target channel (in the format @channelusername)</param>
        /// <param name="fromChatId">Unique identifier for the chat where the original message was sent (or channel username in the format @channelusername)</param>
        /// <param name="messageId">Unique message identifier</param>
        /// <returns>The sent Message is returned.</returns>
        Task<Message> ForwardMessage(int chatId, int fromChatId, int messageId);
        Task<Message> ForwardMessage(string chatId, int fromChatId, int messageId);
        Task<Message> ForwardMessage(string chatId, string fromChatId, int messageId);

        /// <summary>
        /// Method used to build uri for request.
        /// </summary>
        /// <param name="method">Unique identifier of the target user</param>
        /// <returns>Returns a uri.</returns>
        string BuildRequest(string method);
    }
}