using Sadie.API;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.Server;

public class EnableCommand : AbstractRoomChatCommand
{
    public override string Trigger => "enable";
    public override string Description => "Set your current enable ID";

    public override async Task ExecuteAsync(IRoomUser user, IEnumerable<string> parameters)
    {
        if (!int.TryParse(parameters.First(), out var enableId))
        {
            return;
        }
        
        await user.Room.UserRepository.BroadcastDataAsync(new RoomUserEffectWriter
        {
            UserId = user.Player.Id,
            EffectId = enableId,
            DelayMs = 0
        });
    }
}