using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Lib.Model
{
    /// <summary>
    /// User model https://core.telegram.org/bots/api#user
    /// </summary>
    [DataContract]
    public class User
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name", IsRequired = false, EmitDefaultValue = true)]
        public string LastName { get; set; }

        [DataMember(Name = "username", IsRequired = false, EmitDefaultValue = true)]
        public string Username { get; set; }
    }
}
