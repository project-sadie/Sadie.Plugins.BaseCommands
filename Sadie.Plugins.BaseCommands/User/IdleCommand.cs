using Sadie.API;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.User;

public class IdleCommand : AbstractRoomChatCommand
{
    public override string Trigger => "idle";
    public override string Description => "Your avatar falls asleep";
    
    public override async Task ExecuteAsync(IRoomUser user, IEnumerable<string> parameters)
    {
        await user.Room.UserRepository.BroadcastDataAsync(new RoomUserIdleWriter
        {
            UserId = user.Player.Id,
            IsIdle = true
        });
    }
}