using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Enums.Game.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.User;

public class SitCommand(ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "sit";
    public override string Description => "Makes your avatar sit down";
    
    public override Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        if (user.StatusMap.ContainsKey(RoomUserStatus.Sit))
        {
            return Task.CompletedTask;
        }
        
        user.AddStatus(RoomUserStatus.Sit, "0.5");
        return Task.CompletedTask;
    }
}