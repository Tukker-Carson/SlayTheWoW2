using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class Penance() : PriestCard(1, CardType.Attack, CardRarity.Common, CustomTargetType.Anyone) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(3, ValueProp.Move), new RepeatVar(3), new HealVar(2)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowformPower>()];
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        if (play.Target.IsEnemy)
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this)
                .WithHitCount(DynamicVars.Repeat.IntValue).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        else { 
            VfxCmd.PlayOnCreature(play.Target,"vfx/vfx_scream");
            for (var i = 0; i < 3; i++) {
                await CreatureCmd.Heal(play.Target, DynamicVars.Heal.BaseValue);
            }
        }
    }

    protected override void OnUpgrade() => DynamicVars.Repeat.UpgradeValueBy(1);
}