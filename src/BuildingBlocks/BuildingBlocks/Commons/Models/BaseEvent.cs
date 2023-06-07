namespace BuildingBlocks.Commons.Models;
public abstract class BaseEvent
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }

    public BaseEvent()
    {
        Id = Guid.NewGuid();
        CreationDate = DateTime.Now;    
    }

    public BaseEvent(Guid id, DateTime dateTime)
    {
        Id = id;
        CreationDate = dateTime;    
    }
}
