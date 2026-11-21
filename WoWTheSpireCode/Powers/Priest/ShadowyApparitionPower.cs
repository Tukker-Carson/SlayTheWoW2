using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ShadowyApparitionPower : WoWTheSpirePower, IWoWDotTickListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public decimal ModifyDotTickAdditive(Creature target, Creature source, decimal amount) =>
        source == Owner && Owner.HasPower<StrengthPower>() ? Owner.GetPowerAmount<StrengthPower>() : 0;

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (power is not StrengthPower) return Task.CompletedTask;
        foreach (var enemy in CombatState.Enemies)
            foreach (var pow in enemy.Powers)
                if(pow is BaseDoT)
                    pow.DynamicVars.Damage.BaseValue =
                        WoWCmd.ResolveDotTickAmount(Owner, Applier!, pow.DynamicVars["Potency"].BaseValue);
        return Task.CompletedTask;
    }
}