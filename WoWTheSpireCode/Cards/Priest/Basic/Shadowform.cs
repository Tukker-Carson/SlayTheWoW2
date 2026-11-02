using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Basic;

public class Shadowform() : PriestCard(0, CardType.Skill, CardRarity.Basic, TargetType.Self) {
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ShadowformPower>(1),
        new ("ExtraDamage", 1)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ShadowformPower>(),
        HoverTipFactory.Static(WoWKeywords.Desecrate),
        HoverTipFactory.FromKeyword(WoWKeywords.Holy)
    ];

    public override decimal ModifyDamageAdditive(Creature? target, decimal amount, ValueProp props, Creature? dealer,
        CardModel? cardSource, CardPlay? cardPlay) {
        return Pile is not null && Pile.Type == PileType.Hand && dealer is not null && dealer == Owner.Creature ? 1 : 0;
    }
 
    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw) {
        if (card == this) await PowerCmd.Apply<ShadowformPower>(new ThrowingPlayerChoiceContext(),
            Owner.Creature,
            DynamicVars[nameof(ShadowformPower)].BaseValue,
            Owner.Creature,
            this);
    }

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        // No effect when played
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars["ExtraDamage"].UpgradeValueBy(2);
}