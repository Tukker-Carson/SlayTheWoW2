using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ChakraSerenityPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        if (cardPlay.Card.Owner.Creature != Owner || cardPlay.Card.Type != CardType.Power) return;
        if (Owner.HasPower<RenewPower>()) {
            Owner.GetPower<RenewPower>()!.Amount += Amount;
            Owner.GetPower<RenewPower>()!.DynamicVars.Heal.BaseValue += Amount;
        }
        else {
            await PowerCmd.Apply<RenewPower>(new ThrowingPlayerChoiceContext(),
                Owner,
                Amount,
                Owner,
                null);
            if (Owner.HasPower<RenewPower>()) Owner.GetPower<RenewPower>()!.DynamicVars.Heal.BaseValue = 4;
        }
    }
}