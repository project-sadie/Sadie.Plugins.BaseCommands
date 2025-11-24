using Sadie.API;
using Sadie.API.Interfaces.Game.Locale;
using Sadie.API.Interfaces.Game.Rooms.Chat.Commands;
using Sadie.API.Interfaces.Game.Rooms.Users;
using Sadie.Core.Enums.Miscellaneous;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.User;

public class MoonWalkCommand(ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "moonwalk";
    public override string Description => localeService["cmd.moonWalk.describe"];
    
    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        user.MoonWalking = !user.MoonWalking;
        
        var effectId = user.MoonWalking ? (int) EffectIds.Moonwalk : 0;
        
        await user.Room.UserRepository.BroadcastDataAsync(new RoomUserEffectWriter
        {
            UserId = user.Player.Player.Id,
            EffectId = effectId,
            DelayMs = 0
        });
    }
}