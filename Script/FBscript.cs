using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class FBscript : MonoBehaviour
{
    public GameObject LoggedIn;
    public GameObject LoggedOut;
    public GameObject Username;
    public GameObject UserProfilePic;


    void Awake()
       {
       FBManager.Instance.InitFB();
       DealWithFBMenus(FB.IsLoggedIn);
        

    }

    public void FBlogin()
    {
        List<string> permissions = new List<string> ();
        permissions.Add ("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
             Debug.Log (result.Error);
        } 
        else 
        {
            if(FB.IsLoggedIn)
            {
                FBManager.Instance.IsLoggedIn = true;
                FBManager.Instance.GetProfile();
                Debug.Log("FB is logged in");
            } 
            else 
            {
                Debug.Log("FB is not logged in");
            }
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }
   
   void DealWithFBMenus( bool IsLoggedIn)
   {
       if (IsLoggedIn)
       {
           LoggedIn.SetActive (true);
           LoggedOut.SetActive (false);
           
            if(FBManager.Instance.ProfileName != null)
            {
                Text UserName = Username.GetComponent<Text>();
                UserName.text = "" + FBManager.Instance.ProfileName;
            }
            else
            {
                StartCoroutine("WaitForProfileName");
            }
            if (FBManager.Instance.ProfilePic != null)
            {
                Image ProfilePic = UserProfilePic.GetComponent<Image>();
                ProfilePic.sprite = FBManager.Instance.ProfilePic;
            }
            else
            {
                StartCoroutine("WaitForProfilePic");
            }
        }
       else 
       {
           LoggedIn.SetActive (false);
           LoggedOut.SetActive (true);
       }
   }
    IEnumerator WaitForProfileName()
    {
        while(FBManager.Instance.ProfileName == null)
        {
            yield return null;
        }
        DealWithFBMenus (FB.IsLoggedIn);
    }
    IEnumerator WaitForProfilePic()
    {
        while (FBManager.Instance.ProfilePic == null)
        {
            yield return null;
        }
        DealWithFBMenus(FB.IsLoggedIn);
    }

    public void Share()
    {
        FBManager.Instance.Share();
    }
    public void Logout()
    {
        FB.LogOut();

        LoggedIn.SetActive(false);
        LoggedOut.SetActive(true);
    }

}