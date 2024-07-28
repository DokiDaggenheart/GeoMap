using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

namespace SceneSystem
{
    public class ActLoader : MonoBehaviour
    {
        public string actName;
        private ActData actData;
        private int currentSceneIndex;
        [Inject] private MovementModel _movementModel;
        [Inject] private InventorySystem _inventorySystem;
        [Inject] private EnergyModel _energyModel;

        public Image backgroundImage;
        public Image character1Image;
        public Image character2Image;
        public TextMeshProUGUI sceneText;
        public TextMeshProUGUI character1name;
        public TextMeshProUGUI character2name;
        private int characterSpeaking;
        public Button choice1Button;
        public Button choice2Button;
        public Button nextButton;

        void Start()
        {
            LoadAct(actName);
            DisplayScene(currentSceneIndex);
        }

        public void LoadAct(string name)
        {
            string path = "Assets/ActsData/" + name + ".json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                actData = JsonUtility.FromJson<ActData>(json);
            }
            else
            {
                Debug.LogError("‘айл акта не найден: " + path);
            }
        }

        private void DisplayScene(int index)
        {
            if (index < 0 || index >= actData.scenes.Count) return;
            SceneData sceneData = actData.scenes[index];
            currentSceneIndex = index;

            sceneText.text = sceneData.text;
            backgroundImage.sprite = actData.backgroundImage;

            if(sceneData.character1Image == null)
                character1Image.color = new Color(255, 255, 255, 0);
            else
            {
                character1Image.color = new Color(255, 255, 255, 255);
                character1Image.sprite = sceneData.character1Image;
            }

            if (sceneData.character2Image == null)
                character2Image.color = new Color(255, 255, 255, 0);
            else
            {
                character2Image.color = new Color(255, 255, 255, 255);
                character2Image.sprite = sceneData.character2Image;
            }

            characterSpeaking = sceneData.speakingCharacter;
            if (actData.firstCharacterName != "NO")
                character1name.text = actData.firstCharacterName;
            else
                character1name.text = " ";


            if (actData.secondCharacterName != "NO")
                character2name.text = actData.secondCharacterName;
            else
                character2name.text = " ";


            if (characterSpeaking == 0)
            {
                character1name.color = Color.yellow;
                character1name.fontSize = 42;
                character2name.color = Color.white;
                character2name.fontSize = 36;
            }
            if (characterSpeaking == 1)
            {
                character2name.color = Color.yellow;
                character2name.fontSize = 42;
                character1name.color = Color.white;
                character1name.fontSize = 36;
            }

            if (sceneData.isChoiceScene)
            {
                choice1Button.gameObject.SetActive(true);
                choice2Button.gameObject.SetActive(true);
                nextButton.gameObject.SetActive(false);

                choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = sceneData.choice1Text;
                choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = sceneData.choice2Text;

                choice1Button.onClick.RemoveAllListeners();
                choice1Button.onClick.AddListener(() => GoToScene(sceneData.choice1Scene));

                choice2Button.onClick.RemoveAllListeners();
                choice2Button.onClick.AddListener(() => GoToScene(sceneData.choice2Scene));
            }
            else
            {
                choice1Button.gameObject.SetActive(false);
                choice2Button.gameObject.SetActive(false);
                nextButton.gameObject.SetActive(true);

                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() => GoToNextScene());
            }

            if (sceneData.isEndScene)
            {
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(() => EndEvent());
            }
        }

        private void GoToScene(string sceneName)
        {
            int nextIndex = actData.scenes.FindIndex(scene => scene.name == sceneName);
            if (nextIndex >= 0)
            {
                DisplayScene(nextIndex);
            }
            else
            {
                Debug.LogError("—цена не найдена: " + sceneName);
            }
        }

        private void EndEvent()
        {
            gameObject.SetActive(false);
            _movementModel.isRiding = true;
            currentSceneIndex = 0;
            _energyModel.energy += actData.addingEnergy;
            _inventorySystem.food += actData.addingFood;
        }

        private void GoToNextScene()
        {
            string nextSceneName = actData.scenes[currentSceneIndex].nextScene;
            GoToScene(nextSceneName);
        }
    }
}