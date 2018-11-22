using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetMessage : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetText());
    }

    string lastMessage = string.Empty;
    public string url = "http://www.google.co.uk";

    IEnumerator GetText()
    {

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string downloadText = www.downloadHandler.text;
                // Show results as text
                Debug.Log(downloadText);
                if (downloadText != lastMessage)
                {
                    lastMessage = downloadText;
                    EventManager.TriggerEvent("message", downloadText);
                }

            }
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(GetText());

    }
}
