using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //public float mouseSensitivity = 100f;

    public Transform playerBody;
    public Vector2 smoothAmount;

    float m_xRotation = 0.0f;
    float m_yRotation = 0.0f;

    float m_desireX = 0.0f;
    float m_desireY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //if (Setting.current)
        //{
        //    mouseSensitivity = Setting.current.mouseSensitivity;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        MouseMoveAssist();
    }

    void MouseMoveAssist()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity.current.value * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity.current.value * Time.deltaTime;

        m_desireY -= mouseY;
        m_desireX += mouseX;
        m_desireY = Mathf.Clamp(m_desireY, -90.0f, 90.0f);

        m_xRotation = Mathf.Lerp(m_xRotation, m_desireX, smoothAmount.x * Time.deltaTime);
        m_yRotation = Mathf.Lerp(m_yRotation, m_desireY, smoothAmount.y * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(m_yRotation, 0.0f, 0.0f);
        playerBody.transform.eulerAngles = new Vector3(0.0f, m_xRotation, 0.0f);
    }
}
