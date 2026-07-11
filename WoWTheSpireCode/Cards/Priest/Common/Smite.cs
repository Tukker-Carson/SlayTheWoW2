using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Basic;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class Smite() : PriestCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        await PowerCmd.Remove<ShadowformPower>(Owner.Creature);
    }
    
    public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel? clonedBy) {
        if (card != this || card.Pile is null || card.Pile.Type != PileType.Deck) return;
        var removeCards = card.Owner.Deck.Cards.OfType<PriestStrike>().ToList();
        if (removeCards.Count == 0) return;
        CardModel removeCard =  removeCards[0];
        foreach (var c in removeCards.Where(c => 
                     c.DynamicVars.Damage.BaseValue < removeCard.DynamicVars.Damage.BaseValue ||
                     c.DynamicVars.Damage.BaseValue == removeCard.DynamicVars.Damage.BaseValue &&
                     c.Enchantment is null && removeCard.Enchantment is not null)) 
            removeCard = c;
        await CardPileCmd.RemoveFromDeck(removeCard);
    }
    
    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(4);
}