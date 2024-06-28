using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSystem
{
    [System.Serializable]
    public class SceneData
    {
        public string name;
        public string text;
        public Sprite character1Image;
        public Sprite character2Image;
        public int speakingCharacter;
        public SceneData nextScene;
        public bool isChoiceScene;
        public string choice1Text;
        public SceneData choice1Scene;
        public string choice2Text;
        public SceneData choice2Scene;
        public Button deleteButton;
    }
    [System.Serializable]
    public class SceneConnection
    {
        public int fromSceneIndex;
        public int toSceneIndex;
        public bool isChoice1;
    }

    [System.Serializable]
    public class ActData
    {
        public string name;
        public string firstCharacterName;
        public string secondCharacterName;
        public List<SceneData> scenes = new List<SceneData>();
        public List<SceneConnection> connections = new List<SceneConnection>();
    }
}
