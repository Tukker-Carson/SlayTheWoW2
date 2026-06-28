using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
using WoWTheSpire.WoWTheSpireCode.Keywords;
using WoWTheSpire.WoWTheSpireCode.Powers;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class VampiricTouch() : PriestCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VampiricTouchPower>(4), new IntVar("Potency", 2)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<VampiricTouchPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await PowerCmd.Apply<VampiricTouchPower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(VampiricTouchPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Innate);
}