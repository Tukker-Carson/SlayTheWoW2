using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Keywords;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class PowerWordBarrier() : PriestCard(3, CardType.Power, CardRarity.Rare, TargetType.AllAllies) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PowerWordBarrierPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<PowerWordBarrierPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal, WoWKeywords.Holy];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        await PowerCmd.Apply<PowerWordBarrierPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(PowerWordBarrierPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Ethereal);
}