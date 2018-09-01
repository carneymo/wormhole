using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseModel : MonoBehaviour {

    protected Rigidbody rig;
    protected GameController gameController;
    
    protected void Init()
    {
        this.name = this.GetType().ToString();
        rig = GetComponent<Rigidbody>();
        GetGameController();
    }

    protected void GetGameController()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }
}
