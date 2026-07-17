using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class PrayerOfMendingPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
        if (target != Owner || result.UnblockedDamage < 1) return;
        await WoWCmd.Heal(target, Owner, Amount, ValueProp.Unpowered, null);
        var lowHealth = 0;
        var newOwner = Owner;
        foreach (var creature in CombatState.GetTeammatesOf(Owner)
                     .Where(c => c is { IsAlive: true, IsPlayer: true })) {
            if (creature.CurrentHp <= lowHealth) continue;
            lowHealth = creature.CurrentHp;
            newOwner = creature;
        }
        await PowerCmd.Remove(this);
        await PowerCmd.Apply<PrayerOfMendingPower>(choiceContext, newOwner, Amount-1, Applier, null);
    }
}