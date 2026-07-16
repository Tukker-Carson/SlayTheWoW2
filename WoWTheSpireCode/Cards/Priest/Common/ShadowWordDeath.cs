using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class ShadowWordDeath() : PriestCard(0, CardType.Attack, CardRarity.Common, WoWTargetTypes.Any25pHpEnemies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(16, ValueProp.Move),
        new PowerVar<ShadowOrbPower>(1)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowOrbPower>()];

    protected override bool IsPlayable =>
        CombatState is not null && CombatState.Enemies.Any(enemy => enemy.CurrentHp <= enemy.MaxHp/4);

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        
        if (Owner.Creature.HasPower<ShadowformPower>()) 
            await PowerCmd.Apply<ShadowOrbPower>(new ThrowingPlayerChoiceContext(),
                Owner.Creature,
                DynamicVars[nameof(ShadowOrbPower)].BaseValue,
                Owner.Creature,
                this);
        
        if (play.Target.IsAlive) 
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(CreateClone(), PileType.Discard, Owner));
        
    }
    
    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(10);
}