using System.Diagnostics;
using System.Text;
using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Players;
using Sadie.API.Game.Rooms;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Players;
using Sadie.Shared;

namespace Sadie.Plugins.BaseCommands.Server;

public class AboutCommand(
    IRoomRepository roomRepository, 
    IPlayerRepository playerRepository,
    ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "about";
    public override string Description => localeService["cmd.about.describe"];

    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        var version = GlobalState.Version;
        var message = new StringBuilder();
        var memoryMb = Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024);

        message.AppendLine($"Sadie {version}");
        message.AppendLine("");
        message.AppendLine($"Players Online: {playerRepository.Count()}");
        message.AppendLine($"Rooms Loaded: {roomRepository.Count}");
        message.AppendLine($"Memory Used: {memoryMb} MB");
        message.AppendLine("");
        message.AppendLine("Credits:");
        message.AppendLine("Habtard - Lead Developer");
        message.AppendLine("Damien - Developer");
        message.AppendLine("Lucas - Creative Director");
        message.AppendLine("");
        
        await user.NetworkObject.WriteToStreamAsync(new PlayerAlertWriter
        {
            Message = message.ToString()
        });
    }
}