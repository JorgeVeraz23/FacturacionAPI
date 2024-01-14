using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FacturacionAPI.Models;

[Index("Codigo", Name = "UQ__FamiliaP__06370DACA562246A", IsUnique = true)]
public partial class FamiliaProducto
{
    [Key]
    public int IdFamilia { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    public bool? Activo { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaCreacion { get; set; }

    public int? IdUsuario { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("FamiliaProductos")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }

    [InverseProperty("IdFamiliaNavigation")]
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
