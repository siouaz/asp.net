namespace OeuilDeSauron.Domain.Extensions;

/// <summary>
/// Field extensions.
/// </summary>
public static class FieldExtensions
{
    public static string GenerateKey(this int id)
    {
        return id switch
        {
            (int)Field.Category => "category",
            (int)Field.Type => "type",
            (int)Field.Brand => "brand",
            (int)Field.WorkingWidth => "workingWidth",
            (int)Field.Hitch => "hitch",
            (int)Field.Operation => "operation",
            (int)Field.Regulation => "regulation",
            (int)Field.NozzleOperation => "nozzleOperation",
            (int)Field.Nozzle => "nozzle",
            (int)Field.Activity => "activity",
            (int)Field.Department => "department",
            (int)Field.ControlType => "controlType",
            (int)Field.ControlConlusion => "controlConlusion",
            (int)Field.UsageMode => "usageMode",
            (int)Field.DefaultType => "defaultType",
            (int)Field.ImpossibleControlCause => "ImpossibleControlCause",
            _ => "autres",
        };
    }
}

public enum Field
{
    Brand = 1,
    Type = 2,
    Category = 5,
    WorkingWidth = 6,
    Regulation = 7,
    Hitch = 8,
    Operation = 9,
    NozzleOperation = 12,
    Nozzle = 11,
    Activity = 10,
    Department = 13,
    ControlType = 14,
    ControlConlusion = 15,
    UsageMode = 40,
    DefaultType = 17,
    ImpossibleControlCause = 16
}
