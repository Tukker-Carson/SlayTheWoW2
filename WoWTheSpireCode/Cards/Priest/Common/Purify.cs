using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
    
public class Purify() : PriestCard(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        var buffs = play.Target.Powers.Where(
            x => x.Amount > 0 && x is StrengthPower or DexterityPower or ArtifactPower).ToList();
        foreach (var buff in buffs) await PowerCmd.Decrement(buff);
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}