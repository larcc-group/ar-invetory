using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ObjetosModel : MonoBehaviour
{
    // Start is called before the first frame update


    public static List<Objeto> GetAllObjetos()
    {
        var data = HttpUtil.GetRequest("api/objeto");
        return Objeto.FromJsonList(data);
    }

    public static Objeto GetObjeto(string id)
    {
        var data = HttpUtil.GetRequest("api/objeto/" + id);
        return Objeto.FromJson(data);
    } 

    public static bool DeleteObjeto(string id)
    {
        var data = HttpUtil.DeleteRequest("api/objeto/" + id);
        return data;
    }

    public static bool PostObjeto(Objeto objeto)
    {
        string strPayload = objeto.ToJson();
        var data = HttpUtil.PostRequest("api/objeto", strPayload);
        return data;
    }
    public static bool PutObjeto(Objeto objeto, string id)
    {

        string strPayload = objeto.ToJson();
        var data = HttpUtil.PutRequest("api/objeto/" + id, strPayload);
        return data;
    }

}




[DataContract]
public class Objeto
{
    [DataMember]
    public string _id { get; set; }
    [DataMember]
    public string Nome { get; set; }
    [DataMember]
    public string Descricao { get; set; }
    [DataMember]
    public bool AR { get; set; }
    [DataMember]
    public List<string> Filhos { get; set; }
    [DataMember]
    public List<Campo> Campos { get; set; }
    public static Objeto FromJson(string json)
            => JsonConvert.DeserializeObject<Objeto>(json);

    public static List<Objeto> FromJsonList(string json)
          => JsonConvert.DeserializeObject<List<Objeto>>(json);

    public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);


}

public class Campo
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Tipo { get; set; }

    public static Campo FromJson(string json)
           => JsonConvert.DeserializeObject<Campo>(json);
    public static List<Campo> FromJsonList(string json)
          => JsonConvert.DeserializeObject<List<Campo>>(json);
    public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);

}

