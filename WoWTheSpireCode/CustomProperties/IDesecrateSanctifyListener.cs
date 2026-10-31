using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Models;

namespace WoWTheSpire.WoWTheSpireCode.CustomProperties;

public interface IDesecrateSanctifyListener {
    /// <summary>Runs before desecration.</summary>
    Task BeforeDesecrate(CardModel card) => Task.CompletedTask;
    
    /// <summary>Runs after desecration.</summary>
    Task AfterDesecrate(CardModel card) => Task.CompletedTask;

    /// <summary>Runs before desecration.</summary>
    Task BeforeSanctify(CardModel card) => Task.CompletedTask;
    
    /// <summary>Runs after desecration.</summary>
    Task AfterSanctify(CardModel card) => Task.CompletedTask;
}