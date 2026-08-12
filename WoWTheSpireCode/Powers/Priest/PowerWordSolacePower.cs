using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class PowerWordSolacePower : BaseDoT {
    public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants) {
        participants = participants.ToList();
        if (!participants.Contains(Owner)) return;
        var damages = await Tick(choiceContext);
        if (Applier is not null) foreach (var damage in damages) await WoWCmd.Heal(Applier, Owner, 
            damage.UnblockedDamage/2m, ValueProp.Unpowered, null);
        await PowerCmd.Decrement(this);
    }
}