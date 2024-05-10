using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class optionsMenu : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PantallaCompleta(bool PantallaCompleta){
        Screen.fullScreen = PantallaCompleta;
    }

    public void CambiarVolumen(float volumen){
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void CambiarCalidad(int index){
        QualitySettings.SetQualityLevel(index);
    }
}
