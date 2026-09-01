using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace WoWTheSpire.WoWTheSpireCode.Cards.Priest.Rare;

public class Confession() : PriestCard(0, CardType.Skill, CardRarity.Rare, TargetType.AllAllies) {
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ArtifactPower>(1)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<ArtifactPower>()];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play) {
        var selected = (await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            CombatState!.Players.Where(player => player != Owner).SelectMany(player => PileType.Hand.GetPile(player).Cards).ToList(),
            Owner,
            new CardSelectorPrefs(SelectionScreenPrompt, 1))).FirstOrDefault();;
        if (selected is null) return;
        var clone = selected.CreateCloneForPlayer(Owner);
        await CardCmd.AutoPlay(choiceContext, clone, null);
        await CardCmd.Exhaust(choiceContext, clone);
    }
    
    protected override void OnUpgrade() => DynamicVars[nameof(ArtifactPower)].UpgradeValueBy(1);
}