namespace SistemaTienda.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class crecer : DbContext
    {
        public crecer()
            : base("name=crecer")
        {
        }

        public virtual DbSet<tblCargo> tblCargo { get; set; }
        public virtual DbSet<tblCategoria> tblCategoria { get; set; }
        public virtual DbSet<tblCliente> tblCliente { get; set; }
        public virtual DbSet<tblCompra> tblCompra { get; set; }
        public virtual DbSet<tblCxC> tblCxC { get; set; }
        public virtual DbSet<tblCxP> tblCxP { get; set; }
        public virtual DbSet<tblDevoluciones> tblDevoluciones { get; set; }
        public virtual DbSet<tblEmpleado> tblEmpleado { get; set; }
        public virtual DbSet<tblKardex> tblKardex { get; set; }
        public virtual DbSet<tblMetodoPago> tblMetodoPago { get; set; }
        public virtual DbSet<tblProducto> tblProducto { get; set; }
        public virtual DbSet<tblProveedor> tblProveedor { get; set; }
        public virtual DbSet<tblVenta> tblVenta { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblCargo>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tblCargo>()
                .HasMany(e => e.tblEmpleado)
                .WithOptional(e => e.tblCargo)
                .HasForeignKey(e => e.id_cargo);

            modelBuilder.Entity<tblCategoria>()
                .Property(e => e.nombre_categoria)
                .IsUnicode(false);

            modelBuilder.Entity<tblCategoria>()
                .HasMany(e => e.tblProducto)
                .WithOptional(e => e.tblCategoria)
                .HasForeignKey(e => e.id_categoria);

            modelBuilder.Entity<tblCliente>()
                .Property(e => e.nombre_compañia)
                .IsUnicode(false);

            modelBuilder.Entity<tblCliente>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tblCliente>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblCliente>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tblCliente>()
                .Property(e => e.ruta_imagen)
                .IsUnicode(false);

            modelBuilder.Entity<tblCliente>()
                .HasMany(e => e.tblVenta)
                .WithRequired(e => e.tblCliente)
                .HasForeignKey(e => e.id_cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblCompra>()
                .HasMany(e => e.tblDevoluciones)
                .WithOptional(e => e.tblCompra)
                .HasForeignKey(e => e.id_compra);

            modelBuilder.Entity<tblCompra>()
                .HasMany(e => e.tblCxP)
                .WithOptional(e => e.tblCompra)
                .HasForeignKey(e => e.id_compra);

            modelBuilder.Entity<tblDevoluciones>()
                .HasMany(e => e.tblKardex)
                .WithOptional(e => e.tblDevoluciones)
                .HasForeignKey(e => e.id_devolucion);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.apellido)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.dui)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.nit)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .Property(e => e.ruta_imagen)
                .IsUnicode(false);

            modelBuilder.Entity<tblEmpleado>()
                .HasMany(e => e.tblCompra)
                .WithRequired(e => e.tblEmpleado)
                .HasForeignKey(e => e.id_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblEmpleado>()
                .HasMany(e => e.tblVenta)
                .WithRequired(e => e.tblEmpleado)
                .HasForeignKey(e => e.id_empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblMetodoPago>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tblMetodoPago>()
                .HasMany(e => e.tblCompra)
                .WithRequired(e => e.tblMetodoPago)
                .HasForeignKey(e => e.id_metodopago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblMetodoPago>()
                .HasMany(e => e.tblVenta)
                .WithRequired(e => e.tblMetodoPago)
                .HasForeignKey(e => e.id_metodopago)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProducto>()
                .Property(e => e.nombre_producto)
                .IsUnicode(false);

            modelBuilder.Entity<tblProducto>()
                .Property(e => e.precio)
                .HasPrecision(7, 2);

            modelBuilder.Entity<tblProducto>()
                .Property(e => e.imagen_producto)
                .IsUnicode(false);

            modelBuilder.Entity<tblProducto>()
                .Property(e => e.descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<tblProducto>()
                .HasMany(e => e.tblCompra)
                .WithRequired(e => e.tblProducto)
                .HasForeignKey(e => e.id_producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProducto>()
                .HasMany(e => e.tblKardex)
                .WithOptional(e => e.tblProducto)
                .HasForeignKey(e => e.id_producto);

            modelBuilder.Entity<tblProducto>()
                .HasMany(e => e.tblVenta)
                .WithRequired(e => e.tblProducto)
                .HasForeignKey(e => e.id_producto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.nombre_contacto)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.cargo_contacto)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.direccion)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .Property(e => e.ruta_imagen)
                .IsUnicode(false);

            modelBuilder.Entity<tblProveedor>()
                .HasMany(e => e.tblCompra)
                .WithRequired(e => e.tblProveedor)
                .HasForeignKey(e => e.id_proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tblVenta>()
                .HasMany(e => e.tblCxC)
                .WithOptional(e => e.tblVenta)
                .HasForeignKey(e => e.id_venta);

            modelBuilder.Entity<tblVenta>()
                .HasMany(e => e.tblDevoluciones)
                .WithOptional(e => e.tblVenta)
                .HasForeignKey(e => e.id_venta);
        }
    }
}
