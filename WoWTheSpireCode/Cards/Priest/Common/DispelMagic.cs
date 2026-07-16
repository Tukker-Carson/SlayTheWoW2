using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
    
public class DispelMagic() : PriestCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyPlayer) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        var target = play.Target ?? Owner.Creature;
        var debuffs = target.Powers.Where(x => x.Type == PowerType.Debuff).ToList();
        if (debuffs.Count != 0) await PowerCmd.Remove(debuffs[Owner.RunState.Rng.Niche.NextInt(debuffs.Count)]);
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}