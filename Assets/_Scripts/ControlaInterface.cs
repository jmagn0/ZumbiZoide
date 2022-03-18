using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{
    // Cached References
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _painelGameOver;
    [SerializeField] private Text _textoTempoSobrevivencia;
    [SerializeField] private Text _textoPontuacaoMaxima;
    [SerializeField] private Text _textoNumeroZumbisMortos;
    [SerializeField] private Text _textoChefeAparece;
    private float _tempoPontuacaoSalvo;
    private int _quantidadeZumbisMortos;

    private void Start()
    {
        Time.timeScale = 1;
        _tempoPontuacaoSalvo = PlayerPrefs.GetFloat("pontuacaoMaxima");
    }

    public void AtualizarVida(int vida)
    {
        _slider.value = vida;
    }

    public void GameOver()
    {
        _painelGameOver.SetActive(true);
        Time.timeScale = 0;

        int minutos = (int)Time.timeSinceLevelLoad / 60;
        int segundos = (int)Time.timeSinceLevelLoad % 60;
        _textoTempoSobrevivencia.text = $"Você sobreviveu por\n{minutos}min e {segundos}seg";

        AjustarPontuacaoMaxima(minutos, segundos);
    }

    public void AtualizarNumeroZumbisMortos()
    {
        _quantidadeZumbisMortos++;
        _textoNumeroZumbisMortos.text = string.Format("x {0}", _quantidadeZumbisMortos);
    }

    void AjustarPontuacaoMaxima(int minuto, int segundos)
    {
        if(Time.timeSinceLevelLoad > _tempoPontuacaoSalvo)
        {
            _tempoPontuacaoSalvo = Time.timeSinceLevelLoad;
            _textoPontuacaoMaxima.text = string.Format("Seu melhor tempo é\n {0}min e {1}seg", minuto, segundos);
            PlayerPrefs.SetFloat("pontuacaoMaxima", _tempoPontuacaoSalvo);
        }
        else
        {
            minuto = (int)_tempoPontuacaoSalvo / 60;
            segundos = (int)_tempoPontuacaoSalvo % 60;
            _textoPontuacaoMaxima.text = string.Format("Seu melhor tempo é\n {0}min e {1}seg", minuto, segundos);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("game");
    }

    public void MostrarTextoChefeCriado()
    {
        StartCoroutine(DesaparecerTexto(2, _textoChefeAparece));
    }

    IEnumerator DesaparecerTexto(float tempoSumir, Text texto)
    {
        texto.gameObject.SetActive(true);
        Color corTexto = texto.color;
        corTexto.a = 1;
        texto.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while(texto.color.a > 0)
        {
            contador += Time.deltaTime / tempoSumir;
            corTexto.a = Mathf.Lerp(1, 0, contador);
            texto.color = corTexto;
            yield return null;
        }
        texto.gameObject.SetActive(false);
    }
}
