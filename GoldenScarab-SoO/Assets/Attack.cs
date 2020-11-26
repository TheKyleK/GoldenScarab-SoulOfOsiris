using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public float magX;
    public float magY;
    public float time;
    public AnimationCurve curve;
    public Transform trans;
    public Camera cam;
    public PlayerStateMachine psm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoseGameCorutine()
    {
        yield return new WaitForSeconds(1.0f);
        GameSceneManager.current.LoadScene(2, 1);
    }

    void LoseGame()
    {
        GameSceneManager.current.LoadScene(2, 1);
    }

    void Shake()
    {
        CameraShake.current.Shake(magX, magY, time, curve);
    }

    void FreezePlayer()
    {
        psm.state = new PlayerDeadState();
    }

    void SwitchCamera()
    {
        Camera.main.gameObject.SetActive(true);
    }
}
