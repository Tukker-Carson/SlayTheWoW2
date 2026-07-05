using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class PowerWordFortitude() : PriestCard(1, CardType.Power, CardRarity.Common, TargetType.Self) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PowerWordFortitudePower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<PowerWordFortitudePower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy, CardKeyword.Ethereal];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<PowerWordFortitudePower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(PowerWordFortitudePower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Ethereal);
}