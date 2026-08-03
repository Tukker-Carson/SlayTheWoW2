using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public sealed class EchoOfLightPower: WoWTheSpirePower, IWoWHealListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public Task AfterHealCalculated(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        if (target == Owner && target.HasPower<RenewPower>()) 
            target.GetPower<RenewPower>()!.DynamicVars.Heal.BaseValue = Math.Max(target.GetPower<RenewPower>()!.DynamicVars.Heal.BaseValue, amount);
        return Task.CompletedTask;
    }
}