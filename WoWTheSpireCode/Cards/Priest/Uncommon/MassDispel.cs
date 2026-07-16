using BaseLib.Patches.Features;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Random;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;
    
public class MassDispel() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, CustomTargetType.Everyone) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(CombatState);
        foreach (var creature in CombatState.Creatures)
            if (CombatState.Allies.Contains(creature)) {
                var debuffs = creature.Powers.Where(x => x.Type == PowerType.Debuff).ToList();
                if (debuffs.Count != 0) await PowerCmd.Remove(debuffs[Owner.RunState.Rng.Niche.NextInt(debuffs.Count)]);
            } else {
                var buffs = creature.Powers.Where(x => x.Type == PowerType.Buff).ToList();
                if (buffs.Count != 0) await PowerCmd.Remove(buffs[Owner.RunState.Rng.Niche.NextInt(buffs.Count)]);
            }
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}