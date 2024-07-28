using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem
{
    private bool bed;
    public int food;

    public void UseBed()
    {
        if (bed)
        {

        }
    }

    public void UseFood()
    {
        if(food > 0)
        {
            food--;
        }
    }

    public void AddBed()
    {
        bed = true;
    }
    public void AddFood(int addingFood)
    {
        food += addingFood;
    }
}
