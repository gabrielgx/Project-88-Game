    using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FPSCounter : MonoBehaviour
{

    public bool ShowFramesPerSecond = true;
    public Text FPSFrames = null;
    public float UpdateInterval = 0.5F;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    void Update()
    {
        if (ShowFramesPerSecond && FPSFrames != null)
        {
            FramesPerSecond();
        }
    }

    void FramesPerSecond()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            FPSFrames.text = format;

            if (fps < 30)
            {
                FPSFrames.color = Color.yellow;
            }
            else
            {
                if (fps < 10)
                {
                    FPSFrames.color = Color.red;
                }
                else
                {
                    FPSFrames.color = Color.green;
                }
            }
            timeleft = UpdateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }

    public void ChangeFPSFrames(bool b)
    {
        ShowFramesPerSecond = b;
        FPSFrames.gameObject.SetActive(b);
    }
}