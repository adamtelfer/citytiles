using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Economy
{
    public int profit;
    public int population;
    public int reputation;
    public int power;
    public int jobs;

    public Economy()
    {
        profit = 0;
        population = 0;
        reputation = 0;
        power = 0;
        jobs = 0;
    }

    public static Economy operator +(Economy e1, Economy e2)
    {
        Economy n = new Economy();
        n.profit = e1.profit + e2.profit;
        n.population = e1.population + e2.population;
        n.reputation = e1.reputation + e2.reputation;
        n.power = e1.power + e2.power;
        n.jobs = e1.jobs + e2.jobs;
        return n;
    }
}
