using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class HolyWordSerenityPower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(0), new DamageVar(0, ValueProp.Unpowered)];
    
    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (!participants.Contains(Owner)) return;
        foreach (var creature in CombatState.Creatures)
            if (creature.Side == Owner.Side) await WoWCmd.Heal(creature, Owner, DynamicVars.Heal.BaseValue, ValueProp.Move, null);
            else await CreatureCmd.Damage(new BlockingPlayerChoiceContext(), creature, DynamicVars.Damage, Owner);
        await PowerCmd.Decrement(this);
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (cardSource is null) return Task.CompletedTask;
        DynamicVars.Heal.BaseValue = cardSource.DynamicVars.Heal.BaseValue;
        DynamicVars.Damage.BaseValue = cardSource.DynamicVars.Damage.BaseValue;
        return Task.CompletedTask;
    }
}