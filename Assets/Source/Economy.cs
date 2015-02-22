using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoogleFu;

public class Economy
{
    // money
    public int profit;

    //population
    public int population;
    public int reputation;
    public int jobs;

    //services
    public int power;
    public int health;
    public int police;
    public int fire;
    
    public Economy()
    {
        profit = 0;
        population = 0;
        reputation = 0;
        power = 0;
        jobs = 0;
        health = 0;
        police = 0;
        fire = 0;
    }

    public void ApplyConfig(TileDBRow config)
    {
        profit = config._profit;
        population = config._pop;
        reputation = config._rep;
        power = config._power;
        jobs = config._jobs;
        health = config._health;
        police = config._police;
        fire = config._fire;
    }

    public static Economy operator +(Economy e1, Economy e2)
    {
        Economy n = new Economy();
        n.profit = e1.profit + e2.profit;
        n.population = e1.population + e2.population;
        n.reputation = e1.reputation + e2.reputation;
        n.power = e1.power + e2.power;
        n.jobs = e1.jobs + e2.jobs;
        n.health = e1.health + e2.health;
        n.police = e1.police + e2.police;
        n.fire = e1.fire + e2.fire;
        return n;
    }
}
