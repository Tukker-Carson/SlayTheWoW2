using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class MindSear() : PriestCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4, ValueProp.Move), new RepeatVar(4)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowOrbPower>()];

    protected override bool IsPlayable => CombatState is { Enemies.Count: > 1 };

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(CombatState);
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        foreach (var enemy in CombatState.Enemies) if (enemy != play.Target) 
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(enemy).WithHitCount(DynamicVars.Repeat.IntValue).Execute(choiceContext);
        
        if (Owner.Creature.HasPower<ShadowformPower>()) {
            await PowerCmd.Apply<ShadowOrbPower>(new ThrowingPlayerChoiceContext(),
                Owner.Creature,
                DynamicVars[nameof(ShadowOrbPower)].BaseValue,
                Owner.Creature,
                this);
        }
    }
    
    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(1);
}