/* 
 * Author: Stévan Corre [https://github.com/stevancorre]
 * Repo: [https://github.com/stevancorre/dnet-victoria-require]
 */

namespace Victoria.Preconditions
{
    /// <summary>
    ///  Specifies a user or lava player state.
    /// </summary>
    public enum LavaPlayerState
    {
        /// <summary>
        ///  User must to be in a voice channel
        /// </summary>
        UserInVoiceChannel = 1,

        /// <summary>
        ///  Bot must to be in a voice channel
        /// </summary>
        BotInVoiceChannel,

        /// <summary>
        ///  Bot must not to be in a voice channel
        /// </summary>
        BotNotInVoiceChannel,

        /// <summary>
        ///  User and bot has to be in the same channel
        /// </summary>
        UserAndBotInSameChannel,

        /// <summary>
        ///  The bot must be playing
        /// </summary>
        Playing,

        /// <summary>
        ///  The bot must be not playing
        /// </summary>
        NotPlaying,

        /// <summary>
        ///  The player's queue must to be empty
        /// </summary>
        QueueEmpty,

        /// <summary>
        ///  The player's queue must be not empty
        /// </summary>
        QueueNotEmpty
    }
}