using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class MindBlast() : PriestCard(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(22, ValueProp.Move), new PowerVar<ShadowOrbPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowOrbPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Desecrated];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        var holyCards = PileType.Draw.GetPile(Owner).Cards.Where(card => card.Keywords.Contains(WoWKeywords.Holy)).ToList();
        if (holyCards.Count == 0) return;
        var card = (await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            holyCards,
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, 1))).FirstOrDefault();
        if (card is null || CombatState is null) return;
        await WoWCmd.Desecrate(card, CombatState);
        CardCmd.Preview(card);
    }
    
    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(9);
}