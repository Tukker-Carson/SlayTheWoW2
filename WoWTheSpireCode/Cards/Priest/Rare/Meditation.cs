using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Meditation() : PriestCard(1, CardType.Power, CardRarity.Rare, TargetType.Self) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<MeditationPower>(4)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<MeditationPower>(
            new ThrowingPlayerChoiceContext(),
            play.Target ?? Owner.Creature,
            DynamicVars[nameof(MeditationPower)].BaseValue,
            Owner.Creature,
            this);
        DynamicVars[nameof(MeditationPower)].BaseValue -= 1;
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}