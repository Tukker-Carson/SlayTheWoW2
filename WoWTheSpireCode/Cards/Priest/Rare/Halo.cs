using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using WoWTheSpire.WoWTheSpireCode.CustomProperties;
using WoWTheSpire.WoWTheSpireCode.Powers.Priest;
namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Halo() : PriestCard(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) {
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [WoWKeywords.Holy];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DevouringPlaguePower>(2),
        new CalculationBaseVar(2),
        new ExtraDamageVar(5),
        new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card, _) => GetDesecrated(card.Owner))
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(WoWKeywords.Desecrated)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) { 
        foreach (var enemy in CombatState!.Enemies)
            await DamageCmd.Attack(DynamicVars.CalculatedDamage.Calculate(enemy))
                .FromCard(this, play)
                .Targeting(enemy)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
    }
    
    private static decimal GetDesecrated(Player owner) {
        return owner.PlayerCombatState!.AllCards.Count(c =>
            c.Keywords.Contains(WoWKeywords.Desecrated) && c.Pile!.Type != PileType.Exhaust);
    }
    
    protected override void OnUpgrade() => DynamicVars.CalculationExtra.UpgradeValueBy(3);
}