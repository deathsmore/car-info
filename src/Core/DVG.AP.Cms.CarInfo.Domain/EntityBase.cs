using System.ComponentModel.DataAnnotations;

namespace DVG.AP.Cms.CarInfo.Domain
{
    public abstract class EntityBase<T>
    {
     [Key]
      public T Id { get; set; }
        
    }
}