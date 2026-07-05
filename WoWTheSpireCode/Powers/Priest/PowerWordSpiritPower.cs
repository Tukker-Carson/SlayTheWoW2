using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Powers.Priest;

public class PowerWordSpiritPower : WoWTheSpirePower {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player) {
        var card = await CardPileCmd.Draw(choiceContext, player);
        if (card is not null) {
            if (!card.Keywords.Contains<CardKeyword>(WoWKeywords.Holy)) await CardCmd.Discard(choiceContext, card);
            else CardCmd.Upgrade(card);
            CardCmd.Preview(card, 0.6F);
        }
    }
}