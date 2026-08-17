using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Basic;
using WoWTheSpire.WoWTheSpireCode.Extensions;
using WoWTheSpire.WoWTheSpireCode.Relics;

namespace WoWTheSpire.WoWTheSpireCode.Priest;

public class Priest : PlaceholderCharacterModel
{
    public const string CharacterId = "Priest";

    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Feminine;
    public override int StartingHp => 32;

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<PriestStrike>(),
        ModelDb.Card<PriestStrike>(),
        ModelDb.Card<PriestStrike>(),
        ModelDb.Card<PriestStrike>(),
        ModelDb.Card<PriestStrike>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<Shadowform>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics => [
        ModelDb.Relic<CloakOfDiscipline>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<PriestCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<PriestRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<PriestPotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "priest_icon.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "priest_icon.png".CharacterUiPath();
}