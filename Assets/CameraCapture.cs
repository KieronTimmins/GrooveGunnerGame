using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCapture : MonoBehaviour
{
    // Set the file name
    public string screenshotFileName = "screenshot.png";

    // Set the camera to capture
    public Camera captureCamera;

    // Update is called once per frame
    void Update()
    {
        // Example: Capture screenshot when the 'S' key is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            CaptureScreenshot();
        }
    }

    void CaptureScreenshot()
    {
        // Create a RenderTexture
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        captureCamera.targetTexture = renderTexture;

        // Create a Texture2D and read the RenderTexture data into it
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        captureCamera.Render();
        RenderTexture.active = renderTexture;
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Convert the Texture2D to bytes in PNG format
        byte[] bytes = screenshotTexture.EncodeToPNG();

        // Reset camera's target texture
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Get the path to the "Downloads" folder
        string downloadsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile) + "/Downloads/";

        // Save the screenshot to the "Downloads" folder
        System.IO.File.WriteAllBytes(downloadsPath + screenshotFileName, bytes);

        Debug.Log("Screenshot saved to: " + downloadsPath + screenshotFileName);
    }
}
