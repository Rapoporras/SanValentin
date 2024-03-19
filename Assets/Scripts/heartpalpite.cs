using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class heartpalpite : MonoBehaviour
{

    public Image imagenPalpitar;
    public float escalaMaxima = 1.2f;
    public float duracionPalpitacion = 0.5f;

    private void Start()
    {
        // Iniciar la animación de palpitar
        Palpitar();
    }

    void Palpitar()
    {
        // Escala original de la imagen
        Vector3 escalaOriginal = imagenPalpitar.transform.localScale;

        // Palpitación de la imagen usando DoTween
        imagenPalpitar.transform.DOScale(escalaMaxima, duracionPalpitacion / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                imagenPalpitar.transform.DOScale(escalaOriginal, duracionPalpitacion / 2)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() => Palpitar()); // Llamada recursiva para palpitación continua
            });
    }
}
