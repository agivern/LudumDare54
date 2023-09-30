public class RaceDesire : Desire
{
    public AlienRace race;
    public float satisfactionLevel { get; private set; }

    public RaceDesire(AlienRace race, float satisfactionLevel)
    {
        this.race = race;
        this.satisfactionLevel = satisfactionLevel;
    }


    public float satisfaction(Alien alien)
    {
        if (alien.room.ContainsRace(race))
        {
            return satisfactionLevel;
        }
        else
        {
            return -1 * satisfactionLevel;
        }
    }
    
    public bool Likes()
    {
        return satisfactionLevel > 0;
    }
}