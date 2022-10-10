using FMODUnity;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    StudioEventEmitter emitter;
    FMOD.Studio.Bus MasterBus;
    public GameObject BGMAudio;

    //파라미터이름:Zoom
    public GameObject mainCamera;
    public float _zoom;

    //TimeLeft
    public GameObject timer;
    [SerializeField] private float _pureWidth;
    [SerializeField] private float _pureCurrentSizeX;


    //Pause
    public GameObject pauseCanvas;


    void Start()
    {
        emitter = GetComponent<FMODUnity.StudioEventEmitter>();
        MasterBus = FMODUnity.RuntimeManager.GetBus("bus:/");

        _pureWidth = timer.GetComponent<UI_Timer>().width - timer.GetComponent<UI_Timer>().endWidth;

    }

    void Update()
    {
        if (_pureCurrentSizeX <= 0)
        {
            Time.timeScale = 0;
            //MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //...
            //BGMAudio.SetActive(false); //...
            return;
        }

        //Zoom
        _zoom = mainCamera.GetComponent<MultipleTargetCamera>().newZoom;
        emitter.SetParameter("Zoom", _zoom);

        //Timer
        _pureCurrentSizeX = timer.GetComponent<UI_Timer>().currentSize.x
            - timer.GetComponent<UI_Timer>().endWidth;
        //emitter.SetParameter("TimeLeft", 0);

        if (_pureCurrentSizeX <= (_pureWidth / 2)&& _pureCurrentSizeX > (_pureWidth / 4))
        {
            emitter.SetParameter("TimeLeft", 1); //HalfTime:Value==1
        }
        else if (_pureCurrentSizeX <= (_pureWidth / 4)&& _pureCurrentSizeX >0)
        {
            emitter.SetParameter("TimeLeft", 2); //QuarterTime:Value==2
        }

        //Pause
        if (pauseCanvas.activeSelf)
        {
            emitter.SetParameter("Pause", 1);
        }
        else
        {
            emitter.SetParameter("Pause", 0);
        }

    }
}
