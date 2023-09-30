public class ObjectDesire : Desire
{
    public RoomObjectType type;
    public float satisfactionLevel { get; private set; }

    public ObjectDesire(RoomObjectType type, float satisfactionLevel)
    {
        this.type = type;
        this.satisfactionLevel = satisfactionLevel;
    }


    public float satisfaction(Alien alien)
    {
        if (alien.room.ContainsObject(type))
        {
            return satisfactionLevel;
        }
        else
        {
            return -1 * satisfactionLevel;
        }
    }
}