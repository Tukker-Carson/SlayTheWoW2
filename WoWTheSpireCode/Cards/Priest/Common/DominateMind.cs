using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class DominateMind() : PriestCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<FearPower>(1),
        new IntVar("Potency", 10),
        new BlockVar(4, ValueProp.Move)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<FearPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target);
        await PowerCmd.Apply<FearPower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(FearPower)].BaseValue,
            Owner.Creature,
            this);
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, play);
    }

    protected override void OnUpgrade() {
        DynamicVars["Potency"].UpgradeValueBy(5);
        DynamicVars.Block.UpgradeValueBy(3);
    }
}