# About the author

I'm Stévan Corre, a 18yo french developper searching for things to code ^^.  If you can feel free to donate [here](https://www.paypal.com/paypalme/aiixu) 😁

# Usage

### Example command

```cs
[Command("pause")]
[RequireLavaPlayerState(LavaPlayerState.UserAndBotInSameChannel | LavaPlayerState.Playing)]
public async Task PauseCommand()
{  
    // this code will only runs if the bot is in the same channel as the user and if the lava player is playing some music

    LavaPlayer player = lavaNode.GetPlayer(Context.Guild);
    await player.PauseAsync();
}
```

### Work with precondition returns
In order to get the return message from the precondition, you have to list for the `CommandExecuted` event.  
Add this in your command handler. Read more [here](https://discord.foxbot.me/docs/guides/commands/post-execution.html).
```cs
public async Task OnCommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
{
    // result contains the message
    switch(result)
    {
        case VictoriaPreconditionResult victoriaResult:
            // do something extra with it
            break;

        default:
            if (!string.IsNullOrEmpty(result.ErrorReason))
            {
                await context.Channel.SendMessageAsync(result.ErrorReason);
            }
            break;
    }
}
```

### Available flags
| Flags | Description | Default message | Value |
| - | - | - | - |
| UserInVoiceChannel | User must to be in a voice channel | The user has to be in a voice channel in order to execute this command | 1 |
| BotInVoiceChannel | Bot must to be in a voice channel | The bot has to be in a voice channel in order to execute this command | 2 |
| BotNotInVoiceChannel | Bot must not to be in a voice channel | The bot is already in a voice channel | 3 |
| UserAndBotInSameChannel | User and bot has to be in the same channel | The bot has to be in the same voice channel as the user in order to execute this command | 4 |
| Playing | The bot must be playing | The bot must be playing in order to execute this command | 5 |
| NotPlaying | The bot must be not playing | The bot must be not playing in order to execute this command | 6 |
| QueueEmpty | The player's queue must to be empty | The queue must to be empty in order to execute this command | 7 |
| QueueNotEmpty | The player's queue must be not empty | The queue mustn't to be empty in order to execute this command | 8 |
