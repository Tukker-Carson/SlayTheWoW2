using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class DivineInsightPower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new IntVar("Potency", 0)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<FearPower>()];

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource) {
        if (power != this || cardSource is null) return;
        DynamicVars["Potency"].BaseValue = Math.Max(DynamicVars["Potency"].BaseValue, cardSource.DynamicVars["Potency"].BaseValue);
        _applier = applier;
    }

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (Applier is not null && !participants.Contains(Applier)) return;
        if (Owner.Monster is null || Owner.Monster.IntendsToAttack) {
            await PowerCmd.Apply<FearPower>(new ThrowingPlayerChoiceContext(), Owner, Amount, Applier, null);
            if (Owner.HasPower<FearPower>())
                Owner.GetPower<FearPower>()!.DynamicVars["Potency"].BaseValue = DynamicVars["Potency"].BaseValue;
        }
        await PowerCmd.Remove(this);
    }
}