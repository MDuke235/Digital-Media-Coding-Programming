using UnityEngine;
using UnityEngine.UI; // <--- ADD THIS LINE!
using FMODUnity;

public class AudioMixController : MonoBehaviour
{
    // 1. Your new variable to remember which snapshot is playing
    private FMOD.Studio.EventInstance activeSnapshot;

    // 2. Your existing volume function (unchanged)
    public void SetAmbienceVolume(float volumeLevel)
    {
        RuntimeManager.StudioSystem.setParameterByName("AmbienceVolume", volumeLevel);
    }

    // 3. Your NEW environment function (placed safely inside the class!)
    public void ChangeEnvironment(int roomIndex)
    {
        // SAFETY CHECK: Only try to stop the snapshot if one actually exists!
        if (activeSnapshot.isValid())
        {
            activeSnapshot.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        // Dropdown Option 0 is Cave, Option 1 is Beach, Option 2 is Studio
        if (roomIndex == 0)
        {
            activeSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Cave");
        }
        else if (roomIndex == 1)
        {
            activeSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Beach");
        }
        else if (roomIndex == 2)
        {
            activeSnapshot = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Studio");
        }

        activeSnapshot.start();
    }

    [Header("UI Sliders")]
    public Slider reverbSlider;
    public Slider duckingSlider;

    void Start()
    {
        // This tells the sliders to run our functions every time the user moves them
        if (reverbSlider != null)
            reverbSlider.onValueChanged.AddListener(UpdateReverb);
            
        if (duckingSlider != null)
            duckingSlider.onValueChanged.AddListener(UpdateDucking);
    }

    public void UpdateReverb(float value)
    {
        // Sends the 0-1 slider value directly to the FMOD Global Parameter
        RuntimeManager.StudioSystem.setParameterByName("ReverbIntensity", value);
    }

    public void UpdateDucking(float value)
    {
        // Sends the 0-1 slider value directly to the FMOD Global Parameter
        RuntimeManager.StudioSystem.setParameterByName("DuckingThreshold", value);
    }
}