using MegaCrit.Sts2.Core.Entities.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class CircleOfHealingPower() : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
}