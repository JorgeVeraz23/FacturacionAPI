﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FacturacionAPI.Models;

[Table("DetalleFactura")]
public partial class DetalleFactura
{
    [Key]
    public int IdItem { get; set; }

    public int? IdFactura { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string? CodigoProducto { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string NombreProducto { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Precio { get; set; }

    public int Cantidad { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Subtotal { get; set; }

    public virtual Producto? CodigoProductoNavigation { get; set; }

    [ForeignKey("IdFactura")]
    [InverseProperty("DetalleFacturas")]
    public virtual Factura? IdFacturaNavigation { get; set; }
}
