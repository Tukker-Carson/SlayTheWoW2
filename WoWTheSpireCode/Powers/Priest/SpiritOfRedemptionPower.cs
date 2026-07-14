using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public sealed class SpiritOfRedemptionPower: WoWTheSpirePower, IHealAmountModifier {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BoolVar("Active", false)];

    public override bool ShouldDie(Creature creature) {
        if (creature != Owner) return true;
        DynamicVars["Active"].BaseValue = 1;
        CreatureCmd.SetCurrentHp(Owner, 1);
        return false;
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (!participants.Contains(Owner) ||  DynamicVars["Active"].BaseValue == 0) return;
        await PowerCmd.TickDownDuration(this);
    }

    public decimal ModifyHealMultiplicative(Creature creature, decimal amount) {
        return creature != Owner || DynamicVars["Active"].BaseValue == 0 ? 1 : 1.5M;
    }
}