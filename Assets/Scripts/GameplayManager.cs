using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    
    public Text storyText;
    public Transform answersParent;
    public GameObject answerButton;
    public GameObject item1, item2, item3;

     [SerializeField]
    private GameObject EndMenu;

    [SerializeField]
    private Button restartGameButton, mainMenuButton;

    [SerializeField]
    private Text pauseText, victoryText;


    public class StoryNode {
        public string story;

        // Array de posibles respuestas
        public string[] answers;

        // Aquí van las instancias colgantes:
        public StoryNode[] nextNode;

        public bool isFinal = false;

        public delegate void NodeVisited();
        public NodeVisited nodeVisited;

        public delegate void FoundPicture();
        public FoundPicture foundPicture;

        public delegate void FoundKey();
        public FoundKey foundKey;

        public delegate void FoundTeddybear();
        public FoundTeddybear foundTeddybear;

    }

    // Atributo para almacenar la pantalla activa ahora mismo
    private StoryNode current;

    // Metodo que se encargará de pintar en pantalla un Nodo
    private void Repaint() {

        if (current.foundPicture != null)
        {
                item1.gameObject.SetActive(true);
        }

        if (current.foundKey != null)
        {
            item2.gameObject.SetActive(true);
        }

        if (current.foundTeddybear != null)
        {
            item3.gameObject.SetActive(true);
        }

        if (current.nodeVisited != null)
        {
            current.nodeVisited();
        }

        storyText.text += "\n" + current.story;

        //nos cargamos los botones que existan en el padre answersParent
        foreach (Transform child in answersParent.transform) {
            Destroy(child.gameObject);
        }

        bool isLeft = true;
        float height = 35;
        int index = 0;
        foreach (string answer in current.answers) {
            GameObject answerButtonCopy = Instantiate(answerButton);
            answerButtonCopy.transform.SetParent(answersParent, false);
            float x = answerButtonCopy.GetComponent<RectTransform>().rect.x * 1.1f;

            float newX;

            if (isLeft)
            {
                newX = x;
            }
            else
            {
                newX = -x;
            }

            answerButtonCopy.GetComponent<RectTransform>().localPosition = new Vector3(newX, height,0);

            if (!isLeft) height += answerButtonCopy.GetComponent<RectTransform>().rect.y * 2.5f;

            isLeft = !isLeft;

            FillListener(answerButtonCopy.GetComponent<Button>(), index);
            answerButtonCopy.GetComponentInChildren<Text>().text = answer;

            index++;
        }
    }

    void FillListener(Button button, int index) {
        button.onClick.AddListener(() => 
        {
            AnswerSelected(index);
            AudioController.instance.playButtonClickSound();
        });
    }

    void Awake() {
        item1.gameObject.SetActive(false);
        item2.gameObject.SetActive(false);
        item3.gameObject.SetActive(false);
    }

    void Start() {

        EndMenu.gameObject.SetActive(false);

        // Llenamos el mapa de pantallas
        // Recibimos el nodo raiz del arbol de pantallas
        current = StoryFiller.FillStory();

        //Creamos el texto en blanco
        storyText.text = "";

        Repaint();


    }

    void AnswerSelected(int index)
    {
        print(index);

        storyText.text += "\n" + "<color=#939196>" + "<i>" + current.answers[index] + "</i>" + "</color>";
        if (!current.isFinal)
        {
            current = current.nextNode[index];
            Repaint();
        }

        if (current.isFinal) {
            ShowVictoryMenu();
            restartGameButton.onClick.AddListener(() =>
            {
               AudioController.instance.playButtonClickSound();
            });
            mainMenuButton.onClick.AddListener(() =>
            {
                AudioController.instance.playButtonClickSound();
            });
        }
    }


    public void StartGame()
    {
        SceneFader.instance.LoadLevel("Gameplay");
        AudioController.instance.audioSource.Play();
    }

    public void GoToMainMenu()
    {
        SceneFader.instance.LoadLevel("MainMenu");
        AudioController.instance.audioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    void ShowPauseMenu()
    {
        if (!EndMenu.activeSelf)
        {
            EndMenu.gameObject.SetActive(true);
            victoryText.gameObject.SetActive(false);
            pauseText.gameObject.SetActive(true);
            AudioController.instance.audioSource.Stop();

            restartGameButton.onClick.RemoveAllListeners();
            restartGameButton.onClick.AddListener(() =>
            {
                ResumeGame();
                AudioController.instance.playButtonClickSound();
            });

            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(() =>
            {
                GoToMainMenu();
                AudioController.instance.playButtonClickSound();
            });

        }
    }

    void ShowVictoryMenu()
    {
        if (!EndMenu.activeSelf)
        {
            EndMenu.gameObject.SetActive(true);
            victoryText.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(false);
            AudioController.instance.audioSource.Stop();

            restartGameButton.onClick.RemoveAllListeners();
            restartGameButton.onClick.AddListener(() => 
            {
                StartGame();
                AudioController.instance.playButtonClickSound();
            });

            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(() =>
            {
                GoToMainMenu();
                AudioController.instance.playButtonClickSound();
            });
        }
    }

    void ResumeGame()
    {
        EndMenu.gameObject.SetActive(false);
        AudioController.instance.audioSource.Play();
    }



}//GameplayManager




