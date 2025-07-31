using Sadie.API;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Enums.Miscellaneous;
using Sadie.Enums.Unsorted;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.User;

public class MoonWalkCommand : AbstractRoomChatCommand
{
    public override string Trigger => "moonwalk";
    public override string Description => "Your avatar falls asleep";
    
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