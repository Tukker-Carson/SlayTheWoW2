using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class RenewPower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(0)];

    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (side != Owner.Side) return base.AfterSideTurnEnd(choiceContext, side, participants);
        CreatureCmd.Heal(Owner, DynamicVars.Heal.BaseValue);
        PowerCmd.Decrement(this);
        return base.AfterSideTurnEnd(choiceContext, side, participants);
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (power != this || cardSource == null)
            return base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);
        
        DynamicVars.Heal.BaseValue = Math.Max(DynamicVars.Heal.BaseValue, cardSource.DynamicVars["Potency"].BaseValue);
        PowerCmd.ModifyAmount(choiceContext, this, -Math.Min(amount, Amount-amount), null, null);

        return base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);
    }
}