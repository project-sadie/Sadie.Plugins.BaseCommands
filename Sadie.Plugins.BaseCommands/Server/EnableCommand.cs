using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.Server;

public class EnableCommand(ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "enable";
    public override string Description => localeService["cmd.enable.describe"];
    public override List<string> Parameters { get; } = ["id"];

    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        if (!reader.GetInt(out var enableId))
        {
            await user.SendWhisperAsync(localeService["cmd.enable.noId"]);
            return;
        }
        
        user.ActiveEffectId = enableId;
        
        await user.Room.UserRepository.BroadcastDataAsync(new RoomUserEffectWriter
        {
            UserId = user.Player.Id,
            EffectId = enableId,
            DelayMs = 0
        });
    }
}