// Copyright(C) 2015 by Vik Ewoods <vik.ewoods@gmail.com>
// 
// This file is part of Telegram.Bot.Lib.
// 
// Project:  Telegram.Bot.Lib
// File:        Media.cs
// User:      vikew
// Date:      22:59 19/11/2015
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


using System.Runtime.Serialization;


namespace Telegram.Bot.Lib.Model
{
    public abstract class Media
    {
        [DataMember(Name = "file_id")]
        public string FileId { get; set; }

        [DataMember(Name = "file_size", IsRequired = false, EmitDefaultValue = true)]
        public int FileSize { get; set; }
    }
}