using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

  
public class Resurrection() : PriestCard(1, CardType.Skill, CardRarity.Rare, WoWTargetTypes.DyingAllies) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust, WoWKeywords.Holy];
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new("ResHP", 10)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        var target = play.Target.Player!;
        await target.ReviveBeforeCombatEnd();
        CombatManager.Instance.UndoReadyToEndTurn(target);
        target.Deck.InvokeContentsChanged();
        await PlayerCmd.GainEnergy(target.MaxEnergy, target);
        await CreatureCmd.SetCurrentHp(play.Target, 10);
        var newCards = CardFactory.GetDistinctForCombat(target,
            target.Character.CardPool.GetUnlockedCards(target.UnlockState, target.RunState.CardMultiplayerConstraint),
            15, target.RunState.Rng.CombatCardGeneration).ToArray();
        await CardPileCmd.AddGeneratedCardsToCombat(newCards, PileType.Discard, Owner);
        await CardPileCmd.Draw(choiceContext, 5, target);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}