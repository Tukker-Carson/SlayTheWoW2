using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class MindBenderPower : BaseDoT {
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (!participants.Contains(Owner)) return;
        var fear = Owner.Powers.FirstOrDefault(p => p is FearPower && p.Owner == Applier);
        if (fear is not null) fear.Amount += DynamicVars["Potency"].IntValue;
        await base.AfterSideTurnEnd(choiceContext, side, [Owner]);
    }
}