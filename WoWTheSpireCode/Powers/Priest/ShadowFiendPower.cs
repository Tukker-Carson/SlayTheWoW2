using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ShadowFiendPower : BaseDoT {
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        ArgumentNullException.ThrowIfNull(Applier);
        ArgumentNullException.ThrowIfNull(Applier.Player);
        var enumerable = participants.ToList();
        await base.AfterSideTurnEnd(choiceContext, side, enumerable);
        if (enumerable.Contains(Owner))
            await CardPileCmd.Draw(choiceContext, Applier.Player);
    }
}