using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    [SerializeField] private GameObject _botaoSair;

    private void Start()
    {
        #if UNITY_STANDALONE || UNITY_EDITOR
            _botaoSair.SetActive(true);
        #endif
    }

    public void IniciarJogo()
    {
        StartCoroutine(MudarCena("game"));
    }

    IEnumerator MudarCena(string name)
    {
        yield return new WaitForSecondsRealtime(0.3f);
        SceneManager.LoadScene(name);
    }

    public void SairJogo()
    {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
