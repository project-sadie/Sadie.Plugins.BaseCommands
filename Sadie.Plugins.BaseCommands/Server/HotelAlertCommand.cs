using Sadie.API;
using Sadie.API.Game.Locale;
using Sadie.API.Game.Players;
using Sadie.API.Game.Rooms.Chat.Commands;
using Sadie.API.Game.Rooms.Users;
using Sadie.Networking.Writers.Players;

namespace Sadie.Plugins.BaseCommands.Server;

public class HotelAlertCommand(IPlayerRepository playerRepository,
    ILocaleService localeService) : AbstractRoomChatCommand
{
    public override string Trigger => "ha";
    public override string Description => localeService["cmd.ha.describe"];
    
    public override async Task ExecuteAsync(IRoomUser user, IRoomChatCommandParameterReader reader)
    {
        if (!reader.GetSentence(out var message) || string.IsNullOrWhiteSpace(message) || message.Length < 5)
        {
            await user.SendWhisperAsync(localeService["cmd.ha.badMessage"]);
            return;
        }
        
        var author = user.Player.Username;
        
        await playerRepository.BroadcastDataAsync(
            new PlayerAlertWriter
            {
                Message = $"{message}\n\n- {author} at {DateTime.Now:HH:mm}"
            });
    }
    
    public override List<string> PermissionsRequired{ get; set; } = ["command_hotel_alert"];
    public override List<string> Parameters { get; } = ["message"];
}