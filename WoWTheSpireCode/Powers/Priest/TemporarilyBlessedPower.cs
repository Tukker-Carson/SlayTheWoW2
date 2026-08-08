using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class TemporarilyBlessedPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (!participants.Contains(Owner)) return;
        if (Owner.HasPower<BlessedPower>()) Owner.GetPower<BlessedPower>()!.Amount -= Amount;
        else await PowerCmd.Apply<BlessedPower>(new ThrowingPlayerChoiceContext(),Owner, -Amount, Owner, null);
        if (Owner.GetPower<BlessedPower>()!.Amount == 0) await PowerCmd.Remove<BlessedPower>(Owner);
        await PowerCmd.Remove(this);
    }
}