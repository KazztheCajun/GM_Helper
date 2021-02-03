using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : IComparable<Creature>
{
		public bool isPlayer;
		public string name;
		public int health;
		public int initiative;
		public String[] effects;
		public bool isDead;

		public Creature(bool pc, String name, int i)
		{
			this.isPlayer = pc;
			this.name = name.Trim();
			this.effects = new string[10];
			this.isDead = false;
			this.health = 0;
			this.initiative = i;
		}

		public void IsHit(int dmg)
		{
			this.health += dmg;
		}

		public void IsDead(bool b)
		{
			this.isDead = b;
		}

		public int CompareTo(Creature o) // this compareTo is opposite standard Comparable contract
		{
			if (o == null)
        	{
        	    return -1;
        	}

			return -this.initiative.CompareTo(o.initiative);
		}

		public int CompareTo(System.Object o) // this compareTo is opposite standard Comparable contract
		{
			Creature other = o as Creature;
       
			if (other == null)
        	{
        	    return -1;
        	}

			int test = -this.initiative.CompareTo(other.initiative);

			if (test != 0)
			{
				return test;
			}
			
			if (this.isPlayer && !other.isPlayer)
			{
				return -1;
			}
			else if (!this.isPlayer && other.isPlayer)
			{
				return 1 ;
			}
			else
			{
				var rand = new System.Random();
				return (rand.Next(2) * -rand.Next(2));
			} 
		}

		public override string ToString()
		{
			if (this.isPlayer)
			{
				return string.Format("%s", this.name);
			}
			else 
			{
				return string.Format("%s %d", this.name, this.initiative);
			}
		}

		public override bool Equals(System.Object o) 
		{
			if (o == null) {
				return false;
			}
			if (o == this) {
				return true;
			}
			
			Creature d = (Creature) o;
			if (d==null) {return false;}
			return (this.name == d.name);
		}

		
		public override int GetHashCode()
		{
			return name.GetHashCode();
		}
    
}
