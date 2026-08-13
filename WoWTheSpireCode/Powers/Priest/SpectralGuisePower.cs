using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class SpectralGuisePower: WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource) {
        if (target != Owner || result.UnblockedDamage == 0 || dealer is null) return;
        await PowerCmd.Apply<FearPower>(choiceContext, dealer, 1, Owner, null);
        dealer.GetPower<FearPower>()!.DynamicVars["Potency"].BaseValue = 
            Math.Max(result.UnblockedDamage/2m, dealer.GetPower<FearPower>()!.DynamicVars["Potency"].BaseValue);
        await PowerCmd.Decrement(this);
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (participants.Contains(Owner)) await PowerCmd.Remove(this);
    }
}