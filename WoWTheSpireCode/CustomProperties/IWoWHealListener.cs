using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public interface IWoWHealListener {
    /// <summary>Runs Before Heal</summary>
    Task BeforeHeal(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) => Task.CompletedTask;
    
    /// <summary>Return the amount to add.</summary>
    Decimal ModifyHealAdditive(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) => 0M;

    /// <summary>Return the amount to multiply by.</summary>
    Decimal ModifyHealMultiplicative(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) => 1M;
    
    /// <summary>Runs Before Heal but Amount is after Healing Buffs</summary>
    Task AfterHealCalculated(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) => Task.CompletedTask;
    
    /// <summary>Runs After Heal</summary>
    Task AfterHeal(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) => Task.CompletedTask;
}