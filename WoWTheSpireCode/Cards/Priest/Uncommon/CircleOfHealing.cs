using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class CircleOfHealing() : PriestCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<RenewPower>()];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<CircleOfHealingPower>(1),
        new PowerVar<RenewPower>(2), 
        new IntVar("Potency", 1)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<CircleOfHealingPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(CircleOfHealingPower)].BaseValue,
            Owner.Creature,
            this);
        if (IsUpgraded) await PowerCmd.Apply<RenewPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(RenewPower)].BaseValue,
            Owner.Creature,
            this);
    }
}