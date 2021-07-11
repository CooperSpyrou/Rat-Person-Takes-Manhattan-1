using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


public class Gameplay : MonoBehaviour
{
    [System.Serializable]
    public class levels
    {
        public Sprite[] screens;
        public string[] dialogue;
        public AudioClip[] audio;
        public AudioClip bgm;
        public UnityEvent minigame;
    }
    public levels[] levelList;


    public GameObject dialogueText;
    public GameObject gameImage;

    private AudioSource _audioSource;
    private AudioSource _audioSourceBGM;

    [SerializeField] int currentLevel;
    [SerializeField] int currentScene;
    [SerializeField] int currentDialogue;


    public string minigameButton;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.GetComponent<TextMeshProUGUI>().text = levelList[currentLevel].dialogue[currentScene];
        gameImage.GetComponent<Image>().sprite = levelList[currentLevel].screens[currentScene];
        _audioSource = GetComponent<AudioSource>();
        _audioSourceBGM = GameObject.Find("BGM").GetComponent<AudioSource>();

        /*for (int i = 0; i < levelList.Length; i++)
        {
            //Debug.Log("Looking at level " + i);
            //for (int j = 0; j < levelList[i].screens.Length; j++)
            //{
            //    Debug.Log("Looking at screen " + j + " which is "+ levelList[i].screens[j]);
            //}

            //Load Sprit [0]
            //Show text
            // Do a mini game
            //Load Sprit [1]


        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || (Input.GetKeyDown(KeyCode.Space)))
        {
            if (GameObject.Find("Minigame") == null)
            {
                Next();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            NextLevel();
        }
    }



    public void Next()
    {
        // If we finish the level, load the next one
        if (currentDialogue < levelList[currentLevel].dialogue.Length-1)
        {
            currentDialogue += 1;

            if (currentDialogue == 0) { currentScene = 0; }
            if (currentDialogue == 1) { currentScene = 0; levelList[currentLevel].minigame.Invoke();  }
            if (currentDialogue == 2) { currentScene = 1; }

            UpdateLevelUI();
        }
    }



    void NextLevel()
    {
        if (currentDialogue >= levelList[currentLevel].dialogue.Length - 1)
        {
            currentLevel += 1;
            currentDialogue = 0;
            currentScene = 0;

            UpdateLevelUI();

            if (_audioSourceBGM.clip != levelList[currentLevel].bgm)
            {
                _audioSourceBGM.Stop();
            }

            if (levelList[currentLevel].bgm != null)
            {
                _audioSourceBGM.clip = levelList[currentLevel].bgm;
                _audioSourceBGM.Play();
            }
        }
    }



    void UpdateLevelUI()
    {
        dialogueText.GetComponent<TextMeshProUGUI>().text = levelList[currentLevel].dialogue[currentDialogue];
        gameImage.GetComponent<Image>().sprite = levelList[currentLevel].screens[currentScene];
        if (levelList[currentLevel].audio[currentDialogue] != null)
        {
            _audioSource.clip = levelList[currentLevel].audio[currentDialogue];
            _audioSource.Play();
        }
    }



    public void zCreatePrefab(GameObject _minigame)
    {
        var minigame = Instantiate(_minigame, new Vector3(0, 0, 0), Quaternion.identity);
        minigame.name = "Minigame";
    }


    public void MinigameTextUpdate()
    {
        levelList[currentLevel].dialogue[1] = "MASH " + minigameButton + "!";
        dialogueText.GetComponent<TextMeshProUGUI>().text = levelList[currentLevel].dialogue[currentDialogue];
    }
}