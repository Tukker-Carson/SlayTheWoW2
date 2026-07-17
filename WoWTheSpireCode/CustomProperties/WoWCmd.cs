using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public class WoWCmd {
    public static async Task<Decimal> Heal(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        await WoWHooks.BeforeHeal(target, source, amount, props, cardPlay);
        var modifiedAmount = Math.Max(WoWHooks.ModifyHealAdditive(target, source, amount, props, cardPlay), 0M);
        modifiedAmount = WoWHooks.ModifyHealMultiplicative(target, source, modifiedAmount, props, cardPlay);
        await WoWHooks.AfterHealCalculated(target, source, modifiedAmount, props, cardPlay);
        if (modifiedAmount > 0M) await CreatureCmd.Heal(target, modifiedAmount);
        MainFile.Logger.Info("Heal calculated: " + modifiedAmount);
        await WoWHooks.AfterHeal(target, source, modifiedAmount, props, cardPlay);
        return modifiedAmount;
    }
}