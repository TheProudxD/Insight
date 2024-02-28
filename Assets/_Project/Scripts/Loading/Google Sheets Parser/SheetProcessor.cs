using System.Collections.Generic;
using UnityEngine;

public class SheetProcessor
{
    private const char _cellSeporator = ',';
    
    public EntityData ProcessData<T>(string cvsRawData) where T: EntitySpecs
    {
        char lineEnding = GetPlatformSpecificLineEnd();
        string[] rows = cvsRawData.Split(lineEnding);
        int dataStartRawIndex = 1;
        EntityData data = new EntityData();
        for (int i = dataStartRawIndex; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(_cellSeporator);
            var entitySpecs = ScriptableObject.CreateInstance<T>();
            entitySpecs.Initialize(cells);
            data.EntitiesOptions.Add(entitySpecs);
        }
        return data;
    }
    
    private char GetPlatformSpecificLineEnd()
    {
        char lineEnding = '\n';
#if UNITY_IOS
        lineEnding = '\r';
#endif
        return lineEnding;
    }
}
