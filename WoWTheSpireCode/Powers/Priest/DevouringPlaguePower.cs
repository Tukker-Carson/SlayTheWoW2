using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class DevouringPlaguePower : BaseDoT {
    public override async Task<Task> AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        if (side == Owner.Side && Applier is not null ) await WoWCmd.Heal(Applier, Owner, 
            DynamicVars.Damage.BaseValue*(decimal)0.25, ValueProp.Move, null);
        return base.AfterSideTurnEnd(choiceContext, side, participants);
    }
}