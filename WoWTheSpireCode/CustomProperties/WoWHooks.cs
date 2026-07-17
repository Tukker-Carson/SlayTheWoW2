using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public class WoWHooks {
    public static async Task BeforeHeal(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        foreach (var model in Hook.IterateCombatHookListeners(source.CombatState!).OfType<IWoWHealListener>()) {
            await model.BeforeHeal(target, source, amount, props, cardPlay);
            ((AbstractModel)model).InvokeExecutionFinished();
        }
    }
    
    
    public static Decimal ModifyHealAdditive(Creature target, Creature source, Decimal amount, ValueProp props,
        CardPlay? cardPlay) => Hook.IterateCombatHookListeners(source.CombatState!).OfType<IWoWHealListener>()
            .Aggregate(amount, (current, model) => 
                current + model.ModifyHealAdditive(target, source, amount, props, cardPlay));
    
    
    public static Decimal ModifyHealMultiplicative(Creature target, Creature source, Decimal amount, ValueProp props,
        CardPlay? cardPlay) => Hook.IterateCombatHookListeners(source.CombatState!).OfType<IWoWHealListener>()
            .Aggregate(amount, (current, model) => 
                current * model.ModifyHealMultiplicative(target, source, amount, props, cardPlay));
    
    
    public static async Task AfterHealCalculated(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        foreach (var model in Hook.IterateCombatHookListeners(source.CombatState!).OfType<IWoWHealListener>()) {
            await model.AfterHealCalculated(target, source, amount, props, cardPlay);
            ((AbstractModel)model).InvokeExecutionFinished();
        }
    }
    
    
    public static async Task AfterHeal(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        foreach (var model in Hook.IterateCombatHookListeners(source.CombatState!).OfType<IWoWHealListener>()) {
            await model.AfterHeal(target, source, amount, props, cardPlay);
            ((AbstractModel)model).InvokeExecutionFinished();
        }
    }
}