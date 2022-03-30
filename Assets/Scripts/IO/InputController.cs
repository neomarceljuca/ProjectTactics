using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DelegateModel(object sender, object args);

public class InputController : MonoBehaviour
{
    float hCooldown = 0;
    float vCooldown = 0;

    float cooldownTimer = 0.5f;

    public static InputController instance;

    public DelegateModel OnMove;
    public DelegateModel OnFire;

    public DelegateModel TestOnPress;

    private void Awake()
    {
        instance = this;
    }



    private void Update()
    {
        int h = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        int v = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));


        Vector3Int moved = new Vector3Int(0, 0,0);


        if (h != 0)
        {
            moved.x = GetMoved(ref hCooldown, h);
        }
        else
        {
            hCooldown = 0;
        }

        if (v != 0)
        {
            moved.y = GetMoved(ref vCooldown, v);

        }
        else
        {
            vCooldown = 0;
        }

        if (moved != Vector3Int.zero && OnMove != null)
        {
                OnMove(null, moved); 
        }
            


        if (Input.GetButtonDown("Fire1") && OnFire != null)
        {
            OnFire(null, 1);
        }

        if (Input.GetButtonDown("Fire2") && OnFire != null)
        {
            OnFire(null, 2);
        }


        //ambiente de teste
        if (Input.GetButtonDown("F10") && TestOnPress != null)
        {
            TestOnPress(null, 1);
        }


        //fim do ambiente de teste

    }

    int GetMoved(ref float cooldownSum, int value)
    {
        if(Time.time > cooldownSum)
        {
            cooldownSum += Time.time + cooldownTimer;
            return value;
        }
        return 0;
    }


}
