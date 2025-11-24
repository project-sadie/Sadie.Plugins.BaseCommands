using Sadie.API;
using Sadie.API.Interfaces.Game.Locale;
using Sadie.API.Interfaces.Game.Rooms.Chat.Commands;
using Sadie.API.Interfaces.Game.Rooms.Users;
using Sadie.Networking.Writers.Rooms.Users;

namespace Sadie.Plugins.BaseCommands.Server;

public class SummonCommand(ILocaleService locale) : AbstractRoomChatCommand
{
    public override string Trigger => "summon";
    public override string Description => locale["cmd.summon.describe"];

    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        if (!reader.GetWord(out var username))
        {
            await user.SendWhisperAsync(locale["cmd.summon.username.required"]);
            return;
        }

        if (!user.Room.UserRepository.TryGetByUsername(username!, out var targetUser) || targetUser == null)
        {
            await user.SendWhisperAsync(locale["md.summon.username.notFound"]);
            return;
        }

        if (targetUser.Room.Room.Id == user.Room.Room.Id)
        {
            await user.SendWhisperAsync(locale["cmd.summon.alreadyHere"]);
            return;
        }

        await targetUser.NetworkObject.WriteToStreamAsync(new RoomForwardDataWriter
        {
            Room = user.Room.Room,
            RoomForward = false,
            EnterRoom = false,
            IsOwner = user.Room.Room.OwnerId == targetUser.Player.Player.Id,
            UsersNow = user.Room.UserRepository.Count,
        });
    }

    public override List<string> PermissionsRequired { get; set; } = ["command_summon"];
    public override List<string> Parameters { get; } = ["username"];
}