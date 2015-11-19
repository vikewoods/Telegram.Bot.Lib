// Copyright(C) 2015 by Vik Ewoods <vik.ewoods@gmail.com>
// 
// This file is part of Telegram.Bot.Lib.
// 
// Project:  Telegram.Bot.Lib.Tests
// File:        TelegramBotTest.cs
// User:      vikew
// Date:      21:02 19/11/2015
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
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using Telegram.Bot.Lib;

namespace Telegram.Bot.Lib.Tests
{
    /// <summary>Этот класс содержит параметризованные модульные тесты для TelegramBot</summary>
    [PexClass(typeof (TelegramBot))]
    [PexAllowedExceptionFromTypeUnderTest(typeof (InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof (ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class TelegramBotTest
    {
        /// <summary>Тестовая заглушка для GetMe()</summary>
        [PexMethod]
        public Task GetMeTest([PexAssumeUnderTest] TelegramBot target)
        {
            Task result = target.GetMe();
            return result;
            // TODO: добавление проверочных утверждений в метод TelegramBotTest.GetMeTest(TelegramBot)
        }
    }
}