using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class ChakraSerenity() : PriestCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ChakraSerenityPower>(2),
        new ("Potency", 4)
    ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<BlessedPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<ChakraSerenityPower>(choiceContext, Owner.Creature,
            DynamicVars[nameof(ChakraSerenityPower)].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars[nameof(ChakraSerenityPower)].UpgradeValueBy(1);
}

