using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class PowerWordSpirit() : PriestCard(1, CardType.Power, CardRarity.Rare, TargetType.AllAllies) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PowerWordSpiritPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<PowerWordSpiritPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        foreach (var player in CombatState!.Allies) await PowerCmd.Apply<PowerWordSpiritPower>(new ThrowingPlayerChoiceContext(),
            player,
            DynamicVars[nameof(PowerWordSpiritPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}