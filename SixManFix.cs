using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Utils;

namespace SixManFix
{
    public class SixManFix : BasePlugin
    {
        public override string ModuleName => "SixManFix";
        public override string ModuleVersion => "1.0.1";
        public override string ModuleAuthor => "unfortunate";

        #region Events
        [GameEventHandler(HookMode.Pre)]
        public HookResult OnPlayerConnect(EventPlayerConnectFull @event, GameEventInfo info)
        {
            var player = @event.Userid;
            if (!IsPlayerValid(player)) return HookResult.Continue;
            if (player.TeamNum == 0) return HookResult.Continue;

            player.ChangeTeam(CsTeam.None);
            player.ExecuteClientCommand("teammenu");

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
}
