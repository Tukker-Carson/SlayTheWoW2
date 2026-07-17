using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class BindingHeal() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(12)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowformPower>()];
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await WoWCmd.Heal(Owner.Creature, Owner.Creature, DynamicVars.Heal.BaseValue, ValueProp.Move, play);
        await WoWCmd.Heal(play.Target, Owner.Creature, DynamicVars.Heal.BaseValue,  ValueProp.Move, play);
    }

    protected override void OnUpgrade() => DynamicVars.Heal.UpgradeValueBy(6);
}