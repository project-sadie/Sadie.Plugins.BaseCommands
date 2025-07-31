using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Enums.Miscellaneous;
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
            UserId = user.Player.Id,
            EffectId = effectId,
            DelayMs = 0
        });
    }
}