using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;
namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Levitate() : PriestCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self) {
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<LevitatePower>(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<LevitatePower>(
            new ThrowingPlayerChoiceContext(),
            play.Target ?? Owner.Creature,
            DynamicVars[nameof(LevitatePower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => DynamicVars[nameof(LevitatePower)].UpgradeValueBy(1);
}