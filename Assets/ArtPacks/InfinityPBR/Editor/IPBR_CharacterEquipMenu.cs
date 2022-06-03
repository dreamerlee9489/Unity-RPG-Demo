using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class IPBR_CharacterEquipMenu {

    [MenuItem("Window/Infinity PBR/Character Equip")] // Provides a menu item
    public static void EquipCharacterMenu()
    {
        if (Selection.activeGameObject)
        {
            IPBR_CharacterEquip.EquipCharacter(Selection.activeGameObject);
        }
        else
        {
#if UNITY_EDITOR
            Debug.Log("No Object Selected!");
#endif
        }
    }
    
    
}