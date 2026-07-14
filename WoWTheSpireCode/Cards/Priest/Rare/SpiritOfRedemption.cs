using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class SpiritOfRedemption() : PriestCard(3, CardType.Power, CardRarity.Rare, TargetType.AllAllies) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<SpiritOfRedemptionPower>(3)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<SpiritOfRedemptionPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(SpiritOfRedemptionPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}