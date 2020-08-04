using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioView : MonoBehaviour
{
    public GameObject Item, ItemParent;
    void Start()
    {
        if (Item != null && ItemParent != null)
        {
            new InventarioController(Item, ItemParent).Read();

            if (Parametros.IsInventarioEdit)
            {
                //GameObject.Find("itemEdit").GetComponent<Text>().text = Parametros.InventarioId;
                GameObject.Find("btnEdit").GetComponent<Button>().onClick.Invoke();
            }
        }
    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void AtivarFormulario()
    {
    
        List<string> tipoEquipamentos = new List<string>();
        var objetos = ObjetosModel.GetAllObjetos();
        if (objetos != null)
        {
            objetos.ForEach(x =>
            {
                tipoEquipamentos.Add(x.Nome);
            });
        }

        if (GameObject.Find("inpTipo").GetComponent<Dropdown>().options.Count < tipoEquipamentos.Count)
            GameObject.Find("inpTipo").GetComponent<Dropdown>().AddOptions(tipoEquipamentos);

        var sdadas = Marcador.Marcadores.Count;
        var asdjasndja = GameObject.Find("inpMarcador").GetComponent<Dropdown>();
        if (GameObject.Find("inpMarcador").GetComponent<Dropdown>().options.Count < sdadas) 
        { 
            GameObject.Find("inpMarcador").GetComponent<Dropdown>().AddOptions(Marcador.Marcadores);
        }





    }
}
