using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public sealed class BlessedPower: WoWTheSpirePower, IWoWHealListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public decimal ModifyHealAdditive(Creature creature, decimal amount) {
        return creature == Owner ? amount : 0;
    }
}