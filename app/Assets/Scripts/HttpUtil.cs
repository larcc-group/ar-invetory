using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class HttpUtil : MonoBehaviour
{

    private static string host = Config.HostAPI;

    public static string GetRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(host + url))
        {
            request.SendWebRequest();
            while (!request.isDone)
                Thread.Sleep(50);

            if (request.isNetworkError || request.isHttpError)
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            else
                return request.downloadHandler.text;

            return "";
        }
    }

    public static bool DeleteRequest(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Delete(host + url))
        {
            request.SendWebRequest();
            while (!request.isDone)
                Thread.Sleep(50);

            if (request.isNetworkError || request.isHttpError)
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            else
                 if (request.responseCode == 200)
                return true;
            else
                return false;

            return false;
        }
    }

    public static bool PostRequest(string url, string data)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(host + url, data))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "*/*");
            request.SendWebRequest();
            while (!request.isDone)
                Thread.Sleep(50);

            if (request.isNetworkError || request.isHttpError)
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            else

            if (request.responseCode == 200)
                return true;
            else
                return false;

            return false;
        }
    }

    public static bool PutRequest(string url, string data)
    {
        using (UnityWebRequest request = UnityWebRequest.Put(host + url, data))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            request.SendWebRequest();
            while (!request.isDone)
                Thread.Sleep(50);

            if (request.isNetworkError || request.isHttpError)
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            else
                 if (request.responseCode == 200)
                return true;
            else
                return false;

            return false;
        }
    }


}
