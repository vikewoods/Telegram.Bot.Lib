

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Lib.Model;

namespace Telegram.Bot.Lib
{
    public interface ITelegramBot
    {
        Task<User> GetMe();

        /// <summary>
        /// Method used to build uri for request.
        /// </summary>
        /// <param name="method">Unique identifier of the target user</param>
        /// <returns>Returns a uri.</returns>
        string BuildRequest(string method);
    }
}
