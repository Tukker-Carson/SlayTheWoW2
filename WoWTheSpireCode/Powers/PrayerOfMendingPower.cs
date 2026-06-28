using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.Powers;

public class PrayerOfMendingPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource) {
        if (target != Owner || result.UnblockedDamage < 1)
            return base.AfterDamageReceived(choiceContext, target, result, props, dealer, cardSource);
        CreatureCmd.Heal(target, Amount);
        var lowHealth = 0;
        var newOwner = Owner;
        foreach (var creature in CombatState.GetTeammatesOf(Owner)
                     .Where(c => c is { IsAlive: true, IsPlayer: true })) {
            if (creature.CurrentHp <= lowHealth) continue;
            lowHealth = creature.CurrentHp;
            newOwner = creature;
        }
        var oldAmount = Amount;
        PowerCmd.Remove(this);
        PowerCmd.Apply<PrayerOfMendingPower>(choiceContext, newOwner, Amount-1, Applier, null);
        return base.AfterDamageReceived(choiceContext, target, result, props, dealer, cardSource);
    }
}