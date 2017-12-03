using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour {

    [SerializeField]
    private int scene = 0;
    
    //Carregar tela
    public void loadScene () {

        //Executa o SceneManager
        SceneManager.LoadScene(scene);
	}
}
