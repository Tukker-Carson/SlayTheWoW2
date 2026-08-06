using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class PsyfiendPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (!participants.Contains(Owner)) return Task.CompletedTask;
        var target = CombatState.Enemies.Where(e => !e.IsDead && e.HasPower<FearPower>())
            .MaxBy(e => e.GetPower<FearPower>()!.GetDynamicVar("Potency"));
        if (target is not null) target.GetPower<FearPower>()!.Amount *= Amount+1;
        return Task.CompletedTask;
    }
}