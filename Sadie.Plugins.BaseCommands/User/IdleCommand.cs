using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
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
            UserId = user.Player.Id,
            IsIdle = true
        });
    }
}