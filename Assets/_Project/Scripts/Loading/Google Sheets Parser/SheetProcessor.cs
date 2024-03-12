using UnityEngine;

public class SheetProcessor
{
    private const char CELL_SEPARATOR = ',';
    private const int DATA_START_RAW_INDEX = 1;

    public EntityData ProcessData<T>(string cvsRawData) where T : EntitySpecs
    {
        var lineEnding = GetPlatformSpecificLineEnd();
        var rows = cvsRawData.Split(lineEnding);
        var data = new EntityData();
        for (int i = DATA_START_RAW_INDEX; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(CELL_SEPARATOR);
            var entitySpecs = ScriptableObject.CreateInstance<T>();
            entitySpecs.Initialize(cells);
            data.EntitiesOptions.Add(entitySpecs);
        }

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