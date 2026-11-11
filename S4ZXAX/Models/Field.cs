namespace FoldNyilvantarto.Models;

public record class Field(
    int Id,
    string Name,
    double AreaHectares,
    string CropType,
    double ExpectedYieldPerHectare,
    string Status
);
