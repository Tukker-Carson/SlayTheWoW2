using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class ShadowFiend() : PriestCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ShadowFiendPower>(6), 
        new IntVar("Potency", 8),
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowFiendPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT, CardKeyword.Exhaust];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await PowerCmd.Apply<ShadowFiendPower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(ShadowFiendPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() {
        DynamicVars["Potency"].UpgradeValueBy(2);
    }
}