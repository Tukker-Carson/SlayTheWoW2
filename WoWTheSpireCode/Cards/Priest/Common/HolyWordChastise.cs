using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class HolyWordChastise() : PriestCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        if (!cardPlay.Card.Keywords.Contains(WoWKeywords.Holy) || Pile != null && Pile.Type != PileType.Hand) return;
        await CardCmd.AutoPlay(choiceContext, this, null);
        await CardPileCmd.Draw(choiceContext, Owner);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3);
}