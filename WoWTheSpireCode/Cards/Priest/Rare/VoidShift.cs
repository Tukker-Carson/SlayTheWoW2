using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;


namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class VoidShift() : PriestCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override bool IsPlayable => Owner.RunState.CurrentRoom is not { RoomType: RoomType.Boss };
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        var ownerHealth = (decimal)Owner.Creature.CurrentHp/Owner.Creature.MaxHp;
        await CreatureCmd.SetCurrentHp(Owner.Creature, (decimal)play.Target.CurrentHp/play.Target.MaxHp*Owner.Creature.MaxHp);
        await CreatureCmd.SetCurrentHp(play.Target, ownerHealth*play.Target.MaxHp);
    }
    
    public override async Task AfterCombatVictory(CombatRoom room) {
        if (room is not { RoomType: RoomType.Boss } && Pile is { Type: PileType.Deck }) await CardPileCmd.RemoveFromDeck(this);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}