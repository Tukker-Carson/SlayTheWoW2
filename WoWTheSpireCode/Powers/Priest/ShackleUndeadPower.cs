using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ShackleUndeadPower : WoWTheSpirePower, IWoWDotTickListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public decimal ModifyDotTickMultiplicative(Creature target, Creature source, decimal amount) =>
        target == Owner ? 2 : 1;
}