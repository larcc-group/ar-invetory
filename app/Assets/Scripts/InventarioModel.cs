using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

public class InventarioModel : MonoBehaviour
{
    // Start is called before the first frame update

    public static List<Componente> GetAllComponentes()
    {
        var data = HttpUtil.GetRequest("api/componente");
        return Componente.FromJsonList(data);
    }

    public static Componente GetComponente(string id)
    {
        var data = HttpUtil.GetRequest("api/componente/" + id);
        return Componente.FromJson(data);
    }
    public static Componente GetComponeteByTarget(string marcador)
    {
        var data = HttpUtil.GetRequest("api/componente/getbymarcador?marcador=" + marcador);
        return Componente.FromJson(data);
    }

    public static List<Campo> GetCamposComponente(string campo)
    {
        var data = HttpUtil.GetRequest("api/objeto/getcampos?nome=" + campo);
        return Campo.FromJsonList(data);
    }


    public static bool DeleteComponente(string id)
    {
        var data = HttpUtil.DeleteRequest("api/componente/" + id);
        return data;
    }

    public static bool PostComponente(Componente componente)
    {
        string strPayload = componente.ToJson();
        var data = HttpUtil.PostRequest("api/componente", strPayload);
        return data;
    }

    public static bool PutComponente(Componente componente, string id)
    {
        string strPayload = componente.ToJson();
        var data = HttpUtil.PutRequest("api/componente/" + id, strPayload);
        return data;
    }

}






[DataContract]
public class Componente
{
    [DataMember]
    public string _id { get; set; }
    [DataMember]
    public string Nome { get; set; }
    [DataMember]
    public string Tipo { get; set; }
    [DataMember]
    public string Modelo { get; set; }
    [DataMember]
    public string Fabricante { get; set; }
    [DataMember]
    public string Funcao { get; set; }
    [DataMember]
    public string Marcador { get; set; }
    [DataMember]
    public int Projecao { get; set; }
    [DataMember]
    public List<Filho> Filhos { get; set; } = new List<Filho>();
    [DataMember]
    public List<Detalhe> Detalhes { get; set; } = new List<Detalhe>();
    [DataMember]
    public List<Log> Logs { get; set; } = new List<Log>();

    public static Componente FromJson(string json)
            => JsonConvert.DeserializeObject<Componente>(json);

    public static List<Componente> FromJsonList(string json)
          => JsonConvert.DeserializeObject<List<Componente>>(json);

    public string ToJson() => JsonConvert.SerializeObject(this, Formatting.Indented);


}

public class Filho
{
    public string Tipo { get; set; }
    public List<string> Itens { get; set; }
}

public class Log
{
    public DateTime Data { get; set; }
    public string Texto { get; set; }
}

public class Detalhe
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public static class Marcador
{
    public static List<string> Marcadores { get{
            var m = new List<string>();
            m.Add("001");
            m.Add("002");
            m.Add("003");
            m.Add("004");
            m.Add("005");
            m.Add("006");
            m.Add("007");
            m.Add("008");
            m.Add("009");
            m.Add("010");
            m.Add("011");
            m.Add("012");
            m.Add("013");
            m.Add("014");
            m.Add("015");
            m.Add("016");
            m.Add("017");
            m.Add("018");
            m.Add("019");
            m.Add("020");
            return m;
        } }
    
    

}