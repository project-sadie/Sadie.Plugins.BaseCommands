using Sadie.API;
using Sadie.API.Interfaces.Game.Locale;
using Sadie.API.Interfaces.Game.Rooms.Chat.Commands;
using Sadie.API.Interfaces.Game.Rooms.Users;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.User;

public class IdleCommand(ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "idle";
    public override string Description => localeService["cmd.idle.describe"];
    
    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        await user.Room.UserRepository.BroadcastDataAsync(new RoomUserIdleWriter
        {
            UserId = user.Player.Player.Id,
            IsIdle = true
        });
    }
}