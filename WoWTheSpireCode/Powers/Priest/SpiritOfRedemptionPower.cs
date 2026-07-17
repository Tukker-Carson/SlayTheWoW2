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

    public override bool ShouldDie(Creature creature) => creature != Owner;
    
    public override async Task AfterPreventingDeath(Creature creature) {
        DynamicVars["Active"].BaseValue = 1;
        await CreatureCmd.SetCurrentHp(Owner, 1);
    }

    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (!participants.Contains(Owner) ||  DynamicVars["Active"].BaseValue == 0) return;
        await PowerCmd.TickDownDuration(this);
        if (Amount == 0) await CreatureCmd.Kill(Owner, true);
    }

    public decimal ModifyHealMultiplicative(Creature creature, decimal amount) {
        return creature != Owner || DynamicVars["Active"].BaseValue == 0 ? 1 : 1.5M;
    }
}