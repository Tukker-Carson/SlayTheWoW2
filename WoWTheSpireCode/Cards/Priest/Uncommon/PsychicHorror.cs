using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class PsychicHorror() : PriestCard(-1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];
    protected override bool IsPlayable => Owner.HasPower<ShadowOrbPower>();
    protected override bool HasEnergyCostX => true;
    
    private IHoverTip[] _hoverTips = [
        HoverTipFactory.FromPower<ShadowOrbPower>(),
        HoverTipFactory.FromPower<FearPower>(),
        HoverTipFactory.FromPower<StrengthPower>()
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => _hoverTips;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new ("Potency", 0),
        new PowerVar<FearPower>(1)
    ];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        if (!Owner.HasPower<ShadowOrbPower>()) return;
        
        DynamicVars["Potency"].BaseValue = ResolveEnergyXValue()*Owner.Creature.GetPowerAmount<ShadowOrbPower>();
            
        await PowerCmd.Apply<StrengthPower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            -DynamicVars["Potency"].BaseValue,
            Owner.Creature,
            this);
        
        await PowerCmd.Apply<FearPower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(FearPower)].BaseValue,
            Owner.Creature,
            this);
        
        if (IsUpgraded) await PowerCmd.Apply<WeakPower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars["Potency"].BaseValue,
            Owner.Creature,
            this);
        
        await PowerCmd.Remove<ShadowOrbPower>(Owner.Creature);
    }

    protected override void OnUpgrade() =>
        _hoverTips = _hoverTips.Append(HoverTipFactory.FromPower<WeakPower>()).ToArray();
}
