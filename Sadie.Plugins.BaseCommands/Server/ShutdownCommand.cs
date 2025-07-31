using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Players;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Players;

namespace Sadie.Plugins.BaseCommands.Server;

public class ShutdownCommand(
    IServer server,
    IPlayerRepository playerRepository,
    ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "shutdown";
    public override string Description => localeService["cmd.shutdown.describe"];

    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        var shutdownMessage = localeService["cmd.shutdown.message"];

        await playerRepository.BroadcastDataAsync(new PlayerAlertWriter
        {
            Message = shutdownMessage
        });

        await Task.Delay(3000);
        await server.DisposeAsync();
    }

    public override List<string> PermissionsRequired { get; set; } = ["command_shutdown"];
}