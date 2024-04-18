using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace siwar.Domain;

/// <summary>
/// Email templates.
/// </summary>
public static class EmailTemplates
{
    public const string ResetPassword = "reset-password";
}

/// <summary>
/// Common lists.
/// </summary>
public static class Lists
{
    public const string City = "Commune";

    public const string Departments = "Departement";

    public const string Regions = "Region";
}

/// <summary>
/// Application login providers.
/// </summary>
public static class LoginProviders
{
    public const string Legacy = "Legacy";
}

public static class Global
{
    /// <summary>
    /// Two types of roles :
    ///   1. Global roles, explicitly attached to user
    ///   2. UserStructure->roles, implicitly attached to user, explicitly attached to user structure
    /// </summary>
    public static readonly ReadOnlyCollection<string> Roles = new(new[] { "admin", "gipp" });
}

/// <summary>
/// all roles from legacy.
/// </summary>
public static class Roles
{
    public const string DraafManager = "draaf";
    public const string InspectionOrganismEmployee = "employe_oi";
    public const string ValidInspectionOrganismEmployee = "employe_oi_valide";
    public const string TrainingCenterTrainer = "formateur_cf";
    public const string ValidTrainingCenterTrainer = "formateur_cf_valide";
    public const string InspectionOrganismValidInspector = "inspecteur_valide_oi";
    public const string ValidInspectionOrganismValidInspector = "inspecteur_valide_oi_valide";
    public const string TrainingCenterManager = "responsable_cf";
    public const string ValidTrainingCenterManager = "responsable_cf_valide";
    public const string InspectionOrganismManager = "responsable_oi";
    public const string ValidInspectionOrganismManager = "responsable_oi_valide";
    public const string Gipp = "gipp";
    public const string GlobalAdministratorRole = "admin";
}

public static class Eligibility
{
    public static readonly ReadOnlyDictionary<string, IEnumerable<string>> RolesByRole = new(
        new Dictionary<string, IEnumerable<string>>
        {
            // Admin
            {
                Roles.GlobalAdministratorRole,
                new[]
                {
                    Roles.GlobalAdministratorRole, Roles.ValidInspectionOrganismManager,
                    Roles.ValidInspectionOrganismValidInspector, Roles.ValidInspectionOrganismEmployee,
                    Roles.ValidTrainingCenterManager, Roles.ValidTrainingCenterTrainer, Roles.DraafManager,
                    Roles.Gipp
                }
            },

            // Draaf manager
            {
                Roles.DraafManager,
                new string[]
                {
                    Roles.ValidInspectionOrganismManager, Roles.ValidInspectionOrganismValidInspector,
                    Roles.ValidInspectionOrganismEmployee, Roles.ValidTrainingCenterManager,
                    Roles.ValidTrainingCenterTrainer
                }
            },
            // GIPP
            {
                Roles.Gipp,
                new[]
                {
                    Roles.GlobalAdministratorRole, Roles.ValidInspectionOrganismManager,
                    Roles.ValidInspectionOrganismValidInspector, Roles.ValidInspectionOrganismEmployee,
                    Roles.ValidTrainingCenterManager, Roles.ValidTrainingCenterTrainer, Roles.DraafManager,
                    Roles.Gipp
                }
            },

            // Responsable_oi
            {
                Roles.ValidInspectionOrganismManager,
                new[]
                {
                    Roles.ValidInspectionOrganismManager, Roles.ValidInspectionOrganismValidInspector,
                    Roles.ValidInspectionOrganismEmployee
                }
            },

            // Responsable_cf
            {
                Roles.ValidTrainingCenterManager,
                new[]
                {
                    Roles.ValidTrainingCenterManager, Roles.TrainingCenterManager,
                    Roles.ValidTrainingCenterTrainer
                }
            }
        }
    );
}
