using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum State { menu, game, end };

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Text timerText;
    State state;
    // Start is called before the first frame update
    void Start()
    {
        state = State.menu;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.menu)
        {
            Debug.Log("In Menu");
            if (Input.GetKeyDown(KeyCode.G))
                state = State.game;
        }
        else if (state == State.game)
        {
            if (timer.timeRemaining > 0)
            {
                timerText.text = timer.timeRemaining.ToString("#.00");
                timer.tick();
            }
            else
                state = State.end;
        }
        else if (state == State.end)
        {
            Debug.Log("Game End State (SCOREBOARD?)");
        }
    }
}
