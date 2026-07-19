using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class FocusedWill() : PriestCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {
    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        foreach (var card in PileType.Hand.GetPile(Owner).Cards) if (card.Keywords.Contains(WoWKeywords.Holy)) 
            card.EnergyCost.SetThisTurnOrUntilPlayed(0, true);
        return Task.CompletedTask;
    }
    
    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}