using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class ShadowyApparitionPower : WoWTheSpirePower, IWoWDotTickListener {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    public decimal ModifyDotTickAdditive(Creature target, Creature source, decimal amount) =>
        source == Owner && Owner.HasPower<StrengthPower>() ? Owner.GetPowerAmount<StrengthPower>() : 0;
    //Need to recalculate damage on desecrate!
}