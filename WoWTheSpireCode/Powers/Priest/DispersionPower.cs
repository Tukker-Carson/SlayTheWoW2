using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class DispersionPower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new("DamageDecrease", 10)];

    
    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay) {
        return target != Owner || !props.IsPoweredAttack() ? 1 : DynamicVars["DamageDecrease"].BaseValue / 100;
    }

    public override bool ShouldPlay(CardModel card, AutoPlayType autoPlayType) {
        return card.Owner.Creature != Owner || card.Type != CardType.Attack;
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (participants.Contains(Owner)) await PowerCmd.Decrement(this);
    }
}