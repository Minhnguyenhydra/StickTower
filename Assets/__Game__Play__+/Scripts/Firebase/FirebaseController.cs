using Firebase;
using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseController : MonoBehaviour
{
    private static FirebaseController Instance;

    private FirebaseApp app;
    private int sceneIndex;
    private string sceneName;


    private void Start()
    {
        if (Instance != null)
            return;

        Instance = this;
        Init();
        DontDestroyOnLoad(this);
    }



    private void Init()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                app = FirebaseApp.DefaultInstance;

                // Set a flag here to indicate whether Firebase is ready to use by your app.
            }
            else
            {
                Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                // Firebase Unity SDK is not safe to use here.
            }
        });

        var activeScene = SceneManager.GetActiveScene();
        sceneIndex = activeScene.buildIndex;
        sceneName = activeScene.name;

        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
            new Parameter(FirebaseAnalytics.ParameterLevel, sceneIndex),
            new Parameter(FirebaseAnalytics.ParameterLevelName, sceneName));
    }

    private void OnDisable()
    {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
                                   new Parameter(FirebaseAnalytics.ParameterLevel, sceneIndex),
                                   new Parameter(FirebaseAnalytics.ParameterLevelName, sceneName));
    }
}
