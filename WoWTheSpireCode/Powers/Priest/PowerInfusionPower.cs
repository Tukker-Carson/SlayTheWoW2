using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class PowerInfusionPower : WoWTheSpirePower { 
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

    public override async Task AfterEnergyReset(Player player) {
        if (Owner.Player is not null) await PlayerCmd.GainEnergy(1, Owner.Player);
        Flash();
        await PowerCmd.Decrement(this);
    }
}