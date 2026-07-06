using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ArchangelPower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("Potency", 10)];

    
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource) {
        return dealer != Owner || !props.IsPoweredAttack() ? 1 : 1+DynamicVars["Potency"].BaseValue / 100;
    }
    
    public override Task AfterApplied(Creature? applier, CardModel? cardSource) {
        if (cardSource == null) return Task.CompletedTask;
        DynamicVars["Potency"].BaseValue = Math.Max(DynamicVars["Potency"].BaseValue, cardSource.DynamicVars["Potency"].BaseValue);
        return Task.CompletedTask;
    }
    
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (participants.Contains(Owner)) await PowerCmd.Decrement(this);
    }
    
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        if (cardPlay.Card.CanonicalKeywords.Contains(WoWKeywords.Holy) && cardPlay.Card.Owner.Creature == Owner)
            DynamicVars["Potency"].BaseValue += 10;
    }
}