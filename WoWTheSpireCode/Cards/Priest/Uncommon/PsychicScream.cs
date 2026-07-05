using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class PsychicScream() : PriestCard(0, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<FearPower>(2), new IntVar("Potency", 3)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<FearPower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT, CardKeyword.Exhaust];
    public override bool CanBeGeneratedInCombat => false;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(CombatState);
        foreach (var target in CombatState.Enemies)await PowerCmd.Apply<FearPower>(new ThrowingPlayerChoiceContext(),
            target,
            DynamicVars[nameof(FearPower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() => DynamicVars[nameof(FearPower)].UpgradeValueBy(1);
}