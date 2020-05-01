using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    FMOD.Studio.Bus masterBus;

    [FMODUnity.EventRef]
    public string wrenchEvent = "";
    [FMODUnity.EventRef]
    public string openDoorEvent = "";
    [FMODUnity.EventRef]
    public string pickUpEvent = "";
    [FMODUnity.EventRef]
    public string useItemEvent = "";
    [FMODUnity.EventRef]
    public string weatherChangeEvent = "";
    [FMODUnity.EventRef]
    public string breathEvent = "";

    FMOD.Studio.EventInstance wrenchInstance;
    FMOD.Studio.EventInstance openDoorInstance;
    FMOD.Studio.EventInstance pickUpInstance;
    FMOD.Studio.EventInstance useItemInstance;
    FMOD.Studio.EventInstance weatherChangeInstance;
    FMOD.Studio.EventInstance breathInstance;

    FMOD.Studio.PARAMETER_ID pARAMETEROxygenId;
    FMOD.Studio.PARAMETER_DESCRIPTION pARAMETEROxygenDescription;
    FMOD.Studio.EventDescription breathEventDescription;

    // Start is called before the first frame update
    void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("bus:/");
        breathEventDescription = FMODUnity.RuntimeManager.GetEventDescription(breathEvent);
        breathEventDescription.getParameterDescriptionByName("OxygenValue", out pARAMETEROxygenDescription);
        pARAMETEROxygenId = pARAMETEROxygenDescription.id;
        wrenchInstance = FMODUnity.RuntimeManager.CreateInstance(wrenchEvent);
        openDoorInstance = FMODUnity.RuntimeManager.CreateInstance(openDoorEvent);
        pickUpInstance = FMODUnity.RuntimeManager.CreateInstance(pickUpEvent);
        useItemInstance = FMODUnity.RuntimeManager.CreateInstance(useItemEvent);
        weatherChangeInstance = FMODUnity.RuntimeManager.CreateInstance(weatherChangeEvent);
        breathInstance = FMODUnity.RuntimeManager.CreateInstance(breathEvent);
        masterBus.setVolume(0.4f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayWrenchSound() {
        wrenchInstance.start();
    }

    public void PlayOpenDoorSound()
    {
        openDoorInstance.start();
    }

    public void PlayPickSound()
    {
        pickUpInstance.start();
    }

    public void PlayUseItemSound()
    {
        useItemInstance.start();
    }

    public void PlayWeatherChangeSound()
    {
        weatherChangeInstance.start();
    }

    public void PlayBreathSound()
    {
        breathInstance.start();
    }

    public void StopBreathSound()
    {
        breathInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    public void SetBreathSound(int OxygenValue)
    {
        breathInstance.setParameterByID(pARAMETEROxygenId, OxygenValue);
    }

    public void SetVolume(float volume) {
        masterBus.setVolume(volume);
    }

}
