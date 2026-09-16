using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class InnerFire() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<InnerFirePower>(0)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        if (!Owner.HasPower<RenewPower>()) return;
        var renew = Owner.Creature.GetPower<RenewPower>()!;
        foreach (var enemy in CombatState!.HittableEnemies) {
            (await PowerCmd.Apply<InnerFirePower>(choiceContext, enemy, renew.Amount, Owner.Creature, this))?
                .SetPotency(renew.DynamicVars.Heal.IntValue);
        }
    }
}