using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class HymnOfHope() : PriestCard(3, CardType.Power, CardRarity.Rare, TargetType.AllAllies) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<HymnOfHopePower>(1), new PowerVar<EnergyNextTurnPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<HymnOfHopePower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(HymnOfHopePower)].BaseValue,
            Owner.Creature,
            this);
        await PowerCmd.Apply<EnergyNextTurnPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(EnergyNextTurnPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}