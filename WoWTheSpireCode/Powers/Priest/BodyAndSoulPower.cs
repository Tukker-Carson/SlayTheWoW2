using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Platform;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class BodyAndSoulPower: WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new StringVar("Applier"),
        new BoolVar("ApplierIsYou", true)
    ];

    public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel? cardSource) {
        if (creature != Owner) return;
        await PowerCmd.Apply<BlessedPower>(new ThrowingPlayerChoiceContext(), Owner, Amount, Owner, null);
        await PowerCmd.Apply<TemporarilyBlessedPower>(new ThrowingPlayerChoiceContext(), Owner, Amount, Owner, null);
    }
    
    public override Task AfterApplied(Creature? applier, CardModel? cardSource) {
        ((StringVar)DynamicVars["Applier"]).StringValue = Applier!.Player!.NetId == 1 ? "You" : PlatformUtil.GetPlayerName(RunManager.Instance.NetService.Platform, Applier!.Player!.NetId);
        ((BoolVar)DynamicVars["ApplierIsYou"]).BaseValue = Applier!.Player!.NetId == 1 ? 1 : 0;
        return Task.CompletedTask;
    }
}