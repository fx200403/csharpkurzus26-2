using FoldNyilvantarto.Models;

namespace FoldNyilvantarto.Services;

public class FieldService
{
    private readonly List<Field> _fields = new()
    {
        new Field(1, "Alsó-tábla", 12.5, "Búza", 6, "Elvetve"),
        new Field(2, "Felső-tábla", 8, "Kukorica", 9, "Elvetve"),
        new Field(3, "Nagy-rét", 5, "Napraforgó", 3, "Betakarítva")
    };

    public void AddField(Field field)
    {
        _fields.Add(field);
    }

    public IReadOnlyList<Field> GetAllFields()
    {
        return new List<Field>(_fields);
    }
}
