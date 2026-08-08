using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public sealed class BlessedPower: WoWTheSpirePower, IWoWHealListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => true;

    public decimal ModifyHealAdditive(Creature target, Creature source, decimal amount, ValueProp props, CardPlay? cardPlay) {
        return source == Owner ? amount : 0;
    }
}