using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLenghth = 2f;

    void Update ()
    {
        Time.TimeScale += (1f / slowdownLenghth) * Time.unscaleddeltaTime;
        Time.Timescale = Mathf.Clamp(Time.TimeScale, 0f, 1f);
    }

    public void DoSlowmotion ()

    { 
        Time.TimeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.TimeScale * .02f;
        
    }
}
