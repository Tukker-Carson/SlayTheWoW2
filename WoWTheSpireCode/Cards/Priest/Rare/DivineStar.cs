using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class DivineStar() : PriestCard(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(12, ValueProp.Move),
        new RepeatVar(2),
        new WoWHealVar(12, ValueProp.Move)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await WoWCmd.Heal(Owner.Creature, Owner.Creature, (WoWHealVar)DynamicVars["WoWHeal"], play);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).WithHitCount(DynamicVars.Repeat.IntValue)
            .TargetingAllOpponents(CombatState!).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        await WoWCmd.Heal(Owner.Creature, Owner.Creature, (WoWHealVar)DynamicVars["WoWHeal"], play);
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(8);
        DynamicVars["WoWHeal"].UpgradeValueBy(8);
    }
}