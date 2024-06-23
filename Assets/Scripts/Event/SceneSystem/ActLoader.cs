using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace SceneSystem
{
    public class ActLoader : MonoBehaviour
    {
        public string actName;
        private ActData actData;
        private int currentSceneIndex;

        public Image character1Image;
        public Image character2Image;
        public Text sceneText;
        public Text characterSpeakingText;
        public Button choice1Button;
        public Button choice2Button;
        public Button nextButton;

        void Start()
        {
            LoadAct(actName);
            DisplayScene(currentSceneIndex);
        }

        private void LoadAct(string name)
        {
            string path = "Assets/ActsData/" + name + ".json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                actData = JsonUtility.FromJson<ActData>(json);
            }
            else
            {
                Debug.LogError("Act file not found: " + path);
            }
        }

        private void DisplayScene(int index)
        {
            if (index < 0 || index >= actData.scenes.Count) return;
            SceneData sceneData = actData.scenes[index];
            currentSceneIndex = index;

            sceneText.text = sceneData.text;

            character1Image.sprite = sceneData.character1Image;
            character2Image.sprite = sceneData.character2Image;

            if (sceneData.speakingCharacter == 0)
            {
                characterSpeakingText.text = "Character 1 is speaking";
            }
            else
            {
                characterSpeakingText.text = "Character 2 is speaking";
            }

            if (sceneData.isChoiceScene)
            {
                choice1Button.gameObject.SetActive(true);
                choice2Button.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);/*

                choice1Button.GetComponentInChildren<Text>().text = sceneData.choice1Text;
                choice2Button.GetComponentInChildren<Text>().text = sceneData.choice2Text;*/

                choice1Button.onClick.RemoveAllListeners();
                choice1Button.onClick.AddListener(() => GoToScene(actData.connections.Find(c => c.fromSceneIndex == currentSceneIndex && c.isChoice1).toSceneIndex));

                choice2Button.onClick.RemoveAllListeners();
                choice2Button.onClick.AddListener(() => GoToScene(actData.connections.Find(c => c.fromSceneIndex == currentSceneIndex && !c.isChoice1).toSceneIndex));
            }
            else
            {
                choice1Button.gameObject.SetActive(false);
                choice2Button.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);

                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() => GoToNextScene());
            }
        }

        private void GoToScene(int index)
        {
            DisplayScene(index);
        }

        private void GoToNextScene()
        {
            var nextConnection = actData.connections.Find(c => c.fromSceneIndex == currentSceneIndex && !c.isChoice1);
            if (nextConnection != null)
            {
                DisplayScene(nextConnection.toSceneIndex);
            }
            else
            {
                Debug.Log("End of Act");
            }
        }
    }
}
