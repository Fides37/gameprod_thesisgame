using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PhotoCapture: MonoBehaviour
{
    private Texture2D screenCapture;

    [SerializeField ]private Image photoDisplayArea;

    [SerializeField] GameObject photoFrame;
    private bool viewingPhoto;

    private void Start()
    {
        photoFrame.SetActive(false);
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }

    private void Update()
    {
        Debug.Log(viewingPhoto);

        if (photoFrame.activeSelf)
        {
            viewingPhoto = true;
        }
        else
        {
            viewingPhoto = false;
        }



    if (Input.GetMouseButtonDown(0))
        {
            switch (viewingPhoto)
            {
                case false: StartCoroutine(CapturePhoto());
                    break;
                case true: RemovePhoto();
                    break;
            }
        }

    }

    IEnumerator CapturePhoto()
    {
        //ccamera ui to false

        viewingPhoto = true;
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect (0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2 (0.5f, 0.5f), 100.0f);

        photoDisplayArea.sprite = photoSprite;

        photoFrame.SetActive(true);
    }


    void RemovePhoto()
    {
        viewingPhoto = false;
        photoFrame.SetActive(false);

        //camera ui true

    }

}
