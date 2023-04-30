using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Domain.Entities;

[Table("FuelTypes")]
public class FuelType
{
    [Key] public int Id { get; set; }

    public string? Name { get; set; }
    public string? ShortName { get; set; }
}
