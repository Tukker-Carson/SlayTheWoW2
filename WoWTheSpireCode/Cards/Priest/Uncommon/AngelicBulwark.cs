using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class AngelicBulwark() : PriestCard(0, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AngelicBulwarkPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<AngelicBulwarkPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(AngelicBulwarkPower)].BaseValue,
            Owner.Creature,
            this);
        if (IsUpgraded) await CardPileCmd.Draw(choiceContext, Owner);
    }
}