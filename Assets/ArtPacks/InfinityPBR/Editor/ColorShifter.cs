using System;
using UnityEditor;
using UnityEngine;
using InfinityPBR;
using PlasticGui.Help;

namespace InfinityPBR
{
    [System.Serializable]
    public class ColorShifter : EditorWindow
    {
        private string _shaderName = "InfinityPBR/LPColor";
        private string _shaderNameHDRP = "InfinityPBR/LPColorHDRP";
        private string _shaderNameURP = "InfinityPBR/LPColorURP";
        public ColorShifterObject colorShifterObject;
        private Shader _shader;
        private int childSelectIndex = 0;

        private string[] childNames;
        
        Vector2 scrollPos;

        private string[] colorSetOptions;

        [MenuItem("Window/Infinity PBR/Color Shifter")]
        public static void ShowWindow() => GetWindow<ColorShifter>(false, "Color Shifter", true);
        private ColorShifterColorSet ActiveColorSet() => colorShifterObject.colorSets[colorShifterObject.activeColorSetIndex];

        private void CheckListSize()
        {
            if (!colorShifterObject)
                return;

            for (int i = 0; i < colorShifterObject.colorSets.Count; i++)
            {
                if (colorShifterObject.colorSets[i].colorShifterItems.Count >= 49) continue;
                
                for (int v = 0; v < 49; v++)
                {
                    if (colorShifterObject.colorSets[i].colorShifterItems.Count >= v + 1) continue;
                    
                    colorShifterObject.colorSets[i].colorShifterItems.Add(new ColorShifterColorItem());
                    colorShifterObject.colorSets[i].colorShifterItems[v].name = "Unnamed Color " + v;
                    colorShifterObject.colorSets[i].colorShifterItems[v].shaderIndex = v;
                    colorShifterObject.colorSets[i].colorShifterItems[v].orderIndex = v;
                }
            }
        }

        void OnGUI()
        {
            CheckListSize();

            UpdateLinks();
            
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            
            if (!Shader.Find(_shaderName) && !Shader.Find(_shaderNameURP) && !Shader.Find(_shaderNameHDRP))
            {
                EditorGUILayout.HelpBox(
                    "Can't find the shader \"" + _shaderName + "\", \"" + _shaderNameURP + "\", or \"" + _shaderNameHDRP + "\". Please make sure the shader is in the project before continuing.", MessageType.Error);
                EditorGUILayout.EndScrollView();
                return;
            }

            if (colorShifterObject)
            {
                SetColorSetOptions();
            }

            EditorGUILayout.BeginHorizontal();
            EditorPrefs.SetBool("ColorShifter_ShowHelp", EditorGUILayout.Toggle("Show Help Boxes", EditorPrefs.GetBool("ColorShifter_ShowHelp")));
            EditorPrefs.SetBool("ColorShifter_ShowFull", EditorGUILayout.Toggle("Show Full Data", EditorPrefs.GetBool("ColorShifter_ShowFull")));
            EditorGUILayout.EndHorizontal();
            
            if (EditorPrefs.GetBool("ColorShifter_ShowHelp"))
            {
                EditorGUILayout.HelpBox(
                    "COLOR SHIFTER by Infinity PBR\nThis tool is meant to make it easy to set the color of low poly / faceted " +
                    "objects which make use of a texture made up of 49 or fewer distinct colors.\n\nFor a quickstart video, please visit the tutorials hosted at http://www.InfinityPBR.com", MessageType.None);
            }

            if (!colorShifterObject)
            {
                if (EditorPrefs.GetBool("ColorShifter_ShowHelp"))
                {
                    EditorGUILayout.HelpBox(
                        "Please select a Color Shifter Object, drag-and-drop one into the field below, or create a new object.\n\nTo create a new ojbect, navigate to the location in the Project where you would " +
                        "like to keep your object, right click and select \"Create/Infinity PBR/Create Color Shifter Object\", and then name your object as you'd like.", MessageType.Warning);
                }
            }
            
            colorShifterObject = EditorGUILayout.ObjectField("Color Shifter Object", colorShifterObject,
                typeof(ColorShifterObject), false) as ColorShifterObject;
            
            if (colorShifterObject == null)
            {
                EditorGUILayout.EndScrollView();
                return;
            }
            
            colorShifterObject.material = EditorGUILayout.ObjectField("Material", colorShifterObject.material, typeof(Material), false) as Material;
            if (colorShifterObject.material == null)
            {
                if (EditorPrefs.GetBool("ColorShifter_ShowHelp"))
                {

                    EditorGUILayout.HelpBox(
                        "Select a material to manage. The shader for the material should be \"" + _shaderName + "\", \"" + _shaderNameURP + "\", or \"" + _shaderNameHDRP + "\".",
                        MessageType.Warning);
                }

                _shader = null;
                EditorGUILayout.EndScrollView();
                return;
            }
            
            _shader = colorShifterObject.material.shader;

            if (_shader.name != _shaderName && _shader.name != _shaderNameURP && _shader.name != _shaderNameHDRP)
            {
                EditorGUILayout.HelpBox(
                    "The shader for the material must be \"" + _shaderName + "\", \"" + _shaderNameURP + "\", or \"" + _shaderNameHDRP + "\".",
                    MessageType.Error);
                if (GUILayout.Button("Set Shader"))
                {
                    Undo.RecordObject(colorShifterObject.material, "Set Shader");
                    SetShader();
                }
                EditorGUILayout.EndScrollView();
                return;
            }
            
            colorShifterObject.activeColors = Mathf.Clamp(EditorGUILayout.IntField("Active Colors", colorShifterObject.activeColors), 0, 49);

            
            if (EditorPrefs.GetBool("ColorShifter_ShowFull"))
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginVertical();
                colorShifterObject.material.SetFloat("_ColorIDRange", EditorGUILayout.FloatField("Range",colorShifterObject.material.GetFloat("_ColorIDRange")));
                colorShifterObject.material.SetFloat("_ColorIDFuzziness",EditorGUILayout.FloatField("Fuzziness",colorShifterObject.material.GetFloat("_ColorIDFuzziness")));

                EditorGUILayout.EndVertical();
                EditorGUILayout.HelpBox(
                    "In most cases Range and Fuzziness values should be set to 0.01.",
                    MessageType.Warning);
                EditorGUILayout.EndHorizontal();
            }
            
            if (colorShifterObject.material.GetFloat("_ColorIDRange") < 0.01f)
                colorShifterObject.material.SetFloat("_ColorIDRange", 0.01f);
            if (colorShifterObject.material.GetFloat("_ColorIDFuzziness") < 0.01f)
                colorShifterObject.material.SetFloat("_ColorIDFuzziness", 0.01f);

            if (colorShifterObject.exportPath == "" || colorShifterObject.exportPath == null)
            {
                EditorGUILayout.HelpBox(
                    "Please click the button below to set the exportPath for saving PNG files of your textures.",
                    MessageType.Error);
            }
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Export Path", colorShifterObject.exportPath, EditorStyles.wordWrappedLabel);
            if (GUILayout.Button("Choose"))
            {
                colorShifterObject.exportPath = EditorUtility.OpenFolderPanel("Choose export destination", "", "");
            }

            EditorGUILayout.EndHorizontal();
            if (colorShifterObject.exportPath == "")
                colorShifterObject.exportPath = Application.dataPath + "/";

            /*
            EditorPrefs.SetBool("Show Full Texture in Shifter", EditorGUILayout.Foldout(EditorPrefs.GetBool("Show Full Texture in Shifter"), "Color ID Texture"));
            if (EditorPrefs.GetBool("Show Full Texture in Shifter"))
            {
                GUILayout.Label(colorShifterObject.material.mainTexture, GUILayout.Width(300));
            }
            */
            
            
            // ---------------------------------------
            // COLOR SET SELECTION
            // ---------------------------------------
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
            if (EditorPrefs.GetBool("ColorShifter_ShowHelp"))
            {
                EditorGUILayout.HelpBox(
                    "You can have multiple \"Color Sets\", and easily switch between them at run time. Select a color set below, or create a new one. Each set " +
                    "can have unique color outputs, allowing for multiple ready-to-use looks for your material.",
                    MessageType.None);
            }
            EditorGUILayout.BeginHorizontal();
            colorShifterObject.activeColorSetIndex = EditorGUILayout.Popup(colorShifterObject.activeColorSetIndex, colorSetOptions);
          
            if (GUILayout.Button("Duplicate Color Set"))
            {
                Undo.RecordObject(colorShifterObject, "Copy Color Set");
                CopyColorSet();
            }
            if (GUILayout.Button("Delete"))
            {
                if (EditorUtility.DisplayDialog("Delete " + ActiveColorSet().name + "",
                    "Do you really want to delete this color set?", "Yes", "Cancel"))
                {
                    Undo.RecordObject(colorShifterObject, "Delete Color Set");
                    DeleteActiveColorSet();
                }
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Create New Color Set"))
            {
                Undo.RecordObject(colorShifterObject, "Create new color set");
                CreateColorSet();
            }
            /*
            if (GUILayout.Button("Export all as PNG"))
            {
                ExportAllColorSets();
            }
*/
            if (GUILayout.Button("Export as PNG"))
            {
                ExportActiveColorSet();
            }
            EditorGUILayout.EndHorizontal();
            
            
            

            // ---------------------------------------
            // DISPLAY COLORS HEADER
            // ---------------------------------------
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.Space();
            if (EditorPrefs.GetBool("ColorShifter_ShowHelp"))
            {
                EditorGUILayout.HelpBox(
                    "Each of the active colors are displayed below the name of this color set. Each color requires a Color value for the Color ID, and a Hue, Saturation, and Value for the output color.",
                    MessageType.None);
            }
            Undo.RecordObject(colorShifterObject, "Change Color Set Name");
            ActiveColorSet().name = EditorGUILayout.TextField("Color set name", ActiveColorSet().name);
            if (ActiveColorSet().name == "")
            {
                ActiveColorSet().name = "Unnamed Color Set " + colorShifterObject.activeColorSetIndex;
            }
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Expand All"))
            {
                //Undo.RecordObject(colorShifterObject, "Expand All");
                ExpandAll(true);
            }
            if (GUILayout.Button("Collapse All"))
            {
                //Undo.RecordObject(colorShifterObject, "Collapse All");
                ExpandAll(false);
            }

            if (GUILayout.Button("Sort by name"))
            {
                Undo.RecordObject(colorShifterObject, "Sort by name");
                ActiveColorSet().SortThis();
                //ActiveColorSet().colorShifterItems.Sort();
            }
            
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            
            GUI.backgroundColor = EditorPrefs.GetBool("ColorShifter_ShowCopied") ? Color.white : Color.black;
            if (GUILayout.Button($"{(EditorPrefs.GetBool("ColorShifter_ShowCopied") ? "Hide" : "Show")} Copying Colors"))
            {
                Undo.RecordObject(colorShifterObject, "Show/Hide Copying Colors");
                EditorPrefs.SetBool("ColorShifter_ShowCopied", !EditorPrefs.GetBool("ColorShifter_ShowCopied"));
            }
            
            GUI.backgroundColor = EditorPrefs.GetBool("ColorShifter_ShowSkipped") ? Color.white : Color.black;
            if (GUILayout.Button($"{(EditorPrefs.GetBool("ColorShifter_ShowSkipped") ? "Hide" : "Show")} Skipped Colors"))
            {
                Undo.RecordObject(colorShifterObject, "Show/Hide Skipped Colors");
                EditorPrefs.SetBool("ColorShifter_ShowSkipped", !EditorPrefs.GetBool("ColorShifter_ShowSkipped"));
            }
            
            GUI.backgroundColor = EditorPrefs.GetBool("ColorShifter_ShowHidden") ? Color.white : Color.black;
            if (GUILayout.Button($"{(EditorPrefs.GetBool("ColorShifter_ShowHidden") ? "Hide" : "Show")} Hidden Colors"))
            {
                Undo.RecordObject(colorShifterObject, "Show/Hide Hidden Colors");
                EditorPrefs.SetBool("ColorShifter_ShowHidden", !EditorPrefs.GetBool("ColorShifter_ShowHidden"));
            }

            GUI.backgroundColor = Color.white;
            
            EditorGUILayout.EndHorizontal();

            ShowAllColors();
            ShowHelp();
            
            EditorGUILayout.EndScrollView();
            colorShifterObject.SetActiveColorSet();
            
            EditorUtility.SetDirty(colorShifterObject);
        }

        private void ShowHelp()
        {
            if (!EditorPrefs.GetBool("ColorShifter_ShowHelp")) return;
            
            EditorGUILayout.HelpBox(
                "COLOR ID DEFAULT VALUES\nWhile you can set Color ID (RGB) values for all 49 colors yourself, the extension defaults to these colors:\n\n" +
                "Color 0: 255,0,0\n" +
                "Color 1: 0,255,0\n" +
                "Color 2: 0,0,255\n" +
                "Color 3: 255,255,0\n" +
                "Color 4: 255,0,255\n" +
                "Color 5: 0,255,255\n" +
                "Color 6: 255,128,0\n" +
                "Color 7: 255,0,128\n" +
                "Color 8: 128,255,0\n" +
                "Color 9: 0,255,128\n" +
                "Color 10: 128,0,255\n" +
                "Color 11: 0,128,255\n" +
                "Color 12: 255,128,128\n" +
                "Color 13: 128,255,128\n" +
                "Color 14: 128,128,255\n" +
                "Color 15: 255,255,128\n" +
                "Color 16: 255,128,255\n" +
                "Color 17: 128,255,255\n" +
                "Color 18: 100,0,0\n" +
                "Color 19: 0,100,0\n" +
                "Color 20: 0,0,100\n" +
                "Color 21: 100,100,0\n" +
                "Color 22: 0,100,100\n" +
                "Color 23: 100,0,100\n" +
                "Color 24: 50,0,0\n" +
                "Color 25: 0,50,0\n" +
                "Color 26: 0,0,50\n" +
                "Color 27: 50,50,0\n" +
                "Color 28: 50,0,50\n" +
                "Color 29: 0,50,50\n" +
                "Color 30: 196,0,0\n" +
                "Color 31: 0,196,0\n" +
                "Color 32: 0,0,196\n" +
                "Color 33: 196,196,0\n" +
                "Color 34: 196,0,196\n" +
                "Color 35: 0,196,196\n" +
                "Color 36: 196,50,50\n" +
                "Color 37: 50,196,50\n" +
                "Color 38: 50,50,196\n" +
                "Color 39: 196,196,50\n" +
                "Color 40: 196,50,196\n" +
                "Color 41: 50,196,196\n" +
                "Color 42: 196,0,96\n" +
                "Color 43: 196,96,0\n" +
                "Color 44: 96,0,196\n" +
                "Color 45: 0,96,196\n" +
                "Color 46: 128,128,128\n" +
                "Color 47: 0,0,0\n" +
                "Color 48: 255,255,255", MessageType.None);
            
        }

        private void ShowAllColors()
        {
            // ---------------------------------------
            // DISPLAY COLORS ETC
            // ---------------------------------------
            for (int c = 0; c < colorShifterObject.activeColors; c++)
            {
                var colorItem = ActiveColorSet().colorShifterItems[c];

                // Skip if this can be hidden and we are not showing hidden colors
                if (!EditorPrefs.GetBool("ColorShifter_ShowHidden") && 
                    colorItem.hidden && !colorItem.isChild &&
                    colorItem.colorItemChildren.Count == 0) continue;
                
                // Skip if this is copied and we're not showing those
                if (!EditorPrefs.GetBool("ColorShifter_ShowCopied") &&
                    colorItem.isChild) continue;
                
                // Skip if this is skipped and we're not showing those
                if (!EditorPrefs.GetBool("ColorShifter_ShowSkipped") &&
                    (colorItem.skipped || colorItem.isChild && ActiveColorSet().GetColorItem(colorItem.parentName).skipped)) continue;

                if (colorItem.isOn && colorItem.isChild)
                    DisplayChildOpen(colorItem, c);
                else if (!colorItem.isOn && colorItem.isChild)
                    DisplayChildClosed(colorItem, c);
                else if (colorItem.isOn && !colorItem.isChild)
                    DisplayItemOpen(colorItem, c);
                else if (!colorItem.isOn && !colorItem.isChild)
                    DisplayItemClosed(colorItem, c);
            }
        }

        /// <summary>
        /// This is used to update the links. January 9 2022. The old version uses int to keep track of parents and
        /// children, which messes things up when the list is ordered by name. This will run automatically whenever a
        /// object is viewed, so from here on, it should fix the issue.
        /// </summary>
        private void UpdateLinks()
        {
            foreach (ColorShifterColorSet colorSet in colorShifterObject.colorSets)
                colorSet.UpdateLinks();
        }

        // ---------------------------------------
        // PRIVATE METHODS
        // ---------------------------------------
        private void SetShader()
        {
            colorShifterObject.material.shader = Shader.Find(_shaderName);
        }
        
        private void CreateColorSet()
        {
            colorShifterObject.colorSets.Add(new ColorShifterColorSet());
            int newIndex = colorShifterObject.colorSets.Count - 1;
            colorShifterObject.colorSets[newIndex].SetDefaultValues();
            colorShifterObject.activeColorSetIndex = newIndex;
            colorShifterObject.colorSets[newIndex].name = "New Color Set " + newIndex;
        }

        private void DeleteActiveColorSet()
        {
            if (colorShifterObject.colorSets.Count == 1)
            {
                Debug.LogWarning("You can't delete the last color set.");
                return;
            }

            int newIndex = colorShifterObject.activeColorSetIndex - 1;
            if (newIndex < 0)
                newIndex = 0;

            colorShifterObject.colorSets.RemoveAt(colorShifterObject.activeColorSetIndex);
            colorShifterObject.activeColorSetIndex = newIndex;
        }
        
        private void SetColorSetOptions()
        {
            colorSetOptions = new string[colorShifterObject.colorSets.Count];
            for (int i = 0; i < colorShifterObject.colorSets.Count; i++)
            {
                colorSetOptions[i] = colorShifterObject.colorSets[i].name;
            }
        }

        private void CopyColorSet()
        {
            ColorShifterColorSet copyFromColorShifterColorSet = ActiveColorSet();
            var newColorSet = copyFromColorShifterColorSet.Clone();
            newColorSet.name = copyFromColorShifterColorSet.name + " Copy " + colorShifterObject.activeColorSetIndex;
            colorShifterObject.colorSets.Add(newColorSet);

            colorShifterObject.activeColorSetIndex = colorShifterObject.colorSets.Count - 1;

            /*
            CreateColorSet();
            ColorShifterColorSet copyToColorShifterColorSet = ActiveColorSet();

            for (int i = 0; i < copyFromColorShifterColorSet.colorShifterItems.Count; i++)
            {
                ColorShifterColorItem copyTo = copyToColorShifterColorSet.colorShifterItems[i];
                ColorShifterColorItem copyFrom = copyFromColorShifterColorSet.colorShifterItems[i];

                copyTo.hidden = copyFrom.hidden;
                
                copyTo.color = copyFrom.color;
                copyTo.name = copyFrom.name;
                copyTo.hue = copyFrom.hue;
                copyTo.saturation = copyFrom.saturation;
                copyTo.value = copyFrom.value;
                copyTo.isOn = copyFrom.isOn;
                copyTo.orderIndex = copyFrom.orderIndex;
                copyTo.shaderIndex = copyFrom.shaderIndex;
                
                for (int c = 0; c < copyFrom.children.Count; c++)
                {
                    copyTo.children.Add(copyFrom.children[c]); // THIS WILL BE REMOVED IN A FUTURE VERSION REPLACED WITH BELOW
                    copyTo.colorItemChildren.Add(copyFrom.colorItemChildren[c]);
                }

                copyTo.isChild = copyFrom.isChild;
                copyTo.testView = copyFrom.testView;
                copyTo.childHueShift = copyFrom.childHueShift;
                copyTo.childSaturationShift = copyFrom.childSaturationShift;
                copyTo.childValueShift = copyFrom.childValueShift;
                copyTo.parentIndex = copyFrom.parentIndex; // THIS WILL BE REMOVED IN A FUTURE VERSION REPLACED WITH BELOW
                copyTo.parentName = copyFrom.parentName;
            }

            copyToColorShifterColorSet.name = copyFromColorShifterColorSet.name + " Copy " + colorShifterObject.activeColorSetIndex;
            */
        }

        private void ExpandAll(bool v)
        {
            for (int i = 0; i < ActiveColorSet().colorShifterItems.Count; i++)
            {
                ActiveColorSet().colorShifterItems[i].isOn = v;
            }
        }

        private void ExportAllColorSets()
        {
            for (int i = 0; i < colorShifterObject.colorSets.Count; i++)
            {
                ExportColorSet(i);
            }
        }

        private void ExportActiveColorSet()
        {
            ExportColorSet(colorShifterObject.activeColorSetIndex);
        }
        
        private void ExportColorSet(int index)
        {
            //int currentIndex = colorShifterObject.activeColorSetIndex;

            colorShifterObject.activeColorSetIndex = index;
            Repaint();
            
            Texture2D outputTex = new Texture2D(512, 512, TextureFormat.ARGB32, false, true);
            RenderTexture buffer = new RenderTexture(
                512, 
                512, 
                0,                            // No depth/stencil buffer
                RenderTextureFormat.ARGB32//RenderTextureReadWrite.Linear // No sRGB conversions
            );
            
            
            Graphics.Blit(colorShifterObject.material.GetTexture("_MainTex"), buffer, colorShifterObject.material, 2);

            //RenderTexture.active = colorShifterObject.renderTexture;           // If not using a scene camera
            outputTex.ReadPixels(
                new Rect(0, 0, buffer.width, buffer.height), // Capture the whole texture
                0, 0,                          // Write starting at the top-left texel
                false                          // No mipmaps
            );
            
           

            System.IO.File.WriteAllBytes( colorShifterObject.exportPath + "/" + ActiveColorSet().name + ".png", outputTex.EncodeToPNG());


            //colorShifterObject.activeColorSetIndex = currentIndex;
            AssetDatabase.Refresh();
        }

        private void DisplayItemClosed(ColorShifterColorItem colorShifterColorItem, int index)
        {
            GUI.contentColor = colorShifterColorItem.hidden ? Color.black : colorShifterColorItem.skipped ? Color.grey : Color.white;
            EditorGUILayout.BeginHorizontal();
            colorShifterColorItem.isOn = EditorGUILayout.Foldout(colorShifterColorItem.isOn, $"{colorShifterColorItem.name}");
            Color.RGBToHSV(EditorGUILayout.ColorField("", Color.HSVToRGB(colorShifterColorItem.hue, colorShifterColorItem.saturation, colorShifterColorItem.value)), out colorShifterColorItem.hue, out colorShifterColorItem.saturation, out colorShifterColorItem.value);
                    
            if (GUILayout.Button(colorShifterColorItem.testView ? "Revert" : "Test"))
            {
                colorShifterColorItem.testView = !colorShifterColorItem.testView;
            }
            EditorGUILayout.EndHorizontal();
            GUI.contentColor = Color.white;
        }

        private void DisplayItemOpen(ColorShifterColorItem colorShifterColorItem, int index)
        {
            GUI.contentColor = colorShifterColorItem.hidden ? Color.black : colorShifterColorItem.skipped ? Color.grey : Color.white;
            GUI.backgroundColor = colorShifterColorItem.hidden ? Color.black : colorShifterColorItem.skipped ? Color.grey : Color.white;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            //Undo.RecordObject(colorShifterObject, "Update Item isOn value");
            colorShifterColorItem.isOn = EditorGUILayout.Foldout(colorShifterColorItem.isOn, $"{colorShifterColorItem.name}");
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            var tempName = colorShifterColorItem.name;
            Undo.RecordObject(colorShifterObject, "Change Name");
            colorShifterColorItem.name = EditorGUILayout.TextField("Name", colorShifterColorItem.name);
            if (tempName != colorShifterColorItem.name)
                colorShifterColorItem.UpdateChildrenNames();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            //EditorGUILayout.LabelField("COLOR ID VALUE");
            colorShifterColorItem.color = EditorGUILayout.ColorField(new GUIContent($"ColorID color {symbolInfo}", "This is the lookup color on the Color ID texture. Generally do not change this!!"), colorShifterColorItem.color);
            colorShifterColorItem.color.r = (EditorGUILayout.Slider("R",  255 * colorShifterColorItem.color.r, 0,255) / 255);
            colorShifterColorItem.color.g = (EditorGUILayout.Slider("G",  255 * colorShifterColorItem.color.g, 0,255) / 255);
            colorShifterColorItem.color.b = (EditorGUILayout.Slider("B",  255 * colorShifterColorItem.color.b, 0,255) / 255);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            //EditorGUILayout.LabelField("FINAL COLOR VALUE");
            //Undo.RecordObject(colorShifterObject, "Change Color");
            Color.RGBToHSV(EditorGUILayout.ColorField(new GUIContent($"Output Color {symbolInfo}","This is the final color of this portion of the texture."), Color.HSVToRGB(colorShifterColorItem.hue, colorShifterColorItem.saturation, colorShifterColorItem.value)), out colorShifterColorItem.hue, out colorShifterColorItem.saturation, out colorShifterColorItem.value);
            colorShifterColorItem.hue = EditorGUILayout.Slider("Hue", colorShifterColorItem.hue, 0f,1f);
            colorShifterColorItem.saturation = EditorGUILayout.Slider("Saturation", colorShifterColorItem.saturation, 0f,1f);
            colorShifterColorItem.value = EditorGUILayout.Slider("Value", colorShifterColorItem.value, 0f,1f);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            childNames = GetChildNames();
            childSelectIndex = EditorGUILayout.Popup(childSelectIndex, childNames);
            
            if (GUILayout.Button("Control This"))
            {
                AddParentLink(colorShifterColorItem, childNames[childSelectIndex]);
            }
            EditorGUILayout.EndHorizontal();

            if (colorShifterColorItem.colorItemChildren.Count > 0)
            {
                EditorGUILayout.LabelField(new GUIContent($"This color controls these items: {symbolInfo}", "The colors below will match this color."));
                for (int c = 0; c < colorShifterColorItem.colorItemChildren.Count; c++)
                {
                    var childItem = colorShifterColorItem.colorItemChildren[c];
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField($"{childItem.name} {childItem.skipped}");
                    if (GUILayout.Button("Remove Link"))
                    {
                        RemoveParentLink(childItem, colorShifterColorItem);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            if (EditorPrefs.GetBool("ColorShifter_ShowFull"))
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);

                EditorGUILayout.LabelField("Full Details from Scriptable Object", EditorStyles.boldLabel);

                Undo.RecordObject(colorShifterObject, "Change Name");
                colorShifterColorItem.name = EditorGUILayout.TextField("Name", colorShifterColorItem.name);
                colorShifterColorItem.shaderIndex = EditorGUILayout.IntField("Shader Index", colorShifterColorItem.shaderIndex);
                colorShifterColorItem.orderIndex = EditorGUILayout.IntField("Order Index", colorShifterColorItem.orderIndex);
                colorShifterColorItem.color = EditorGUILayout.ColorField("Color ID color", colorShifterColorItem.color);
                colorShifterColorItem.hue = EditorGUILayout.FloatField("Hue", colorShifterColorItem.hue);
                colorShifterColorItem.saturation = EditorGUILayout.FloatField("Saturation", colorShifterColorItem.saturation);
                colorShifterColorItem.value = EditorGUILayout.FloatField("Lightness", colorShifterColorItem.value);
                
                EditorGUILayout.EndVertical();
            }
            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.white;
            //var tempBool = colorShifterColorItem.skipped;
            colorShifterColorItem.SetSkipped(EditorGUILayout.ToggleLeft(
                new GUIContent($"Skip This Color {symbolInfo}",
                    "If true, this color will be skipped when the Color Set is activated."),
                colorShifterColorItem.skipped));

            /*
            if (tempBool != colorShifterColorItem.skipped)
            {
                Debug.Log($"Should update {colorShifterColorItem.colorItemChildren.Count} chilren to {colorShifterColorItem.skipped}");
                colorShifterColorItem.SetChildrenSkipped(colorShifterColorItem.skipped);
            }
            */
            
            colorShifterColorItem.hidden = EditorGUILayout.ToggleLeft(new GUIContent($"Hide This Color {symbolInfo}", "Hide this color. Generally this is for colors that aren't used on this texture."), colorShifterColorItem.hidden);
            
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            
        }

        private void DisplayChildClosed(ColorShifterColorItem colorShifterColorItem, int index)
        {
            var skipped = ActiveColorSet().GetColorItem(colorShifterColorItem.parentName).skipped;
            GUI.contentColor = colorShifterColorItem.hidden ? Color.black : skipped ? Color.grey : Color.white;
            GUI.backgroundColor = colorShifterColorItem.hidden ? Color.black : skipped ? Color.grey : Color.white;
            EditorGUILayout.BeginHorizontal();
            colorShifterColorItem.isOn = EditorGUILayout.Foldout(colorShifterColorItem.isOn, $"{colorShifterColorItem.name} [{index}]");
            //ColorShifterColorItem parentColorItem = ActiveColorSet().colorShifterItems[colorShifterColorItem.parentIndex];
            ColorShifterColorItem parentColorItem = GetColorShifterItem(colorShifterColorItem.parentName);
            EditorGUILayout.LabelField($"This color copies {colorShifterColorItem.parentName}");
            if (GUILayout.Button("Remove Link"))
            {
                RemoveParentLink(colorShifterColorItem, parentColorItem);
            }
            EditorGUILayout.EndHorizontal();
            GUI.contentColor = Color.white;
        }

        private void DisplayChildOpen(ColorShifterColorItem colorShifterColorItem, int index)
        {
            var skipped = ActiveColorSet().GetColorItem(colorShifterColorItem.parentName).skipped;
            GUI.contentColor = colorShifterColorItem.hidden ? Color.black : skipped ? Color.grey : Color.white;
            GUI.backgroundColor = colorShifterColorItem.hidden ? Color.black : skipped ? Color.grey : Color.white;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            colorShifterColorItem.isOn = EditorGUILayout.Foldout(colorShifterColorItem.isOn, $"{colorShifterColorItem.name} [{index}]");
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();
            Undo.RecordObject(colorShifterObject, "Change Name");
            colorShifterColorItem.name = EditorGUILayout.TextField("Name", colorShifterColorItem.name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();
            ColorShifterColorItem parentColorItem = GetColorShifterItem(colorShifterColorItem.parentName);
            //ColorShifterColorItem parentColorItem = ActiveColorSet().colorShifterItems[colorShifterColorItem.parentIndex];
            EditorGUILayout.LabelField($"This color copies {colorShifterColorItem.parentName} {colorShifterColorItem.skipped}");
            if (GUILayout.Button("Remove Link"))
            {
                RemoveParentLink(colorShifterColorItem, parentColorItem);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.LabelField("Shift the final color by the values below");
            colorShifterColorItem.childHueShift = EditorGUILayout.Slider("Hue Shift", colorShifterColorItem.childHueShift, -1f,1f);
            colorShifterColorItem.childSaturationShift = EditorGUILayout.Slider("Saturation Shift", colorShifterColorItem.childSaturationShift, -1f,1f);
            colorShifterColorItem.childValueShift = EditorGUILayout.Slider("Value Shift", colorShifterColorItem.childValueShift, -1f,1f);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            GUI.contentColor = Color.white;
            GUI.backgroundColor = Color.white;
        }

        private void RemoveParentLink(ColorShifterColorItem childItem, ColorShifterColorItem parentColorItem)
        {
            childItem.isChild = false;
            parentColorItem.RemoveChild(childItem);
            //parentColorItem.colorItemChildren.Remove(childItem);
            
            /*
            for (int i = 0; i < parentColorItem.children.Count; i++)
            {
                if (parentColorItem.children[i] == GetColorShifterItemIndex(childItem))
                {
                    parentColorItem.children.RemoveAt(i);
                    childItem.isChild = false;
                }
            }
            */
        }

        private void AddParentLink(ColorShifterColorItem parentColorItem, string childName)
        {
            ColorShifterColorItem childColorItem = GetColorShifterItem(childName);
            if (childColorItem == parentColorItem)
            {
                Debug.Log("Error: Can't add a parent as its own child");
                return;
            }
            if (childColorItem.isChild)
            {
                Debug.Log($"Error: {childColorItem.name} is already a child of {childColorItem.parentName}");
                return;
            }
            
            if (childColorItem.colorItemChildren.Count > 0)
            {
                Debug.Log($"Error: {childColorItem.name} is already the parent of {childColorItem.colorItemChildren.Count} children");
                return;
            }
            parentColorItem.children.Add(GetColorShifterItemIndex(childColorItem)); // TO BE DEPRECATED
            parentColorItem.colorItemChildren.Add(childColorItem);
            childColorItem.skipped = parentColorItem.skipped;
            childColorItem.isChild = true;
            childColorItem.parentIndex = GetColorShifterItemIndex(parentColorItem); // TO BE DEPRECATED
            childColorItem.parentName = parentColorItem.name;
        }

        private int GetColorShifterItemIndex(ColorShifterColorItem colorShifterColorItem)
        {
            for (int i = 0; i < ActiveColorSet().colorShifterItems.Count; i++)
            {
                if (ActiveColorSet().colorShifterItems[i] == colorShifterColorItem)
                    return i;
            }

            return -1;
        }
        
        private ColorShifterColorItem GetColorShifterItem(string itemName, ColorShifterColorSet colorSet = null)
        {
            if (colorSet == null)
                colorSet = ActiveColorSet();
            
            for (int i = 0; i < colorSet.colorShifterItems.Count; i++)
            {
                if (colorSet.colorShifterItems[i].name == itemName)
                    return colorSet.colorShifterItems[i];
            }

            return null;
        }

        private string[] GetChildNames()
        {
            string[] allChildNames = new string[ActiveColorSet().colorShifterItems.Count];

            for (int i = 0; i < ActiveColorSet().colorShifterItems.Count; i++)
            {
                if (ActiveColorSet().colorShifterItems[i].isChild)
                    continue;
                allChildNames[i] = ActiveColorSet().colorShifterItems[i].name;
            }

            return allChildNames;
        }
        
        public static string symbolInfo = "ⓘ";
        public static string symbolX = "✘";
        public static string symbolCheck = "✔";
        public static string symbolCheckSquare = "☑";
        public static string symbolDollar = "$";
        public static string symbolCent = "¢";
        public static string symbolCarrotRight = "‣";
        public static string symbolCarrotLeft = "◄";
        public static string symbolCarrotUp = "▲";
        public static string symbolCarrotDown = "▼";
        public static string symbolDash = "⁃";
        public static string symbolBulletClosed = "⦿";
        public static string symbolBulletOpen = "⦾";
        public static string symbolHeartClosed = "♥";
        public static string symbolHeartOpen = "♡";
        public static string symbolStarClosed = "★";
        public static string symbolStarOpen = "☆";
        public static string symbolArrowUp = "↑";
        public static string symbolArrowDown = "↓";
        public static string symbolRandom = "↬";
        public static string symbolMusic = "♫";
        public static string symbolImportant = "‼";
    }
}
