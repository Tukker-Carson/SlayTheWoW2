using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Basic;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class HolyWordSanctuary() : PriestCard(1, CardType.Attack, CardRarity.Common, TargetType.Self) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(6, ValueProp.Move), new HealVar(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
        await WoWCmd.Heal(Owner.Creature, Owner.Creature, DynamicVars.Heal.BaseValue,  ValueProp.Move, play);
    }
    
    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy) {
        if (card != this || card.Pile is null || card.Pile.Type != PileType.Deck) return;
        var removeCards = card.Owner.Deck.Cards.OfType<PriestDefend>().ToList();
        if (removeCards.Count == 0) return;
        CardModel removeCard =  removeCards[0];
        foreach (var c in removeCards.Where(c => 
                     c.DynamicVars.Block.BaseValue < removeCard.DynamicVars.Block.BaseValue ||
                     c.DynamicVars.Block.BaseValue == removeCard.DynamicVars.Block.BaseValue &&
                     c.Enchantment is null && removeCard.Enchantment is not null)) 
            removeCard = c;
        await CardPileCmd.RemoveFromDeck(removeCard);
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(3);
        DynamicVars.Heal.UpgradeValueBy(1);
    }
}