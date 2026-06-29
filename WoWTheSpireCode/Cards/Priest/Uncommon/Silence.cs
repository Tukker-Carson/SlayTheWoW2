using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class Silence() : PriestCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ArtifactPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ArtifactPower>()];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(play.Target.Monster, "cardPlay.Target.Monster");
        ArgumentNullException.ThrowIfNull(CombatState);
        if( play.Target.Monster.NextMove.Intents.Any(intent => { 
               return intent.IntentType switch {
                   IntentType.Debuff or IntentType.CardDebuff => true,
                   _ => false
               };
           })) foreach (var player in CombatState.Allies) 
            await PowerCmd.Apply<ArtifactPower>(new ThrowingPlayerChoiceContext(), 
                player,
                DynamicVars[nameof(ArtifactPower)].BaseValue,
                Owner.Creature,
                this);
    }
    
    protected override void OnUpgrade() => AddKeyword(CardKeyword.Retain);
}