using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ChakraSanctuaryPower: WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Potency", 0)];

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        if (cardPlay.Card.Owner.Creature != Owner || !cardPlay.Card.Keywords.Contains(WoWKeywords.Holy)) return;
        if (Amount > 1) await PowerCmd.Decrement(this);
        else {
            await PowerCmd.Apply<BlessedPower>(choiceContext, Owner, DynamicVars["Potency"].BaseValue, Owner, null);
            await PowerCmd.ModifyAmount(choiceContext, this, 2, Owner, null);
        }
    }

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (power != this || cardSource == null)
            return base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);

        DynamicVars["Potency"].BaseValue += cardSource.DynamicVars["Potency"].BaseValue;
        if (Amount > 3) Amount -= 3;
        return base.AfterPowerAmountChanged(choiceContext, power, amount, applier, cardSource);
    }
}