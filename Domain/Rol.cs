using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class Rol : BaseDomain
{
    public int IdRol {  get; set; }
    public string? Name { get; set; }
    public string? Descripcion { get; set; }
    public bool State { get; set; }

}
