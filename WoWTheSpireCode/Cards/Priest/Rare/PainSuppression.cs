using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class PainSuppression() : PriestCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyPlayer) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PainSuppressionPower>(3),
        new ("BasePowerLevel", 3)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<PainSuppressionPower>(
            new ThrowingPlayerChoiceContext(),
            play.Target ?? Owner.Creature,
            DynamicVars[nameof(PainSuppressionPower)].BaseValue,
            Owner.Creature,
            this);
        DynamicVars[nameof(PainSuppressionPower)].BaseValue -= 1;
    }

    public override Task AfterCombatEnd(CombatRoom room) {
        DynamicVars[nameof(PainSuppressionPower)].BaseValue = DynamicVars["BasePowerLevel"].BaseValue;
        return base.AfterCombatEnd(room);
    }

    protected override void OnUpgrade() {
        DynamicVars[nameof(PainSuppressionPower)].BaseValue += 1;
        DynamicVars["BasePowerLevel"].BaseValue += 1;
    }
}