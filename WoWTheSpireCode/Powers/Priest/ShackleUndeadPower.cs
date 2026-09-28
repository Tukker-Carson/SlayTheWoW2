using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ShackleUndeadPower : WoWTheSpirePower, IWoWDotTickListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public decimal ModifyDotTickMultiplicative(Creature target, Creature source, decimal amount) =>
        target == Owner ? 2 : 1;
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (participants.Contains(Owner)) await PowerCmd.Decrement(this);
    }
}