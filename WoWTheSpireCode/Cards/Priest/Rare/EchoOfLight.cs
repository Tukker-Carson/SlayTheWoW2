using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class EchoOfLight() : PriestCard(2, CardType.Power, CardRarity.Rare, TargetType.AllAllies) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<EchoOfLightPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<EchoOfLightPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(EchoOfLightPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}