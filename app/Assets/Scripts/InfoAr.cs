using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoAr : MonoBehaviour
{
    public static string IdItem { get; set; }

    public void UnLoad(GameObject target)
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Transform child = target.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public void LoadCapa(GameObject target)
    {

        string marcador = target.name;
        var item = InventarioModel.GetComponeteByTarget(marcador);

        if (item == null) 
        {
            return;
        }


        //disable any pre-existing augmentation
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Transform child = target.transform.GetChild(i);
            child.gameObject.SetActive(false);
            //cube = child.gameObject;
        }
        GameObject cubeModelo = GameObject.Find("Canvas");
        GameObject cube = Instantiate(cubeModelo, target.transform);
        cube.transform.localPosition = new Vector3(5, 0, 0);
        cube.transform.localRotation = Quaternion.Euler(90,0,0);

        float projecao = 0.05f;
        if (item.Projecao == 2)
            projecao = 0.12f;
        else if (item.Projecao == 1)
            projecao = 0.09f;

        cube.transform.localScale = new Vector3(projecao, projecao, projecao);
        // Make sure it is active
        cube.SetActive(true);


        IdItem = item._id;
        var ident = cube.transform.Find("Panel").Find("indetificacao");

        ident.Find("txtNome").GetComponent<Text>().text = item.Nome;
        ident.Find("txtModelo").GetComponent<Text>().text = item.Modelo;
        ident.Find("txtFabricante").GetComponent<Text>().text = item.Fabricante;
        ident.Find("txtTipo").GetComponent<Text>().text = item.Tipo;
        ident.Find("txtFuncao").GetComponent<Text>().text = item.Funcao;

    }

    public void LoadDetalhe1(GameObject btnDetalhe)
    {
        btnDetalhe.transform.parent.Find("btnCapa").gameObject.SetActive(true);
        btnDetalhe.transform.parent.Find("btnDetalhes").gameObject.SetActive(false);

        btnDetalhe.transform.parent.Find("indetificacao").gameObject.SetActive(false);
        btnDetalhe.transform.parent.Find("detalhes").gameObject.SetActive(true);

        var marcador = btnDetalhe.transform.parent.parent.parent.gameObject.name;
        var item = InventarioModel.GetComponeteByTarget(marcador);

        var detalhes = btnDetalhe.transform.parent.Find("detalhes");


        var campos = InventarioModel.GetCamposComponente(item.Tipo);

        detalhes.Find("txtNomeD").GetComponent<Text>().text = item.Nome;

        GameObject parentC = detalhes.Find("GridDetalhes").Find("Viewport").Find("detalhesContent").gameObject;
        for (int i = 0; i < parentC.transform.childCount; i++)
        {
            Destroy(parentC.transform.GetChild(i).gameObject);
        }

        foreach (var campo in campos)
        {
            GameObject itemC, tmpItem;

            itemC = btnDetalhe.transform.parent.Find("itemDetalhe").gameObject;
            var valor = BuscarValorDetalhe(item.Detalhes, campo.Nome);

            if (string.IsNullOrEmpty(valor))
                continue;

            tmpItem = Instantiate(itemC, parentC.transform);
            tmpItem.name = campo.Nome;
            tmpItem.transform.GetChild(0).GetComponent<Text>().text = campo.Descricao;
            tmpItem.transform.GetChild(1).GetComponent<Text>().text = valor;
        }

    }


    private string BuscarValorDetalhe(List<Detalhe> detalhes, string campo)
    {
        foreach (var detalhe in detalhes)
        {
            if (detalhe.Key == campo)
            {
                return detalhe.Value;
            }
        }
        return "";
    }

    public void SetParams()
    {
        Parametros.IsInventarioEdit = true;
        Parametros.InventarioId = IdItem;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LoadScenes loadScenes = new LoadScenes();
            loadScenes.OpenMain();
        }
    }

}
