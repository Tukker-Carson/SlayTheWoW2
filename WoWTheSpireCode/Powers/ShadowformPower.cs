using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Powers;

public class ShadowformPower() : WoWTheSpirePower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ShadowformPower>(1)];

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource) {
        if (!props.IsPoweredAttack()) return 1;
        if (Owner == dealer) return 1.1M;
        if (Owner == target) return 0.9M;
        return 1;
    }
}