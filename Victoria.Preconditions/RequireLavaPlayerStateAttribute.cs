/* 
 * Author: Stévan Corre [https://github.com/stevancorre]
 * Repo: [https://github.com/stevancorre/dnet-victoria-require]
 */

using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

using Victoria;

namespace Victoria.Preconditions
{
    /// <summary>
    ///  Requires the laval player to have a specific <see cref="LavaPlayerState"/> in the context a command is invoked in.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class RequireLavaPlayerStateAttribute : PreconditionAttribute
    {
        /// <summary>
        ///  Get the specified <see cref="LavaPlayerState"/> of the precondition.
        /// </summary>
        public LavaPlayerState PlayerState { get; }

        /// <summary>
        ///  Message to return in a <see cref="PreconditionAttribute"/> if an error occurs.
        /// </summary>
        public override string ErrorMessage { get; set; }

        /// <summary>
        ///  Requires the bot account to have a specific <see cref="LavaPlayerState"/>.
        /// </summary>
        /// <param name="playerState">The <see cref="LavaPlayerState"/> that the lava player or the user must have. Multiple permissions can be specified by ORing the permissions together.</param>
        public RequireLavaPlayerStateAttribute(LavaPlayerState playerState)
            => PlayerState = playerState;

        public async override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            LavaNode lavaNode = services.GetService(typeof(LavaNode)) as LavaNode;
            if(lavaNode is null)
            {
                throw new NullReferenceException("LavaNode service isn't initialized.");
            }

            IVoiceState userVoiceState = context.User as IVoiceState;
            LavaPlayer player = lavaNode.HasPlayer(context.Guild) ? lavaNode.GetPlayer(context.Guild) : null;

            if(PlayerState.HasFlag(LavaPlayerState.UserInVoiceChannel) &&
            userVoiceState?.VoiceChannel == null)
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The user has to be in a voice channel in order to execute this command"));
            }

            if(PlayerState.HasFlag(LavaPlayerState.BotInVoiceChannel) &&
            player?.VoiceChannel == null)
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The bot has to be in a voice channel in order to execute this command"));
            }

            if (PlayerState.HasFlag(LavaPlayerState.BotNotInVoiceChannel) &&
            player?.VoiceChannel != null)
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The bot is already in a voice channel"));
            }


            if (PlayerState.HasFlag(LavaPlayerState.UserAndBotInSameChannel) &&
            (player?.VoiceChannel == null || player?.VoiceChannel != userVoiceState?.VoiceChannel))
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The bot has to be in the same voice channel as the user in order to execute this command"));
            }

            if(PlayerState.HasFlag(LavaPlayerState.Playing) &&
            player?.PlayerState != Victoria.Enums.PlayerState.Playing)
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The bot must be playing in order to execute this command"));
            }

            if (PlayerState.HasFlag(LavaPlayerState.NotPlaying) &&
            (player?.PlayerState != Victoria.Enums.PlayerState.Stopped && player.PlayerState != Victoria.Enums.PlayerState.Paused))
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The bot must be not playing in order to execute this command"));
            }

            if (PlayerState.HasFlag(LavaPlayerState.QueueEmpty) &&
                player?.Queue.Count != 0)
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The queue must to be empty in order to execute this command"));
            }

            if (PlayerState.HasFlag(LavaPlayerState.QueueNotEmpty) &&
            player?.Queue.Count == 0)
            {
                return await Task.FromResult(VictoriaPreconditionResult.FromError(ErrorMessage ?? "The queue mustn't to be empty in order to execute this command"));
            }

            return await Task.FromResult(VictoriaPreconditionResult.FromSuccess());
        }
    }
}