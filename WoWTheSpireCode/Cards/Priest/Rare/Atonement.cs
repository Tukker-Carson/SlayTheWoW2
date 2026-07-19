using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Atonement() : PriestCard(2, CardType.Power, CardRarity.Rare, TargetType.Self) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AtonementPower>(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<AtonementPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(AtonementPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => DynamicVars.Power<AtonementPower>().UpgradeValueBy(1);
}