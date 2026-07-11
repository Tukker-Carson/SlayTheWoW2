using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class MeditationPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        ArgumentNullException.ThrowIfNull(Owner.Player);
        if (cardPlay.Card.Owner.Creature != Owner) return;
        Amount -= 1;
        if (Amount > 0) return;
        await PlayerCmd.GainEnergy(1, Owner.Player);
        Amount = 4;
    }
    
    public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (!participants.Contains(Owner)) return Task.CompletedTask;
        Amount = 4;
        return Task.CompletedTask;
    }
}