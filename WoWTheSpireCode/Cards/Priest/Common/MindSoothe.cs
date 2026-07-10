using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;

public class MindSoothe() : PriestCard(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ShadowOrbPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ShadowOrbPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(play.Target.Monster, "cardPlay.Target.Monster");
        if (play.Target.Monster.NextMove.Intents.Any(intent => {
                return intent.IntentType switch {
                    IntentType.Attack => true,
                    _ => false
                };
            }) && (Owner.Creature.HasPower<ShadowformPower>() || IsUpgraded)) {
            await PowerCmd.Apply<ShadowOrbPower>(new ThrowingPlayerChoiceContext(),
                Owner.Creature,
                DynamicVars[nameof(ShadowOrbPower)].BaseValue,
                Owner.Creature,
                this);
        }
    }
}