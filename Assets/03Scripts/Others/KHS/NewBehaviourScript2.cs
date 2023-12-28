using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript2 : MonoBehaviour
{
    // Start is called before the first frame update
    float version = 1.0f;
    int standard = 10000;
    int process = 0;
    int rank = 1;
    int hpbar = 300;
    int currentlength = 300;
    int f = 30;
    int damagecount = 0;

    [SerializeField, Range(0, 1000)]
    private int hp = 300;
    void Start()
    {
        
        
        bool newupdateavailable = false;
        bool damagekind1 = false;
        bool damagekind2 = false;
        bool damagekind3 = false;
        bool damagekind4 = false;
        bool damagekind5 = false;
        bool damagekind6 = false;
        bool damageOX = false;

        
        if (process >= standard)
        {
            process = process - standard;
            standard = standard + 5000;
            rank = rank + 1;
        }
        
        if (newupdateavailable == true)
        {
            float subtractresult = version % 1;
            if (subtractresult == 0.5)
            {
                version = version + 0.5f;
            }
            else
            {
                version = version + 0.1f;
            }

            if(damagekind1==true)
            {
                damageOX = true;
                hp = hp - 10;
                currentlength = currentlength - 10;
                damagekind1 = false;
            }
            if (damagekind2 == true)
            {
                damageOX = true;
                hp = hp - 20;
                currentlength = currentlength - 10;
                damagekind2 = false;
            }
            if (damagekind3 == true)
            {
                damageOX = true;
                hp = hp - 30;
                currentlength = currentlength - 10;
                damagekind3 = false;
            }
            if (damagekind4 == true)
            {
                damageOX = true;
                hp = hp - 40;
                currentlength = currentlength - 10;
                damagekind4 = false;
            }
            if (damagekind5 == true)
            {
                damageOX = true;
                hp = hp - 50;
                currentlength = currentlength - 10;
                damagekind5 = false;
            }
            if (damagekind6 == true)
            {
                damageOX = true;
                hp = hp - 60;
                currentlength = currentlength - 10;
                damagekind6 = false;
            }
            if (damageOX==true)
            {
                hpbar = currentlength;
                damageOX = false;
            }
            damagecount = 300 - hp;
            while (f<=damagecount/10)
            {
                Debug.Log("ㅣ");
            }
            if(currentlength<=0)
            {
                //game over
                Debug.Log("게임 오버");
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
