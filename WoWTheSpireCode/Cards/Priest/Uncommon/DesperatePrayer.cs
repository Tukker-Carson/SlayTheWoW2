using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class DesperatePrayer() : PriestCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy, CardKeyword.Exhaust];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0),
        new CalculationExtraVar(20),
        new CalculatedVar("Heal").WithMultiplier((_,target) => target is null?0:target.MaxHp/100m),
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowformPower>()];
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await WoWCmd.Heal(Owner.Creature, Owner.Creature, ((CalculatedVar)DynamicVars["Heal"]).Calculate(Owner.Creature),  ValueProp.Move, play);
    }

    protected override void OnUpgrade() => DynamicVars.CalculationExtra.UpgradeValueBy(5);
}