using Sadie.API;
using Sadie.API.Game.Players;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Players;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.Server;

public class SummonCommand : AbstractRoomChatCommand
{
    public override string Trigger => "summon";
    public override string Description => "Summons a user to your room";

    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        if (!reader.GetWord(out var username) ||
            !user.Room.UserRepository.TryGetByUsername(username!, out var targetUser) ||
            targetUser == null ||
            targetUser.Room.Id == user.Room.Id)
        {
            return;
        }

        await targetUser.NetworkObject.WriteToStreamAsync(new RoomForwardDataWriter
        {
            Room = user.Room,
            RoomForward = false,
            EnterRoom = false,
            IsOwner = user.Room.OwnerId == targetUser.Player.Id
        });
    }

    public override List<string> PermissionsRequired { get; set; } = ["command_summon"];
    public override List<string> Parameters { get; } = ["username"];
}