using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class TwistOfFate() : PriestCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move), new PowerVar<TwistOfFatePower>(1)];
    protected override bool ShouldGlowGoldInternal => CombatState is not null && CombatState.HittableEnemies.Any(e => e.GetHpPercentRemaining()<=0.15);

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        if (play.Target.GetHpPercentRemaining() <= 0.15) {
            await PowerCmd.Apply<TwistOfFatePower>(new ThrowingPlayerChoiceContext(),
                Owner.Creature,
                DynamicVars[nameof(TwistOfFatePower)].BaseValue,
                Owner.Creature,
                this);
        }
    }
    
    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(6);
}