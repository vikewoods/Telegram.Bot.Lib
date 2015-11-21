# Welcome to Telegram Bot Library for .NET

The Bot API is an HTTP-based interface created for developers keen on building bots for Telegram. To learn how to create and set up a bot, please read here.

## GetMe();
A simple method for testing your bot's auth token.

`Requires` no parameters.

`Returns` basic information about the bot in form of a User object.

`Example` usage:
```csharp
async Task GetMeTest()
{
    var client = new TelegramBot("");
    var getMe = await client.GetMe();

    Console.WriteLine($"[ID:{getMe.Id}] {getMe.FirstName} {getMe.LastName} with username {getMe.Username}");
}
``` 


## GetUpdates(int offset = 0, int limit = 100, int timeout = 0);
Use this method to receive incoming updates using long polling.

`Requires` parameters:

1.  `offset` Identifier of the first update to be returned. Must be greater by one than the highest among the identifiers of previously received updates. By default, updates starting with the earliest unconfirmed update are returned. An update is considered confirmed as soon as getUpdates is called with an offset higher than its update_id.
2.  `limit` Limits the number of updates to be retrieved. Values between 1 - 100 are accepted. Defaults to 100
3.  `timeout` Timeout in seconds for long polling. Defaults to 0, i.e. usual short polling

`Returns` basic information about the bot in form of a User object.
`Example` usage:
```csharp
async Task GetUpdatesTest()
{
    var client = new TelegramBot("");
    var getUpdates = await client.GetUpdates();

    var ar = getUpdates.ToArray();

    foreach (var a in ar)
    {
        Console.WriteLine("Update id is: " + a.UpdateId);
    }
}
``` 


