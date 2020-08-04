using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("inpHost").GetComponent<InputField>().text = Config.HostAPI;
        GameObject.Find("inpSenha").GetComponent<InputField>().text = Config.SenhaAPI;
    }



    public void Salvar()
    {
        Config.HostAPI = GameObject.Find("inpHost").GetComponent<InputField>().text;
        Config.SenhaAPI = GameObject.Find("inpSenha").GetComponent<InputField>().text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public static class Config
{
    public static string HostAPI { get; set; } = "http://127.0.0.1:4080/";
    public static string SenhaAPI { get; set; } = "vitor123";

}
