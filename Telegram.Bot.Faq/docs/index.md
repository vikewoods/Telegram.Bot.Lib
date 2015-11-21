# Welcome to Telegram Bot Library for .NET

## What is Telegram Bot Api ?
The Bot API is an HTTP-based interface created for developers keen on building bots for Telegram. To learn how to create and set up a bot, please consult our Introduction to Bots and Bot FAQ.

## Function list

```csharp
Task<User> GetMe();
``` 
- A simple method for testing your bot's auth token.
* `Task<List<Update>> GetUpdates(int offset = 0, int limit = 100, int timeout = 0)` - Start the live-reloading docs server.
* `mkdocs build` - Build the documentation site.
* `mkdocs help` - Print this help message.

## Project layout

    mkdocs.yml    # The configuration file.
    docs/
        index.md  # The documentation homepage.
        ...       # Other markdown pages, images and other files.
