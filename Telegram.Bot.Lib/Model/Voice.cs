using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Telegram.Bot.Lib.Model
{
    [DataContract(Name = "Voice")]
    public class Voice : Audio{}
}
