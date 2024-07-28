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
        public string nextScene;
        public bool isEndScene;
        public bool isChoiceScene;
        public string choice1Text;
        public string choice1Scene;
        public string choice2Text;
        public string choice2Scene;
        public Button deleteButton;
    }

    [System.Serializable]
    public class ActData
    {
        public string name;
        public Sprite backgroundImage;
        public string firstCharacterName;
        public string secondCharacterName;
        public int addingEnergy;
        public int addingFood;
        public List<SceneData> scenes = new List<SceneData>();
    }
}
