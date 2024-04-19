using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;

namespace SixManFix;

public class SixManFix : BasePlugin
{
    public override string ModuleName => "SixManFix";
    public override string ModuleVersion => "1.0.3";
    public override string ModuleAuthor => "unfortunate";

    #region Events
    [GameEventHandler(HookMode.Post)]
    public HookResult OnPlayerDisconnect(EventPlayerDisconnect @event, GameEventInfo info)
    {
        var player = @event.Userid;
        if (!IsPlayerValid(player))
            return HookResult.Continue;
        if (player.TeamNum == 0)
            return HookResult.Continue;

        player.ChangeTeam(CsTeam.None);
        Console.WriteLine($"[SixManFix] {player.PlayerName} switched to CsTeam.None");

        return HookResult.Continue;
    }
    #endregion

    #region Functions
    public static bool IsPlayerValid(CCSPlayerController player)
    {
        return player != null
            && player.IsValid
            && !player.IsBot
            && player.Pawn != null
            && player.Pawn.IsValid
            && player.Connected == PlayerConnectedState.PlayerConnected
            && !player.IsHLTV;
    }
    #endregion
}
