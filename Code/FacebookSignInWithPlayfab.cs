using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Facebook.Unity;

public class FacebookSignInWithPlayfab : MonoBehaviour
{
  void sign_in_with_facebook()
    {
        FB.Init(() =>
        {
            if (FB.IsLoggedIn)
            {
                FB.LogOut();
            }
            FB.LogInWithReadPermissions(null, internal_sign_in_with_facebook =>
            {
                if (internal_sign_in_with_facebook == null || string.IsNullOrEmpty(internal_sign_in_with_facebook.Error))
                {
                    Debug.Log("Facebook Auth Complete! Access Token: " + AccessToken.CurrentAccessToken.TokenString + "\nLogging into PlayFab...");

                    PlayFabClientAPI.LoginWithFacebook(new LoginWithFacebookRequest
                    {
                        CreateAccount = true,
                        AccessToken = AccessToken.CurrentAccessToken.TokenString
                    },
                        OnPlayfabFacebookAuthComplete =>
                        {
                            Debug.Log("PlayFab Facebook Auth Complete. Session ticket: " + OnPlayfabFacebookAuthComplete.SessionTicket);
                        }, OnPlayfabFacebookAuthFailed =>
                        {
                            Debug.LogWarning(OnPlayfabFacebookAuthFailed.GenerateErrorReport());
                        });
                }
                else
                {
                    Debug.LogError("Facebook Auth Failed: " + internal_sign_in_with_facebook.Error + "\n" + internal_sign_in_with_facebook.RawResult);
                }
            });
        });
    }
}
