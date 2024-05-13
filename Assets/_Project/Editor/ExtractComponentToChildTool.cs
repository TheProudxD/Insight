using UnityEditorInternal;
using UnityEditor;
using UnityEngine;

public static class ExtractComponentToChildTool
{
    [MenuItem("CONTEXT/Component/Extract", priority = 504)]
    public static void ExtractMenuOption(MenuCommand command)
    {
        var sourceComponent = command.context as Component;
        var undoGroupIndex = Undo.GetCurrentGroup();
        Undo.IncrementCurrentGroup();
        if (sourceComponent == null) return;
        var gameObject = new GameObject(sourceComponent.GetType().Name)
        {
            transform =
            {
                parent = sourceComponent.transform,
                localScale = Vector3.one,
                localPosition = Vector3.zero,
                localRotation = Quaternion.identity
            }
        };

        Undo.RegisterCreatedObjectUndo(gameObject, "Created child obj");

        if (!ComponentUtility.CopyComponent(sourceComponent) ||
            !ComponentUtility.PasteComponentAsNew(gameObject))
        {
            Debug.LogError("Cannot extract component", sourceComponent.gameObject);
            Undo.CollapseUndoOperations(undoGroupIndex);
            Undo.PerformUndo();
            return;
        }

        Undo.DestroyObjectImmediate(sourceComponent);
        Undo.CollapseUndoOperations(undoGroupIndex);
    }
}