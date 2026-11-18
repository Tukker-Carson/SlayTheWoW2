using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

  
public class Resurrection() : PriestCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, WoWKeywords.Holy];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await WoWCmd.Sanctify(choiceContext, this, CombatState!, 4, 0);
        if (IsUpgraded && !CanonicalKeywords.Contains(WoWKeywords.Holy)) await CardCmd.Exhaust(choiceContext, this);
    }
    
    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}   