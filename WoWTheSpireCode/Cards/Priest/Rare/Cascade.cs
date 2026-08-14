using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Cascade() : PriestCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0),
        new ExtraDamageVar(2),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
            (_, target) => target is not null && target.HasPower<FearPower>() ? target.GetPower<FearPower>()!.DynamicVars["Potency"].BaseValue : 0),
        new RepeatVar(3)
    ];
    protected override bool ShouldGlowGoldInternal => CombatState is not null && CombatState.HittableEnemies.Any(e => e.GetHpPercentRemaining()<=0.15);

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.CalculatedDamage).FromCard(this, play).WithHitCount(DynamicVars.Repeat.IntValue)
            .Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }
    
    protected override void OnUpgrade() => DynamicVars.Repeat.UpgradeValueBy(1);
}