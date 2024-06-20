using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.AdsScripts
{
    public class ScreenShotMaker : MonoBehaviour
    {
        private int supersize = 2;
        private int count;
        void Start()
        {
            count = PlayerPrefs.GetInt("scCount");
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakeScreenShot();
            }
        }
        public void TakeScreenShot()
        {
            ScreenCapture.CaptureScreenshot($"Screenshot{count}.png", supersize);
            count++;
            PlayerPrefs.SetInt("scCount", count);

        }
    }
}