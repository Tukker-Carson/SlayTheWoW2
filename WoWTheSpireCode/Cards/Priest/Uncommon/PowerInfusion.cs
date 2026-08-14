using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class PowerInfusion() : PriestCard(-1, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override bool HasEnergyCostX => true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<PowerInfusionPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            ResolveEnergyXValue()*2,
            Owner.Creature, 
            this);
    }

    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}