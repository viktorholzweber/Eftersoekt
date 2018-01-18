using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


enum gameStates { defaultState, startState, midgameState, solutionState, endState };

public class GameManager : MonoBehaviour 
{
    public CharacterController m_character;
    public string m_gameScene;

    private ArrayList collectedItems = new ArrayList();
    private gameStates currentState = gameStates.defaultState;

    public static GameManager instance;

    void Start()
    {
        showMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void showMenu()
    {
        EnableCharacter(false);
        //show menu and controls and so on
        startGame();
    }
    public void startGame()
    {
        Debug.Log("Game starting");
        currentState = gameStates.startState;
        collectedItems.Clear();
        EnableCharacter(true);
        //dont show menu, set cam to spawn
        m_character.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().GroundController();

        currentState = gameStates.midgameState;
        Debug.Log("Game started");
        TextUI.instance.ShowText("Let's see if there is something in this house...", 5);
    }

    public void endGame()
    {
        Debug.Log("Successfully finished the game!");
        currentState = gameStates.endState;
        //show finished menu
    }

    public void addCollectedItem(GameObject go)
    {
        Debug.Log(collectedItems.Count + " items collected!");
        if (go.name.Equals("DeleteForSolution") && currentState==gameStates.solutionState)
        {
            solutionGame(go);
        }
        else
        {
            //show preview
            //EnableCharacter(false);
            //CloseUpObjectPreview.instance.ShowPreview(go.GetInstanceID());

            //add if keyobject
            if (go.name.Contains("KeyObject"))
            {
                collectedItems.Add(go);
                Debug.Log(go.name + " collected!");
                Debug.Log(collectedItems.Count + " items collected!");

                //delete object from scene
                Destroy(go, .5f);
            }

            //if all keyobjects -> open solution
            if (collectedItems.Count == 3)
            {
                TextUI.instance.ShowText("There was a sound at the end of the hallway...", 5);
                currentState = gameStates.solutionState;
            }
        }
    }

    public void solutionGame(GameObject go)
    {
            Destroy(go, .5f);
        
        //licht einschalten
        //kasten interaktion einschalten
        //TODO: Wand entfernen
    }

    public void EnableCharacter(bool enable)
    {
        m_character.enabled = enable;
        m_character.GetComponent<FirstPersonController>().enabled = enable;
        //m_character.GetComponentInChildren<LookAtInteraction>().enabled = enable;
        if (enable)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public void OnClosePreview()
    {
        EnableCharacter(true);
    }

    void Awake()
    {
        GameManager.instance = this;
    }
}
