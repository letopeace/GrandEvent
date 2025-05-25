using System;
using System.Collections.Generic;
using System.Linq;

public class Revolver
{
	private static Random random = new Random();
	private bool[] drum = new bool[6];

    public Revolver(bool[] drum)
    {
        if (drum.Length != 6)
        {
            throw new System.Exception("Drum bullets out of range");
        }

        this.drum = drum;
    }

    public Revolver(int count)
    {
		if (count > 6) count = 6;
		if (count < 0) count = 0;

		bool[] newDrum = new bool[6];

		List<int> indices = Enumerable.Range(0, 6).ToList();

		indices = indices.OrderBy(x => random.Next()).ToList();

		for (int i = 0; i < count; i++)
		{
			newDrum[indices[i]] = true;
		}

		this.drum = newDrum;

	}



    public void Randomize()
    {
        int repeatCount = random.Next(1, 7);

        Spin(repeatCount);
	}

    public void Spin(int repeatCount)
    {
		for (int i = 0; i < repeatCount; i++)
		{
			Iteration();
		}
	}

    public bool Shoot()
    {
        bool res = drum[0];
        drum[0] = false;
        Iteration();

        return res;
    }

    public bool[] Show()
    {
		return (bool[])drum.Clone();
	}

    public int CountOfRemainBullet()
    {
        int res = 0;
        foreach (bool b in drum)
        {
            res = b ? res++ : res;
        }

        return res;
    }

    private void Iteration()
    {
        bool[] newDrum = new bool[6];

        for (int i = 0; i < 5; i++)
        {
            newDrum[i] = drum[i+1];

		}
        newDrum[5] = drum[0];

        drum = newDrum;
    }

}
