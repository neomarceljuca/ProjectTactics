using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public int damage;
    public int manaCost;
    public int reach;
    public int energyCost;
    public Sprite icon;
    public Animator myAnimator;
    public float animationTime;
    public bool canTargetSelf;

    private Vector3 posOffset;

    public void Awake()
    {
        myAnimator = StateMachineController.instance.skillFXAnimator;  //transform.parent.GetComponentInChildren<Animator>();
        posOffset = new Vector3(0f,0.5f);
    }


    public bool CanUse(){  //apenas checar se pode usar. Reducao de mana e demais acoes em perform state
        if (Turn.unit.GetStat(StatEnum.MP) >= manaCost)
            return true;
        return false;
    }

    public bool ValidadeTarget()
    {
        Unit unit = null;
        if (StateMachineController.instance.selectedTile.content != null) //checando e obtendo tile visado
            unit = StateMachineController.instance.selectedTile.content.GetComponent<Unit>();

        if (unit != null)
            return true;
        return false;


    }

    public List<TileLogic> GetTargets()
    {
        List<TileLogic> targets = new List<TileLogic>();

        targets.Add(StateMachineController.instance.selectedTile);
        return targets;
    }

    public void Effect()
    {

        for (int i = 0; i < Turn.targets.Count; i++)
        {
            Unit unit = Turn.targets[i].content.GetComponent<Unit>();
            if (unit != null)
            {

                Debug.LogFormat(" {0} estava com {1} de HP, foi afetado por {2}, recebeu a diferenca de {3}", 
                    unit ,unit.GetStat(StatEnum.HP), this.name, -damage, unit.GetStat(StatEnum.HP) - damage);

                string temp = string.Format(" {0} estava com {1} de HP, foi afetado por {2}, recebeu a diferenca de {3}",
                    unit, unit.GetStat(StatEnum.HP), this.name, -damage, unit.GetStat(StatEnum.HP) - damage);

                CombatLog.UpdateLog(temp);
                
                
                unit.SetStat(StatEnum.HP, -damage);
            }
        }
    }

    public void PlayAnimation(List<TileLogic> targets)
    {
        //not supported for multiple targets yet
        for (int i = 0; i < targets.Count; i++)
        {
            Unit unit = Turn.targets[i].content.GetComponent<Unit>();
            if (unit != null)
            {
                myAnimator.transform.position = unit.transform.position + posOffset;
                myAnimator.Play(this.gameObject.name);
                
            }
        }
    }

    void FilterContent()
    {
        Turn.targets.RemoveAll((x) => x.content == null);
    }
}
