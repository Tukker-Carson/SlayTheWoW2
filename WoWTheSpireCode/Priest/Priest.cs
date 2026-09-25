using System.Runtime.InteropServices;
using BaseLib.Abstracts;
using BaseLib.Utils.NodeFactories;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Basic;
using WoWTheSpire.WoWTheSpireCode.Cards.Priest.Common;
using WoWTheSpire.WoWTheSpireCode.Extensions;
using WoWTheSpire.WoWTheSpireCode.Relics;

namespace WoWTheSpire.WoWTheSpireCode.Priest;

public class Priest : CustomCharacterModel
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
        ModelDb.Card<Smite>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<PriestDefend>(),
        ModelDb.Card<HolyWordSanctuary>(),
        ModelDb.Card<Shadowform>()
    ];

    public override IReadOnlyList<RelicModel> StartingRelics => [
        ModelDb.Relic<CloakOfDiscipline>()
    ];

    public override CardPoolModel CardPool => ModelDb.CardPool<PriestCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<PriestRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<PriestPotionPool>();
    
    public override Control CustomIcon {
        get {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public string CharId = "ironclad";
    
    public override string CustomIconTexturePath => "priest_icon.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "priest_icon.png".CharacterUiPath();
    public override string CustomVisualPath => SceneHelper.GetScenePath("creature_visuals/" + CharId);
    public override string CustomTrailPath => SceneHelper.GetScenePath("vfx/card_trail_" + CharId);
    public override string CustomIconPath => SceneHelper.GetScenePath($"ui/character_icons/{CharId}_icon");
    public override string? CustomIconOutlineTexturePath => ImageHelper.GetImagePath($"ui/top_panel/character_icon_{CharId}_outline.png");
    public override string CustomEnergyCounterPath => SceneHelper.GetScenePath($"combat/energy_counters/{CharId}_energy_counter");
    public override string CustomRestSiteAnimPath => SceneHelper.GetScenePath($"rest_site/characters/{CharId}_rest_site");
    public override string CustomMerchantAnimPath => SceneHelper.GetScenePath($"merchant/characters/{CharId}_merchant");
    public override string CustomArmPointingTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{CharId}_point.png");
    public override string CustomArmRockTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{CharId}_rock.png");
    public override string CustomArmPaperTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{CharId}_paper.png");
    public override string CustomArmScissorsTexturePath => ImageHelper.GetImagePath($"ui/hands/multiplayer_hand_{CharId}_scissors.png");
    public override string CustomCharacterSelectBg => SceneHelper.GetScenePath("screens/char_select/char_select_bg_" + CharId);
    public override string CustomCharacterSelectTransitionPath => $"res://materials/transitions/{CharId}_transition_mat.tres";
    public override string CharacterSelectSfx => $"event:/sfx/characters/{CharId}/{CharId}_select";
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_" + CharId;
    public override string CustomAttackSfx => $"event:/sfx/characters/{CharId}/{CharId}_attack";
    public override string CustomCastSfx => $"event:/sfx/characters/{CharId}/{CharId}_cast";
    public override string CustomDeathSfx => $"event:/sfx/characters/{CharId}/{CharId}_die";

    public override List<string> GetArchitectAttackVfx() {
        const int num = 5;
        var list = new List<string>(num);
        CollectionsMarshal.SetCount(list, num);
        return list;
    }
}