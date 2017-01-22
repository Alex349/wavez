﻿using UnityEngine;

public class ScreenGrab : MonoBehaviour {
    [SerializeField]
    int superSize = 2;

	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Application.CaptureScreenshot(Time.time.ToString("F0") + ".png", superSize);
        }
	}
}
