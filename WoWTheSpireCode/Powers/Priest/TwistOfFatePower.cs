using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class TwistOfFatePower() : WoWTheSpirePower, IWoWHealListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay) {
        return !props.IsPoweredAttack() || Owner != dealer ? 1 : 1.5m;
    }

    public decimal ModifyHealMultiplicative(Creature target, Creature source, decimal amount, ValueProp props, CardPlay? cardPlay) {
        return source == Owner ? 1.5m : 1;
    }
}