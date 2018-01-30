using UnityEngine;
using GoogleARCore;

public class ApplicationController : MonoBehaviour {

#region Status
    bool isExitting;
#endregion

#region Constants
    const string NEED_PERMISSION_MESSEGE = "Camera permission is needed to run this application.";
    const string CONNECTION_FAILED_MESSEGE = "ARCore encountered a problem connecting.  Please start the app again.";
#endregion

#region Unity
    // Update is called once per frame
    void Update () {
        CheckForceExit();
        CheckConnectionError();
    }
#endregion

#region Force Exit Check

    bool IsForceExit()
    {
        return Input.GetKey(KeyCode.Escape);
    }

    void CheckForceExit()
    {
        if (IsForceExit())
            Exit();
    }

    void Exit()
    {
        Application.Quit();
    }
#endregion

#region Error Check

    bool IsPermissionDenied()
    {
        return Session.ConnectionState == SessionConnectionState.UserRejectedNeededPermission;
    }

    bool IsConnectionFailed()
    {
        return Session.ConnectionState == SessionConnectionState.ConnectToServiceFailed;
    }

    void ExitAndNotify(string messege)
    {
        ToastMessage(messege);
        isExitting = true;
        Invoke("Exit", 0.5f);
    }

    void CheckConnectionError()
    {
        if (!isExitting)
        {
            if (IsPermissionDenied()) ExitAndNotify(NEED_PERMISSION_MESSEGE);
            else if (IsConnectionFailed()) ExitAndNotify(CONNECTION_FAILED_MESSEGE);
        }
    }
#endregion

#region Android Method
    public void ToastMessage(string message)
    {
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        if (unityActivity != null)
        {
            AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity,
                    message, 0);
                toastObject.Call("show");
            }));
        }
    }
#endregion

}
