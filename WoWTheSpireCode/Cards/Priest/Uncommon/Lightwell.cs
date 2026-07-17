using BaseLib.Extensions;
using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class Lightwell() : PriestCard(0, CardType.Skill, CardRarity.Uncommon, CustomTargetType.Everyone) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(5, ValueProp.Move),
        new HealVar(5)
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy, CardKeyword.Retain];
    public override bool CanBeGeneratedInCombat => false;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        var target = CombatState?.Creatures.Where(c => c.IsAlive).MinBy(c => c.CurrentHp) ?? Owner.Creature;
        if (CombatState?.Allies.Contains(target) ?? true) await CreatureCmd.Heal(target, DynamicVars.Heal.BaseValue);
        else await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(3);
        DynamicVars.Heal.UpgradeValueBy(3);
    }
}