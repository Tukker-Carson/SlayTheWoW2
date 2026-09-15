using MegaCrit.Sts2.Core.Entities.Creatures;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public interface IWoWDotTickListener {
    /// <summary>Runs Before the DoT ticks</summary>
    Task BeforeDotTick(Creature target, Creature source, Decimal amount) => Task.CompletedTask;
    
    /// <summary>Return the amount to add.</summary>
    Decimal ModifyDotTickAdditive(Creature target, Creature source, Decimal amount) => 0M;

    /// <summary>Return the amount to multiply by.</summary>
    Decimal ModifyDotTickMultiplicative(Creature target, Creature source, Decimal amount) => 1M;
    
    /// <summary>Runs before DoT tick but after damage calculation</summary>
    Task AfterDotTickCalculated(Creature target, Creature source, Decimal amount) => Task.CompletedTask;
    
    /// <summary>Runs After Dot tick</summary>
    Task AfterDotTick(Creature target, Creature source, Decimal amount) => Task.CompletedTask;
}