using System.Collections.Generic;
using UnityEngine;

public class SheetProcessor
{
    private const char CELL_SEPARATOR = ',';
    private const int DATA_START_RAW_INDEX = 1;

    private readonly bool _isDebug;

    public SheetProcessor(bool isDebug) => _isDebug = isDebug;

    public List<EntitySpecs> ProcessData<T>(string cvsRawData) where T : EntitySpecs, new()
    {
        var lineEnding = GetPlatformSpecificLineEnd();
        var rows = cvsRawData.Split(lineEnding);
        var data = new List<EntitySpecs>();
        for (var i = DATA_START_RAW_INDEX; i < rows.Length; i++)
        {
            var cells = rows[i].Split(CELL_SEPARATOR);
            var entitySpecs = new T();
            entitySpecs.Initialize(cells);
            data.Add(entitySpecs);
        }

        if (_isDebug)
            data.ForEach(x => Debug.Log($"Parsed from GS with {x}"));
        return data;
    }

    private char GetPlatformSpecificLineEnd()
    {
        var lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
        return lineEnding;
    }
}