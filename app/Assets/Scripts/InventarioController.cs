using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioController : MonoBehaviour
{
    public GameObject ItemParent { get; set; }
    public GameObject Item { get; set; }
    private GameObject ItemEdit { get; set; }



    public InventarioController(GameObject item, GameObject itemParent)
    {
        Item = item;
        ItemParent = itemParent;
    }

    public void Read()
    {

        for (int i = 0; i < ItemParent.transform.childCount; i++)
        {
            Destroy(ItemParent.transform.GetChild(i).gameObject);
        }

        List<Componente> componentes = InventarioModel.GetAllComponentes();
        componentes.ForEach(x => {

            GameObject tmpItem = Instantiate(Item, ItemParent.transform);

            tmpItem.name = x._id;
            tmpItem.transform.GetChild(0).GetComponent<Text>().text = x.Nome;
            //tmpItem.transform.GetChild(1).GetComponent<Text>().text = x.Tipo;

        });
    }

    public void Gravar()
    {
        try
        {
            Componente componente = new Componente();

            componente.Nome = GameObject.Find("inpNome").GetComponent<InputField>().text;
            
            var inpTipo = GameObject.Find("inpTipo").GetComponent<Dropdown>();
            componente.Tipo = inpTipo.options[inpTipo.value].text;

            var inpMarcador = GameObject.Find("inpMarcador").GetComponent<Dropdown>();
            componente.Marcador = inpMarcador.options[inpMarcador.value].text;

            componente.Projecao = GameObject.Find("inpProjecao").GetComponent<Dropdown>().value;
            componente.Modelo = GameObject.Find("inpModelo").GetComponent<InputField>().text;
            componente.Fabricante = GameObject.Find("inpFabricante").GetComponent<InputField>().text;
            componente.Funcao = GameObject.Find("inpFuncao").GetComponent<InputField>().text;

            var result = InventarioModel.PostComponente(componente);

            LimpaCmpos();
            Read();
        }
        catch (Exception ex)
        {

        }
    }

    public void Delete(GameObject gameObject)
    {
        string id = gameObject.name;
        bool isOk = InventarioModel.DeleteComponente(id);

        if (isOk)
        {
            Destroy(gameObject);
        }
    }

    public void PreencherCampos(GameObject itemEdit)
    {
        ItemEdit = itemEdit;
        string id = itemEdit.name;
        if (Parametros.IsInventarioEdit)
        {
            id = Parametros.InventarioId;
        }


        Componente componente = InventarioModel.GetComponente(id);

        InventarioView.AtivarFormulario();


        GameObject.Find("itemEdit").GetComponent<Text>().text = componente._id;

        GameObject.Find("inpNome").GetComponent<InputField>().text = componente.Nome;

        var inpTipo = GameObject.Find("inpTipo").GetComponent<Dropdown>();
        inpTipo.value = inpTipo.options.FindIndex(x => x.text == componente.Tipo);
        inpTipo.enabled = false;

        var inpMarcador = GameObject.Find("inpMarcador").GetComponent<Dropdown>();
        inpMarcador.value = inpMarcador.options.FindIndex(x => x.text == componente.Marcador);

        GameObject.Find("inpProjecao").GetComponent<Dropdown>().value = componente.Projecao;
        GameObject.Find("inpModelo").GetComponent<InputField>().text = componente.Modelo;
        GameObject.Find("inpFabricante").GetComponent<InputField>().text = componente.Fabricante;
        GameObject.Find("inpFuncao").GetComponent<InputField>().text = componente.Funcao;

    }

    public void Editar()
    {
        try
        {
            string id = GameObject.Find("itemEdit").GetComponent<Text>().text;
            if (Parametros.IsInventarioEdit)
            {
                id = Parametros.InventarioId;
            }

            
            Componente componente = InventarioModel.GetComponente(id);

            componente.Nome = GameObject.Find("inpNome").GetComponent<InputField>().text ?? componente.Nome;


            var inpTipo = GameObject.Find("inpTipo").GetComponent<Dropdown>();
            componente.Tipo = inpTipo.options[inpTipo.value].text ?? componente.Tipo;

            var inpMarcador = GameObject.Find("inpMarcador").GetComponent<Dropdown>();
            componente.Marcador = inpMarcador.options[inpMarcador.value].text ?? componente.Marcador;

            componente.Projecao = GameObject.Find("inpProjecao").GetComponent<Dropdown>().value;
            componente.Modelo = GameObject.Find("inpModelo").GetComponent<InputField>().text ?? componente.Marcador;
            componente.Fabricante = GameObject.Find("inpFabricante").GetComponent<InputField>().text ?? componente.Marcador;
            componente.Funcao = GameObject.Find("inpFuncao").GetComponent<InputField>().text ?? componente.Marcador;

            var result = InventarioModel.PutComponente(componente, id);

            LimpaCmpos();
            Read();
        }
        catch (Exception ex)
        {

        }
    }

    public void SalvarDetalhes()
    {
        string id = GameObject.Find("itemEdit").GetComponent<Text>().text;
        if (Parametros.IsInventarioEdit)
        {
            id = Parametros.InventarioId;
        }

        Componente componente = InventarioModel.GetComponente(id);


        List<Detalhe> detalhes = LerGridDetalhes();
        componente.Detalhes = detalhes;

        InventarioModel.PutComponente(componente, id);
    }


    private List<Detalhe> LerGridDetalhes()
    {
        List<Detalhe> detalhes = new List<Detalhe>();
        GameObject gridCampos = GameObject.Find("detalhesContent");
        for (int i = 0; i < gridCampos.transform.childCount; i++)
        {
            var item = gridCampos.transform.GetChild(i).gameObject;

            detalhes.Add(new Detalhe
            {
                Key = item.name,
                Value = item.transform?.GetChild(1)?.GetComponent<InputField>()?.text ??
                    item.transform.GetChild(0)?.GetComponent<Toggle>()?.isOn.ToString()
            }) ;
        }
        return detalhes;
    }


    private string BuscarValorDetalhe(List<Detalhe> detalhes, string campo)
    {
        foreach(var detalhe in detalhes)
        {
            if (detalhe.Key == campo)
            {
                return detalhe.Value;
            }
        }
        return "";
    }

    public void AbrirDetalhes()
    {
        string id = GameObject.Find("itemEdit").GetComponent<Text>().text;
        if (Parametros.IsInventarioEdit)
        {
            id = Parametros.InventarioId;
        }

        Componente componente = InventarioModel.GetComponente(id);

        var campos = InventarioModel.GetCamposComponente(componente.Tipo);


        GameObject parentC = GameObject.Find("detalhesContent");
        for (int i = 0; i < parentC.transform.childCount; i++)
        {
            Destroy(parentC.transform.GetChild(i).gameObject);
        }

        foreach (var campo in campos)
        {
            GameObject itemC, tmpItem;
            switch (campo.Tipo)
            {
                case "texto":
                    itemC = GameObject.Find("itemTexto");
                    tmpItem = Instantiate(itemC, parentC.transform);

                    tmpItem.name = campo.Nome;
                    tmpItem.transform.GetChild(0).GetComponent<Text>().text = campo.Descricao;
                    tmpItem.transform.GetChild(1).GetComponent<InputField>().text = BuscarValorDetalhe(componente.Detalhes, campo.Nome);

                    break;
                case "numero":
                    itemC = GameObject.Find("itemTexto");

                    tmpItem = Instantiate(itemC, parentC.transform);
                    tmpItem.name = campo.Nome;
                    tmpItem.transform.GetChild(0).GetComponent<Text>().text = campo.Descricao;
                    tmpItem.transform.GetChild(1).GetComponent<InputField>().text = BuscarValorDetalhe(componente.Detalhes, campo.Nome);
                   
                    break;
                case "decimal":
                    itemC = GameObject.Find("itemTexto");

                    tmpItem = Instantiate(itemC, parentC.transform);
                    tmpItem.name = campo.Nome;
                    tmpItem.transform.GetChild(0).GetComponent<Text>().text = campo.Descricao;
                    tmpItem.transform.GetChild(1).GetComponent<InputField>().text = BuscarValorDetalhe(componente.Detalhes, campo.Nome);
                   
                    break;
                case "boleano":
                    itemC = GameObject.Find("itemBolean");

                    tmpItem = Instantiate(itemC, parentC.transform);
                    tmpItem.name = campo.Nome;
                    tmpItem.transform.GetChild(0).GetComponent<Toggle>().GetComponentInChildren<Text>().text = campo.Descricao;
                    tmpItem.transform.GetChild(0).GetComponent<Toggle>().isOn = BuscarValorDetalhe(componente.Detalhes, campo.Nome) == "true";
                    break;
                default:
                    itemC = GameObject.Find("itemTexto");

                    tmpItem = Instantiate(itemC, parentC.transform);
                    tmpItem.name = campo.Nome;
                    tmpItem.transform.GetChild(0).GetComponent<Text>().text = campo.Descricao;
                    tmpItem.transform.GetChild(1).GetComponent<InputField>().text = BuscarValorDetalhe(componente.Detalhes, campo.Nome);

                    break;
            }

        }
    } 

    public void Abrirlogs()
    {
        string id = GameObject.Find("itemEdit").GetComponent<Text>().text;
        if (Parametros.IsInventarioEdit)
        {
            id = Parametros.InventarioId;
        }

        Componente componente = InventarioModel.GetComponente(id);

        GameObject gridLogs = GameObject.Find("LogsContent");
        for (int i = 0; i < gridLogs.transform.childCount; i++)
        {
            Destroy(gridLogs.transform.GetChild(i).gameObject);
        }

        GameObject itemLog = GameObject.Find("ItemLogs");

        componente.Logs.ForEach(x => {

            GameObject tmpItem = Instantiate(itemLog, gridLogs.transform);

            tmpItem.transform.GetChild(0).GetComponent<Text>().text = x.Data.ToString("dd/MM/yyy");
            tmpItem.transform.GetChild(1).GetComponent<Text>().text = x.Texto;

        });

    }

    public void InserirLog()
    {
        Log log = new Log();
        log.Data = DateTime.Now;
        log.Texto = GameObject.Find("inpLog").GetComponent<InputField>().text;

        string id = GameObject.Find("itemEdit").GetComponent<Text>().text;
        if (Parametros.IsInventarioEdit)
            id = Parametros.InventarioId;

        Componente componente = InventarioModel.GetComponente(id);
        componente.Logs.Add(log);
        InventarioModel.PutComponente(componente, id);

        GameObject.Find("inpLog").GetComponent<InputField>().text = "";

        Abrirlogs();
    }

    private static void LimpaCmpos()
    {
        GameObject.Find("inpNome").GetComponent<InputField>().text = "";
        GameObject.Find("inpTipo").GetComponent<Dropdown>().value = 0;
        GameObject.Find("inpMarcador").GetComponent<Dropdown>().value = 0;
        GameObject.Find("inpProjecao").GetComponent<Dropdown>().value = 0;

        GameObject.Find("inpModelo").GetComponent<InputField>().text = "";
        GameObject.Find("inpFabricante").GetComponent<InputField>().text = "";
        GameObject.Find("inpFuncao").GetComponent<InputField>().text = "";

        GameObject.Find("itemEdit").GetComponent<Text>().text = "";

        Parametros.InventarioId = "";
        Parametros.IsInventarioEdit = false;

        GameObject.Find("btnSalvar")?.gameObject?.SetActive(true);
        GameObject.Find("btnAtualizar")?.gameObject?.SetActive(false);
    }



}
