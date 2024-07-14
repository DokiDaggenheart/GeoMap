using UnityEngine;
using UnityEditor;

namespace SceneSystem
{
    public class ActEditorWindow : EditorWindow
    {
        private ActData actData = new ActData();
        private Vector2 scrollPos;
        private SceneData selectedScene;
        private Vector2 mousePosition;
        private float hSbarValue;
        private float vSbarValue = 0;

        [MenuItem("Window/Act Editor")]
        public static void ShowWindow()
        {
            GetWindow<ActEditorWindow>("Act Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Act Editor for Visual Novel", EditorStyles.boldLabel);

            actData.name = EditorGUILayout.TextField("Act Name", actData.name);
            actData.backgroundImage = (Sprite)EditorGUILayout.ObjectField("Background Image", actData.backgroundImage, typeof(Sprite), false);
            EditorGUILayout.BeginHorizontal();
            actData.firstCharacterName = EditorGUILayout.TextField("First Character Name", actData.firstCharacterName);
            actData.secondCharacterName = EditorGUILayout.TextField("Second Character Name", actData.secondCharacterName);
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("Add Scene"))
            {
                actData.scenes.Add(new SceneData());
            }

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            BeginWindows();
            for (int i = 0; i < actData.scenes.Count; i++)
            {
                var scene = actData.scenes[i];
                var window = GUILayout.Window(i, new Rect((-1800 + i * 240) + (240 * hSbarValue), 30 + (180 * vSbarValue), 200, 250), id => DrawSceneWindow(id, scene), "Scene " + (i + 1));
            }
            EndWindows();

            hSbarValue = (GUILayout.HorizontalScrollbar(hSbarValue, 1.0f, -10.0f, 10.0f) * -1);

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Scroll Up"))
            {
                ScrollUp();
            }
            if (GUILayout.Button("Scroll Down"))
            {
                ScrollDown();
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save Act"))
            {
                SaveAct();
            }
            if (GUILayout.Button("LoadAct"))
            {
                LoadAct("Assets/ActsData/" + actData.name + ".json"); ;
            }
            if (GUILayout.Button("Delete"))
            {
                DeleteAct();
            }
            HandleEvents();
        }

        private void DrawSceneWindow(int id, SceneData scene)
        {
            scene.name = EditorGUILayout.TextField("Scene Name", scene.name);
            scene.text = EditorGUILayout.TextArea(scene.text, EditorStyles.textArea, GUILayout.Width(190), GUILayout.ExpandHeight(true));
            scene.character1Image = (Sprite)EditorGUILayout.ObjectField("Character 1 Image", scene.character1Image, typeof(Sprite), false);
            scene.character2Image = (Sprite)EditorGUILayout.ObjectField("Character 2 Image", scene.character2Image, typeof(Sprite), false);
            scene.speakingCharacter = EditorGUILayout.Popup("Speaking Character", scene.speakingCharacter, new string[] { "Character 1", "Character 2" });
            scene.isChoiceScene = EditorGUILayout.Toggle("Is Choice Scene", scene.isChoiceScene);
            scene.isEndScene = EditorGUILayout.Toggle("Is End Scene", scene.isEndScene);
            if (scene.isChoiceScene)
            {
                scene.choice1Text = EditorGUILayout.TextField("Choice 1 Text", scene.choice1Text);
                EditorGUILayout.LabelField("Choice1 Scene");
                try
                {
                    EditorGUILayout.LabelField(scene.choice1Scene);
                }
                catch
                {
                    EditorGUILayout.LabelField("choice1 Scene Name");
                }
                if (GUILayout.Button("SetScene1"))
                {
                    scene.choice1Scene = selectedScene.name;
                    selectedScene = null;
                }

                scene.choice2Text = EditorGUILayout.TextField("Choice 2 Text", scene.choice2Text);
                EditorGUILayout.LabelField("Choice2 Scene");
                try
                {
                    EditorGUILayout.LabelField(scene.choice2Scene);
                }
                catch
                {
                    EditorGUILayout.LabelField("choice2 Scene Name");
                }
                if (GUILayout.Button("SetScene2"))
                {
                    scene.choice2Scene = selectedScene.name;
                    selectedScene = null;
                }
            }
            else
            {
                EditorGUILayout.LabelField("NextScene");
                try
                {
                    EditorGUILayout.LabelField(scene.nextScene);
                }
                catch
                {
                    EditorGUILayout.LabelField("choice2 Scene Name");
                }
                if (GUILayout.Button("SetScene"))
                {
                    scene.nextScene = selectedScene.name;
                    selectedScene = null;
                }
            }

            if (GUILayout.Button("Select"))
            {
                selectedScene = scene;
                Debug.Log("new scene selected");
            }


            if (GUILayout.Button("Delete"))
            {
                RemoveScene(scene);
            }
            GUI.DragWindow();
            
        }

        private void HandleEvents()
        {
            mousePosition = Event.current.mousePosition;
        }

        private void SaveAct()
        {
            string path = "Assets/ActsData/" + actData.name + ".json";
            string json = JsonUtility.ToJson(actData);
            System.IO.File.WriteAllText(path, json);
            AssetDatabase.Refresh();
            Debug.Log("Act saved: " + path);
        }

        private void DeleteAct()
        {
            actData = new ActData();
        }

        public void LoadAct(string path)
        {
            string json = System.IO.File.ReadAllText(path);
            actData = JsonUtility.FromJson<ActData>(json);
        }

        private void RemoveScene(SceneData scene)
        {
            if (actData.scenes.Contains(scene))
            {
                actData.scenes.Remove(scene);
            }
        }

        private void ScrollUp()
        {
            vSbarValue += 0.5f;
        }
        private void ScrollDown()
        {
            vSbarValue -= 0.5f;
        }
    }
}