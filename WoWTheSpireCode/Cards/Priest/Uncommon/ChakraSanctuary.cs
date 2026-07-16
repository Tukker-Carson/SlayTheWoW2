using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class ChakraSanctuary() : PriestCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ChakraSanctuaryPower>(3),
        new ("Potency", 1)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<RenewPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<ChakraSanctuaryPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(ChakraSanctuaryPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}