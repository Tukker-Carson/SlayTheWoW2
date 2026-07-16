using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class ChakraChastise() : PriestCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ChakraChastisePower>(1)];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<ChakraChastisePower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(ChakraChastisePower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}
