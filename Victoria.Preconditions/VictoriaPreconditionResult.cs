using Discord.Commands;

namespace Victoria.Preconditions
{
    public sealed class VictoriaPreconditionResult : PreconditionResult
    {
        public VictoriaPreconditionResult(CommandError? error, string reason)
            : base(error, reason)
        { }

        public static new VictoriaPreconditionResult FromError(string reason) =>
            new VictoriaPreconditionResult(CommandError.Unsuccessful, reason);

        public static new VictoriaPreconditionResult FromSuccess(string reason = null) =>
            new VictoriaPreconditionResult(null, reason);
    }
}