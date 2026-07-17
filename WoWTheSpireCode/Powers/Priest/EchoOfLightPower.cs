using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public sealed class EchoOfLightPower: WoWTheSpirePower, IHealAmountModifier {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new("heal1", 0),
        new("heal2", 0),
        new("heal3", 0)
    ];
    
    
}