using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public class WoWPotencyVar : DynamicVar {
      public const string DefaultName = "Potency";

      public Creature? Source { get; set; }
      public Creature? Target { get; set; }

      
      public WoWPotencyVar(Decimal damage, Creature? target=null, Creature? source=null) : base(DefaultName, damage) {
          Target = target;
          Source = source;
      }

      public WoWPotencyVar(string name, Decimal damage, Creature? target=null, Creature? source=null) : base(name, damage) {
          Target = target;
          Source = source;
      }

      public override void UpdateCardPreview(CardModel card, CardPreviewMode previewMode, Creature? target, bool runGlobalHooks) {
          // Doesn't take enchantments into consideration
          if (runGlobalHooks) 
              PreviewValue = WoWCmd.ResolveDotTickAmount(card.Owner.Creature, card.Owner.Creature, BaseValue);
      }
}
