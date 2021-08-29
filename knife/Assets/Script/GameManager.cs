using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public State state;
    public enum State
    {
        WaitToStart,
        Playing,
        Dead,
        Win
    }

    public Transform knife;
}
