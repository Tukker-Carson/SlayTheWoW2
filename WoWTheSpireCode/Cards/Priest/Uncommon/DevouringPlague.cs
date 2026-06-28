using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
using WoWTheSpire.WoWTheSpireCode.Keywords;
using WoWTheSpire.WoWTheSpireCode.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class DevouringPlague() : PriestCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<DevouringPlaguePower>(2), new IntVar("Potency", 8)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<DevouringPlaguePower>(), HoverTipFactory.FromPower<ShadowOrbPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];
    public override bool CanBeGeneratedInCombat => false;
    protected override bool IsPlayable => Owner.HasPower<ShadowOrbPower>();

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        if (!Owner.HasPower<ShadowOrbPower>()) return;
        await PowerCmd.Apply<DevouringPlaguePower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(DevouringPlaguePower)].BaseValue * Owner.Creature.GetPowerAmount<ShadowOrbPower>(),
            Owner.Creature,
            this);
        await PowerCmd.Remove<ShadowOrbPower>(Owner.Creature);
    }
    
    protected override void OnUpgrade() => DynamicVars["Potency"].UpgradeValueBy(2);
}