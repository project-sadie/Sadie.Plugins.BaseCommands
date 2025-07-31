using Sadie.API;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.Server;

public class EnableCommand : AbstractRoomChatCommand
{
    public override string Trigger => "enable";
    public override string Description => "Set your current enable ID";
    public override List<string> Parameters { get; } = ["id"];

    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        if (!reader.GetInt(out var enableId))
        {
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