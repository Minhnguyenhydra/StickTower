using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFollowDevice : MonoBehaviour
{
    public float Aspect;
    public bool GizmosUpdate;
    public bool IsInvert;
    public bool changeCamSize;
    public float CurCamSize;
#if UNITY_EDITOR
    public static Action OnSolutionChanged;
#endif

    public List<ResolutionInfor> Resolutions = new List<ResolutionInfor>
    {
        new ResolutionInfor
        {
            Name = "Fold 2 5G Tablet",
            Aspect = 2208f / 1768f
        },
        new ResolutionInfor
        {
            Name = "Ipad",
            Aspect = 2732f / 2048f
        },
        new ResolutionInfor
        {
            Name = "Iphone 7",
            Aspect = 2208f / 1242f
        },
        new ResolutionInfor
        {
            Name = "Iphone XS Max",
            Aspect = 2437f / 1125f
        },
        new ResolutionInfor
        {
            Name = "Redmi",
            Aspect = 2400f / 1080f
        },
        new ResolutionInfor
        {
            Name = "Fold2 5G Phone",
            Aspect = 2658f / 960f
        },
        new ResolutionInfor
        {
            Name = "Quynh",
            Aspect = 3200f / 1440f
        },
    };

    private CanvasScaler _canvasScaler;
    [SerializeField] private Camera _cam;

    private void Awake()
    {
        var canvas = gameObject.GetComponent<Canvas>();

        _canvasScaler = GetComponent<CanvasScaler>();
        if (_cam)
        {
            _cam = Camera.main;
            canvas.worldCamera = _cam;
        }
        FixCamSizeFollowScreen();


        Debug.LogError("=======Resolution: " + Screen.width + "/" + Screen.height);
#if UNITY_EDITOR
        OnSolutionChanged += Update;
#endif
    }

#if UNITY_EDITOR
    public void Update()
    {
        FixCamSizeFollowScreen();
    }
#endif

    [ContextMenu("Fix cam zide follow screen")]
    private void FixCamSizeFollowScreen()
    {
        if (this != null && !enabled)
            return;

        if (_cam == null)
        {
            _cam = FindObjectOfType<Camera>();
        }

        if (_canvasScaler == null)
        {
            _canvasScaler = GetComponent<CanvasScaler>();
        }

        if (IsInvert)
        {
            if (_cam)
            {
                Aspect = 1 / _cam.aspect;
            }
        }
        else
        {
            if (_cam)
            {
                Aspect = _cam.aspect;
            }
        }

        for (int i = 0; i < Resolutions.Count - 1; i++)
        {
            if (Mathf.Approximately(Aspect, Resolutions[i].Aspect))
            {
                _canvasScaler.matchWidthOrHeight = Mathf.Clamp(Resolutions[i].Scaler, 0f, 1f);

                if (_cam && changeCamSize)
                {
                    if (_cam.orthographic)
                    {
                        _cam.orthographicSize = Resolutions[i].CamSize;
                    }
                    else
                    {
                        _cam.fieldOfView = Resolutions[i].PerspectiveSize;
                    }
                }
                return;
            }
            else
            {
                if (Aspect > Resolutions[i].Aspect && Aspect < Resolutions[i + 1].Aspect)
                {
                    _canvasScaler.matchWidthOrHeight = Mathf.Clamp(Resolutions[i].Scaler + (Aspect - Resolutions[i].Aspect) / (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) * (Resolutions[i + 1].Scaler - Resolutions[i].Scaler), 0f, 1f);
                    if (_cam && changeCamSize)
                    {
                        if (_cam.orthographic)
                        {
                            _cam.orthographicSize = Resolutions[i].CamSize + (Aspect - Resolutions[i].Aspect) / (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) * (Resolutions[i + 1].CamSize - Resolutions[i].CamSize);
                        }
                        else
                        {
                            //_cam.fieldOfView = Resolutions[i].PerspectiveSize + (Aspect - Resolutions[i].Aspect) / (Resolutions[i + 1].Aspect - Resolutions[i].Aspect) * (Resolutions[i + 1].PerspectiveSize - Resolutions[i].PerspectiveSize);
                        }
                    }
                    return;
                }
            }
        }
        if (_cam)
        {
            CurCamSize = _cam.orthographicSize;
        }
    }


    [ContextMenu("OrderResolutions")]
    public void OrderResolutions()
    {
        Resolutions.OrderBy(s => s.Aspect);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (GizmosUpdate)
        {
            FixCamSizeFollowScreen();
        }
    }
#endif
}

[Serializable]
public class ResolutionInfor
{
    public string Name;
    public float Aspect;
    public float CamSize;
    public float PerspectiveSize = 60;
    public float Scaler;
}