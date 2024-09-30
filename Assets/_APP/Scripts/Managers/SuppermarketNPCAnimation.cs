using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppermarketNPCAnimation : MonoBehaviour
{
    public void PlayNpcExposure1()
    {
        SupermarketGameManager.Instance.Npc1Talk();
    }


    public void PlayNpcExposure2()
    {
        SupermarketGameManager.Instance.Npc2Talk();
    }

    public void PlayNpcExposure3()
    {
        SupermarketGameManager.Instance.Npc3Talk();
    }

    public void PlayNpcExposure4()
    {
        SupermarketGameManager.Instance.Npc4Talk();
    }
}