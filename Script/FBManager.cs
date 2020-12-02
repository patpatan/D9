using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBManager : MonoBehaviour
{
    private static FBManager _instance;
    public static FBManager Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject fbm = new GameObject("FBManager");
                fbm.AddComponent<FBManager>();
            }
            return _instance;
        }
    }

    public bool IsLoggedIn { get; set; }
    public string ProfileName { get; set; }
    public Sprite ProfilePic { get; set; }
    public string AppLinkURL { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        _instance = this;
    }

    public void InitFB()
    {
        if (!FB.IsInitialized)
        {

            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            IsLoggedIn = FB.IsLoggedIn;
        }
    }
    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
            GetProfile();
        }
        else
        {
            Debug.Log("FB is not logged in");
        }

        IsLoggedIn = FB.IsLoggedIn;
    }
    void OnHideUnity(bool isGameShow)
    {
        if (!isGameShow)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void GetProfile()
    {
        FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
        FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
        FB.GetAppLink(DealWithAppLink);
    }
    void DisplayUsername(IResult result)
    {
        if (result.Error == null)
        {
            ProfileName = "" + result.ResultDictionary["first_name"];
        }
        else
        {
            Debug.Log (result.Error);
        }
    }
    public void DisplayProfilePic(IGraphResult result)
    {
        if (result.Texture != null)
        {
            ProfilePic = Sprite.Create (result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
    void DealWithAppLink(IAppLinkResult result)
    {
        if(!string.IsNullOrEmpty(result.Url))
        {
            AppLinkURL = result.Url;
        }
        else
        {
            AppLinkURL = "https://drive.google.com/drive/folders/1UEYKx7dskbxrkbDkjS4ljmOGGzt98xYC?usp=sharing";
        }
    }
    public void Share()
    {
        FB.FeedShare 
        (
           string.Empty,
           new Uri(AppLinkURL),
           "NinJaSpace",
           "Come to play with me",
           "Have Fun",
           new Uri("https://drive.google.com/drive/folders/1UEYKx7dskbxrkbDkjS4ljmOGGzt98xYC?usp=sharing"),
           string.Empty,
           ShareCallback
        );
    }
    void ShareCallback(IResult result)
    {
        if (result.Cancelled)
        { 
        Debug.Log("Share Cancelled");
        }
        else if (!string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Error on share");
        }
        else if (!string.IsNullOrEmpty(result.RawResult))
        {
            Debug.Log("Success on share");
        }
    }
}