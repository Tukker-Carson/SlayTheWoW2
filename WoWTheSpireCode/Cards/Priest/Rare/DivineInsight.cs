using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class DivineInsight() : PriestCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) {
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<FearPower>()];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DivineInsightPower>(4),
        new ("Potency", 16)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await PowerCmd.Apply<DivineInsightPower>(
            new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(DivineInsightPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade() { 
        DynamicVars[nameof(DivineInsightPower)].UpgradeValueBy(1);
        DynamicVars["Potency"].UpgradeValueBy(4);
    }
}