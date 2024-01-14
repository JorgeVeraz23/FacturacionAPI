﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FacturacionAPI.Models;

[Index("Codigo", Name = "UQ__Producto__06370DAC840DC866", IsUnique = true)]
public partial class Producto
{
    [Key]
    public int IdProducto { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string Codigo { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Nombre { get; set; } = null!;

    public int? IdFamilia { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public bool? Activo { get; set; }

    [Column(TypeName = "date")]
    public DateTime? FechaCreacion { get; set; }

    public int? IdUsuario { get; set; }

    public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();

    [ForeignKey("IdFamilia")]
    [InverseProperty("Productos")]
    public virtual FamiliaProducto? IdFamiliaNavigation { get; set; }

    [ForeignKey("IdUsuario")]
    [InverseProperty("Productos")]
    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
