using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class PsychicHorror() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<FearPower>(),
        HoverTipFactory.Static(WoWKeywords.Desecrate)
    ];
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Potency", 20),
        new PowerVar<FearPower>(1)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        var card = await WoWCmd.Desecrate(choiceContext, this, CombatState!, 5, 1);
        if (card is not null) await PowerCmd.Apply<FearPower>
            (choiceContext, play.Target, DynamicVars[nameof(FearPower)].BaseValue, Owner.Creature, this);
    }

    protected override void OnUpgrade() => DynamicVars["Potency"].UpgradeValueBy(12);
}
