using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public class WoWCmd {
    // -----Heal-----
    public static Decimal ResolveHealAmount(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        amount = Math.Max(WoWHooks.ModifyHealAdditive(target, source, amount, props, cardPlay), 0M);
        // MainFile.Logger.Info("Additive calculated: " + amount);
        return WoWHooks.ModifyHealMultiplicative(target, source, amount, props, cardPlay);
    }
    
    public static async Task<Decimal> Heal(Creature target, Creature source, Decimal amount, ValueProp props, CardPlay? cardPlay) {
        await WoWHooks.BeforeHeal(target, source, amount, props, cardPlay);
        var targetHp = target.CurrentHp;
        var modifiedAmount = props==ValueProp.Unpowered ? amount : ResolveHealAmount(target, source, amount, props, cardPlay);
        await WoWHooks.AfterHealCalculated(target, source, modifiedAmount, props, cardPlay);
        if (modifiedAmount > 0M) await CreatureCmd.Heal(target, modifiedAmount);
        // MainFile.Logger.Info("Heal calculated: " + modifiedAmount);
        await WoWHooks.AfterHeal(target, source, modifiedAmount, modifiedAmount+targetHp-target.CurrentHp, props, cardPlay);
        return modifiedAmount;
    }

    public static Task<Decimal> Heal(Creature target, Creature source, WoWHealVar healVar, CardPlay? cardPlay) {
        return Heal(target, source, healVar.BaseValue, healVar.Props, cardPlay);
    }
    
    
    // -----DoT Tick-----
    public static Decimal ResolveDotTickAmount(Creature target, Creature source, Decimal amount) {
        amount = Math.Max(WoWHooks.ModifyDotTickAdditive(target, source, amount), 0M);
        return WoWHooks.ModifyDotTickMultiplicative(target, source, amount);
    }
    
    public static async Task<IEnumerable<DamageResult>> DotTick(PlayerChoiceContext choiceContext, Creature target, Creature source, Decimal amount) {
        await WoWHooks.BeforeDotTick(target, source, amount);
        MainFile.Logger.Info("Before Calculate");
        var modifiedAmount = ResolveDotTickAmount(target, source, amount);
        MainFile.Logger.Info("Dot Tick calculated: " + modifiedAmount);
        await WoWHooks.AfterDotTickCalculated(target, source, modifiedAmount);
        MainFile.Logger.Info("Before Damage");
        var damage = await CreatureCmd.Damage(choiceContext, target, modifiedAmount, ValueProp.Unpowered, null, null);
        MainFile.Logger.Info("After Damage");
        await WoWHooks.AfterDotTick(target, source, modifiedAmount);
        return damage;
    }
}