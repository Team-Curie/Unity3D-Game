using UnityEngine;
using System.Collections;

public class CursorLockHide : MonoBehaviour {

    bool isLocked;
    
    
    // Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetCursorLock(!isLocked);
        }
	
	}

    void SetCursorLock(bool isLocked)
    {
        this.isLocked = isLocked;
        Screen.lockCursor = isLocked;
        Cursor.visible = !isLocked;

    }






}
