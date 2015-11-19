using System.Runtime.Serialization;

namespace Telegram.Bot.Lib
{
    [DataContract]
    public class BotResponse<T>
    {
        [DataMember(Name = "ok")]
        public bool Ok { get; set; }

        [DataMember(Name = "result")]
        public T Result { get; set; }
    }
}
