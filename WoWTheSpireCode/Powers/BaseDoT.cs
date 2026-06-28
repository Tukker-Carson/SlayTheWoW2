using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.Powers;

public abstract class BaseDoT : WoWTheSpirePower {
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.InstancedPerApplier;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(0, ValueProp.Unpowered), new StringVar("Applier")];
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (side == Owner.Side) return;
        await CreatureCmd.Damage(choiceContext, Owner, DynamicVars.Damage.BaseValue, ValueProp.Unpowered, Owner, null);
        await PowerCmd.Decrement(this);
    }
    
    public override Task AfterApplied(Creature? applier, CardModel? cardSource) {
        ((StringVar)DynamicVars["Applier"]).StringValue = Applier!.Player!.NetId == 1 ? "You gain" : PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, Applier!.Player!.NetId)+"gains";
        return Task.CompletedTask;
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (power != this || cardSource == null)
            return base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);
        
        DynamicVars.Damage.BaseValue = Math.Max(DynamicVars.Damage.BaseValue, cardSource.DynamicVars["Potency"].BaseValue);
        PowerCmd.ModifyAmount(choiceContext, this, -Math.Min(amount, Amount-amount), null, null);

        return base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);
    }
}