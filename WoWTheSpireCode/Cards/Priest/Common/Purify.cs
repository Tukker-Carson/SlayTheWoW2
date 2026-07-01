using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
    
public class Purify() : PriestCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        var buffs = play.Target.Powers.Where(x => x.Type == PowerType.Buff).ToList();
        if (buffs.Count != 0) await PowerCmd.Remove(buffs[new MegaRandom().Next(buffs.Count)]);
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}