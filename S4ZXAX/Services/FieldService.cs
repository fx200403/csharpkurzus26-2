using System;
using System.Linq;
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

    public int GetNextId()
    {
        if (_fields.Count == 0)
        {
            return 1;
        }

        int maxId = _fields[0].Id;
        foreach (Field field in _fields)
        {
            if (field.Id > maxId)
            {
                maxId = field.Id;
            }
        }

        return checked(maxId + 1);
    }

    public List<Field> SearchFieldsByCrop(string cropType)
    {
        string normalizedCropType = cropType.Trim();

        return _fields
            .Where(field => string.Equals(field.CropType, normalizedCropType, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public List<Field> GetFieldsOrderedByArea()
    {
        return _fields
            .OrderByDescending(field => field.AreaHectares)
            .ToList();
    }
}
