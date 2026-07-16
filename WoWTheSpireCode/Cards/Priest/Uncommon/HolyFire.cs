using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Uncommon;

public class HolyFire() : PriestCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<HolyFirePower>(2), 
        new IntVar("Potency", 6),
        new DamageVar(2, ValueProp.Move)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<HolyFirePower>()];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.DoT];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this, play).Targeting(play.Target).WithHitFx("vfx/vfx_attack_slash").Execute(choiceContext);
        await PowerCmd.Apply<HolyFirePower>(new ThrowingPlayerChoiceContext(),
            play.Target,
            DynamicVars[nameof(HolyFirePower)].BaseValue,
            Owner.Creature,
            this);
    }
    
    protected override void OnUpgrade() {
        DynamicVars[nameof(HolyFirePower)].UpgradeValueBy(1);
        DynamicVars.Damage.UpgradeValueBy(1);
    }
}