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
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using Telegram.Bot.Lib;
using Telegram.Bot.Lib.Model;

namespace Telegram.Bot.Lib.Tests
{
    [TestFixture]
    public class TelegramBotTest
    {
        
        [Test]
        public async Task GetMeTest()
        {
            var client = new TelegramBot("161652985:AAHg3nbjt1AduvFzRisQWMsk8ooWl6flx6I");
            var getMe = await client.GetMe();

            Assert.IsNotNull(getMe.Id, getMe.FirstName, getMe.LastName, getMe.Username);
            Console.WriteLine($"[ID:{getMe.Id}] {getMe.FirstName} {getMe.LastName} with username {getMe.Username}");
        }

        [Test]
        public async Task GetUpdatesTest()
        {
            var client = new TelegramBot("161652985:AAHg3nbjt1AduvFzRisQWMsk8ooWl6flx6I");
            var getUpdates = await client.GetUpdates();

            var ar = getUpdates.ToArray();

            foreach (var a in ar)
            {
                Console.WriteLine("Update id is: " + a.UpdateId);
            }

            Assert.IsNotNull(getUpdates);
        }

        [Test]
        public async Task SendMessageTest()
        {
            var client = new TelegramBot("161652985:AAHg3nbjt1AduvFzRisQWMsk8ooWl6flx6I");
            var getUpdates = await client.GetUpdates();

            if (getUpdates.Count > 0)
            {
                string text = "SendMessageTest";
                var msg = await client.SendMessage(getUpdates[0].Message.Chat.Id, text, false, null, null);
                Assert.AreEqual(text, msg.Text);
            }
            Assert.IsNotNull(getUpdates);
        }

        [Test]
        public async Task SendMessageTest_ReplyToLastMessage()
        {
            var client = new TelegramBot("161652985:AAHg3nbjt1AduvFzRisQWMsk8ooWl6flx6I");
            var getUpdates = await client.GetUpdates();

            if (getUpdates.Count > 0)
            {
                string text = "SendMessageTest_ReplyToLastMessage";
                var msg = await client.SendMessage(getUpdates[0].Message.Chat.Id, text, false, getUpdates[0].Message.MessageId, null);
                Assert.AreEqual(text, msg.Text);
            }
            Assert.IsNotNull(getUpdates);
        }

        [Test]
        public async Task ForwardMessage()
        {
            var client = new TelegramBot("161652985:AAHg3nbjt1AduvFzRisQWMsk8ooWl6flx6I");
            var getUpdates = await client.GetUpdates();

            if (getUpdates.Count > 0)
            {
                var msg = await client.ForwardMessage(getUpdates[0].Message.Chat.Id, getUpdates[0].Message.Chat.Id, getUpdates[0].Message.MessageId);
                Assert.AreEqual(getUpdates[0].Message.Text, msg.Text);
            }
            Assert.IsNotNull(getUpdates);
        }
    }
}