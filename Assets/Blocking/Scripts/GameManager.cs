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
        TextUI.instance.ShowText("Let's see if there is something in this house...", 4);
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

                //Text for KeyObjects
                if (go.name.Equals("KeyObject1"))
                {
                    TextUI.instance.ShowText("I remember this object, lets take it with me...", 4);
                }
                if (go.name.Equals("KeyObject2"))
                {
                    TextUI.instance.ShowText("This is an object from one of the missing children, maybe I will need it later...", 4);
                }
                if (go.name.Equals("KeyObject3"))
                {
                    TextUI.instance.ShowText("I've seen this thing before, but can't remember where - I will take it with me...", 4);
                }

                //delete object from scene
                Destroy(go, .5f);
            }
            if (go.name.Contains("UselessItem"))
            {
                //Text for useless objects

                if (go.name.Equals("UselessItem1"))
                {
                    TextUI.instance.ShowText("A dirty piece of paper...", 3);
                }
                if (go.name.Equals("UselessItem2"))
                {
                    TextUI.instance.ShowText("Hmm... a torn-out page from a book... ", 3);
                }
                if (go.name.Equals("UselessItem3"))
                {
                    TextUI.instance.ShowText("A piece of paper, nothing important...", 3);
                }
                if (go.name.Equals("UselessItem4"))
                {
                    TextUI.instance.ShowText("Looks like a note...", 3);
                }
                if (go.name.Equals("UselessItem5"))
                {
                    TextUI.instance.ShowText("An empty bottle of beer...", 3);
                }
                if (go.name.Equals("UselessItem5"))
                {
                    TextUI.instance.ShowText("Seems like the home of an alcoholic... ", 3);
                }
                if (go.name.Equals("UselessItem6"))
                {
                    TextUI.instance.ShowText("Seems like the home of an alcoholic... ", 3);
                }
                if (go.name.Equals("UselessItem7"))
                {
                    TextUI.instance.ShowText("Some pills, better not try them... ", 3);
                }
                if (go.name.Equals("UselessItem8"))
                {
                    TextUI.instance.ShowText("Strange looking plastic can... ", 3);
                }
                if (go.name.Equals("UselessItem9"))
                {
                    TextUI.instance.ShowText("A brandnew book... ", 3);
                }
                if (go.name.Equals("UselessItem10"))
                {
                    TextUI.instance.ShowText("'The Lord of the Rings', I like this book... ", 3);
                }
            }

            //if all keyobjects -> open solution
            if (collectedItems.Count == 3)
            {
                TextUI.instance.ShowText("There was a sound at the end of the hallway...", 4);
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
