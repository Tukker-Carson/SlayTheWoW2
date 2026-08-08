using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class AngelicFeather() : PriestCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<BlessedPower>(4),
        new PowerVar<TemporarilyBlessedPower>(4)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlessedPower>()];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<BlessedPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(BlessedPower)].BaseValue,
            Owner.Creature,
            this);
        await PowerCmd.Apply<TemporarilyBlessedPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(TemporarilyBlessedPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() {
        DynamicVars.Power<BlessedPower>().UpgradeValueBy(2);
        DynamicVars.Power<TemporarilyBlessedPower>().UpgradeValueBy(2);
    }
}