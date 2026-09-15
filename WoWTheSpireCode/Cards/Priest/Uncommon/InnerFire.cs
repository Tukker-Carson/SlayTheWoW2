using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class InnerFire() : PriestCard(0, CardType.Power, CardRarity.Uncommon, TargetType.AllEnemies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AngelicBulwarkPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        if (!Owner.HasPower<RenewPower>()) return;
        
    }
}