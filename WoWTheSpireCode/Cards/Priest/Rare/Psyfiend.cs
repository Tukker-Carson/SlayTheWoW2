using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Psyfiend() : PriestCard(1, CardType.Power, CardRarity.Rare, TargetType.Self) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PsyfiendPower>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<PsyfiendPower>(
            new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(PsyfiendPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}