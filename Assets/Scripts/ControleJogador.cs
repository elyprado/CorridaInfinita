using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleJogador : MonoBehaviour
{
    public Rigidbody jogador;
    public GameObject cenario; 
    public float velocidadeJogador;
    public float velocidadeCenario;
    private float distanciaRaia = 2.5F;
    private int raiaAtual = 1;
    private Vector3 target = new Vector3();
    private Vector2 initialPosition;

    void Start()
    {
        target = jogador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        int novaRaia = -1;

        //teclado
        if (Input.GetKeyDown(KeyCode.RightArrow) && raiaAtual<2) {
            novaRaia=raiaAtual+1;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && raiaAtual>0) {
            novaRaia=raiaAtual-1;
        }
       

        //mouse
        if (Input.GetMouseButtonDown(0)) {
            initialPosition = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            if (Input.mousePosition.x > initialPosition.x && raiaAtual<2) {
                novaRaia=raiaAtual+1;
            } else if (Input.mousePosition.x < initialPosition.x && raiaAtual>0) {
                novaRaia=raiaAtual-1;
            }
        }

        //touch
        if(Input.touchCount >= 1) {
            if(Input.GetTouch(0).phase == TouchPhase.Began) {
                initialPosition = Input.GetTouch(0).position;
            } else if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary){
                if (Input.GetTouch(0).position.x > initialPosition.x && raiaAtual<2) {
                    novaRaia=raiaAtual+1;
                } else if (Input.GetTouch(0).position.x < initialPosition.x && raiaAtual>0) {
                    novaRaia=raiaAtual-1;
                }
            }
        }
        
        if (novaRaia>=0) {
            raiaAtual = novaRaia;
            target = new Vector3((raiaAtual-1)*distanciaRaia, jogador.transform.position.y, jogador.transform.position.z);
            //jogador.transform.position = pos;
        }
        if (jogador.transform.position.x != target.x) {
            //LERP - posição original, posição de destino, velocidade do movimento
            jogador.transform.position = Vector3.Lerp(jogador.transform.position, target, 5*Time.deltaTime);
        }

        //move o chao
        cenario.transform.Translate(0,0,velocidadeCenario * Time.deltaTime * -1);
    }
}
