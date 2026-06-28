using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;

namespace WoWTheSpire.WoWTheSpireCode.Powers;

public class VampiricTouchPower : BaseDoT {

    public override async Task AfterEnergyReset(Player player) {
        if (Applier is { Player: not null } && player == Applier.Player) await PlayerCmd.GainEnergy(1, Applier.Player);
    }
}