using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetosController : MonoBehaviour
{
    public GameObject ItemParent { get; set; }
    public GameObject Item { get; set; }
    private GameObject ItemEdit { get; set; }


    public ObjetosController(GameObject item, GameObject itemParent)
    {
        Item = item;
        ItemParent = itemParent;
    }

    public void Read()
    {

        for (int i = 0; i< ItemParent.transform.childCount; i++)
        {
            Destroy(ItemParent.transform.GetChild(i).gameObject);
        }

        List<Objeto> objetos = ObjetosModel.GetAllObjetos();
        objetos.ForEach(x => {

            GameObject tmpItem = Instantiate(Item, ItemParent.transform);

            tmpItem.name = x._id;
            tmpItem.transform.GetChild(0).GetComponent<Text>().text = x.Descricao;
            tmpItem.transform.GetChild(1).GetComponent<Text>().text = x.AR? "Sim": "Não";

        });
    }

    public void AddCampo()
    {

        GameObject item = GameObject.Find("itemCampos");
        GameObject parent = GameObject.Find("ContentGridCampos");

        GameObject tmpItem = Instantiate(item, parent.transform);

        Campo campo = new Campo();
        campo.Nome = GameObject.Find("inpNomeCampo").GetComponent<InputField>().text;
        campo.Descricao = GameObject.Find("inpDescricaoCampo").GetComponent<InputField>().text;
        campo.Tipo = GetTipoById(GameObject.Find("inpTipoCampo").GetComponent<Dropdown>().value);

        tmpItem.name = campo.Nome;
        tmpItem.transform.GetChild(0).GetComponent<Text>().text = campo.Descricao;
        tmpItem.transform.GetChild(1).GetComponent<Text>().text = campo.Tipo;

        GameObject.Find("inpNomeCampo").GetComponent<InputField>().text = "";
        GameObject.Find("inpDescricaoCampo").GetComponent<InputField>().text = "";
        GameObject.Find("inpTipoCampo").GetComponent<Dropdown>().value = 0;
    }

    public string GetTipoById(int id)
    {
        switch (id)
        {
            case 0: return "texto";
            case 1: return "numero";
            case 2: return "decimal";
            case 3: return "boleano";
            default: return "texto";
        }
    }

    public void Gravar()
    {
        try
        {
            Objeto objeto = new Objeto();

            objeto.Nome = GameObject.Find("inpNome").GetComponent<InputField>().text;
            objeto.Descricao = GameObject.Find("inpDescricao").GetComponent<InputField>().text;
            objeto.AR = GameObject.Find("inpAR").GetComponent<Toggle>().isOn;

            objeto.Campos = LerGridCampos();

            var result = ObjetosModel.PostObjeto(objeto);

            LimpaCampos();
            Read();
        }
        catch (Exception ex)
        {

        }
    }


    private List<Campo> LerGridCampos()
    {
        List<Campo> campos = new List<Campo>();
        GameObject gridCampos = GameObject.Find("ContentGridCampos");
        for (int i = 0; i < gridCampos.transform.childCount; i++)
        {
            var item = gridCampos.transform.GetChild(i).gameObject;

            campos.Add(new Campo
            {
                Nome = item.name,
                Descricao = item.transform.GetChild(0).GetComponent<Text>().text,
                Tipo = item.transform.GetChild(1).GetComponent<Text>().text
            });
        }
        return campos;
    }

    public void Delete(GameObject gameObject)
    {
        string id = gameObject.name;
        bool isOk = ObjetosModel.DeleteObjeto(id);

        if (isOk)
        {
            Destroy(gameObject);
        }
    }
    public void DeleteCampo(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    public void PreencherCampos(GameObject itemEdit)
    {
        ItemEdit = itemEdit;
        string id = itemEdit.name;
        Objeto objeto = ObjetosModel.GetObjeto(id);

        GameObject.Find("itemEdit").GetComponent<Text>().text = objeto._id;

        GameObject.Find("inpNome").GetComponent<InputField>().text = objeto.Nome;
        GameObject.Find("inpDescricao").GetComponent<InputField>().text = objeto.Descricao;
        GameObject.Find("inpAR").GetComponent<Toggle>().isOn = objeto.AR;


     

        // adiciona os campos na grid
        GameObject itemC = GameObject.Find("itemCampos");
        GameObject parentC = GameObject.Find("ContentGridCampos");

        for (int i = 0; i < parentC.transform.childCount; i++)
        {
            Destroy(parentC.transform.GetChild(i).gameObject);
        }

        objeto.Campos.ForEach(x => {

            GameObject tmpItem = Instantiate(itemC, parentC.transform);

            tmpItem.name = x.Nome;
            tmpItem.transform.GetChild(0).GetComponent<Text>().text = x.Descricao;
            tmpItem.transform.GetChild(1).GetComponent<Text>().text = x.Tipo;
        });

    }

    public void Editar()
    {
        try
        {
            string id = GameObject.Find("itemEdit").GetComponent<Text>().text;
            Objeto objeto = ObjetosModel.GetObjeto(id);

            objeto.Nome = GameObject.Find("inpNome").GetComponent<InputField>().text ?? objeto.Nome;
            objeto.Descricao = GameObject.Find("inpDescricao").GetComponent<InputField>().text ?? objeto.Descricao;
            objeto.AR = GameObject.Find("inpAR").GetComponent<Toggle>().isOn;
            
            objeto.Campos = LerGridCampos();

            var result = ObjetosModel.PutObjeto(objeto, id);

            LimpaCampos();
            Read();
        }
        catch (Exception ex)
        {

        }
    }

    private static void LimpaCampos()
    {
        GameObject.Find("inpNome").GetComponent<InputField>().text = "";
        GameObject.Find("inpDescricao").GetComponent<InputField>().text = "";
        GameObject.Find("inpAR").GetComponent<Toggle>().isOn = false;

        GameObject.Find("itemEdit").GetComponent<Text>().text = "";

        GameObject.Find("inpNomeCampo").GetComponent<InputField>().text = "";
        GameObject.Find("inpDescricaoCampo").GetComponent<InputField>().text = "";
        GameObject.Find("inpTipoCampo").GetComponent<Dropdown>().value = 0;


        GameObject.Find("btnSalvar")?.gameObject?.SetActive(true);
        GameObject.Find("btnAtualizar")?.gameObject?.SetActive(false);
    }


}
