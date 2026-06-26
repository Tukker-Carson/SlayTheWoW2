using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Cards;

public class Shadowform() : PriestCard(0, CardType.Skill, CardRarity.Basic, TargetType.Self) {
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ShadowformPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowformPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<ShadowformPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(ShadowformPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    // protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3);
}