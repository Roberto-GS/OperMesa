using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Data;

/// <summary>
/// Contexto de base de datos de la Plataforma de Gestión Integral para Restaurantes.
/// Mapea las 29 tablas del script SQL Server mediante Fluent API.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ENTIDADES
    public DbSet<Configuracion> Configuraciones { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<RolPermiso> RolPermisos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Zona> Zonas { get; set; }
    public DbSet<Mesa> Mesas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }
    public DbSet<LoteIngrediente> LotesIngredientes { get; set; }
    public DbSet<ProductoIngrediente> ProductoIngredientes { get; set; }
    public DbSet<MovimientoStock> MovimientosStock { get; set; }
    public DbSet<Proveedor> Proveedores { get; set; }
    public DbSet<PedidoCompra> PedidosCompra { get; set; }
    public DbSet<PedidoCompraLinea> PedidoCompraLineas { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoMesa> PedidoMesas { get; set; }
    public DbSet<PedidoLinea> PedidoLineas { get; set; }
    public DbSet<MetodosPago> MetodosPago { get; set; }
    public DbSet<Factura> Facturas { get; set; }
    public DbSet<FacturaPagos> FacturaPagos { get; set; }
    public DbSet<CajaDiaria> CajasDiarias { get; set; }
    public DbSet<MovimientosCaja> MovimientosCaja { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<LogsAuditoria> LogsAuditoria { get; set; }

    // Vista de verificación de Stock
    public DbSet<StockRealVista> StockRealVistas { get; set; }

    /// <summary>
    /// Configuramos el mapeo de todas las entidades: tablas, columnas, relaciones,
    /// conversiones de enum, CHECK constraints, índices y valores por defecto.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. CONFIGURACIÓN
        modelBuilder.Entity<Configuracion>(static entidad =>
        {
            entidad.ToTable("Configuracion");
            entidad.HasKey(c => c.Id);

            entidad.Property(c => c.Clave).HasMaxLength(80).IsRequired();
            entidad.HasIndex(c => c.Clave).IsUnique();

            entidad.Property(c => c.Valor).HasMaxLength(500).IsRequired();
            entidad.Property(c => c.Descripcion).HasMaxLength(200);
            entidad.Property(c => c.FechaModificacion).HasDefaultValueSql("GETUTCDATE()");
        });


        // 2. SEGURIDAD Y USUARIOS
        modelBuilder.Entity<Rol>(entidad =>
        {
            entidad.ToTable("Roles");
            entidad.HasKey(r => r.Id);

            entidad.Property(r => r.Nombre).HasMaxLength(50).IsRequired();
            entidad.HasIndex(r => r.Nombre).IsUnique();

            entidad.Property(r => r.Descripcion).HasMaxLength(200);
            entidad.Property(r => r.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");

            // No navigation properties declared on Rol in Domain models; nothing to ignore
        });

        modelBuilder.Entity<Permiso>(entidad =>
        {
            entidad.ToTable("Permisos");
            entidad.HasKey(p => p.Id);

            entidad.Property(p => p.Codigo).HasMaxLength(100).IsRequired();
            entidad.HasIndex(p => p.Codigo).IsUnique();

            entidad.Property(p => p.Descripcion).HasMaxLength(200);

            // No navigation properties declared on Permiso in Domain models; nothing to ignore
        });

        // Tabla puente Rol <-> Permiso, clave compuesta, sin Id propio
        modelBuilder.Entity<RolPermiso>(entidad =>
        {
            entidad.ToTable("RolPermisos");
            entidad.HasKey(rp => new { rp.RolId, rp.PermisoId });

            entidad.HasOne<Rol>()
                   .WithMany()
                   .HasForeignKey(rp => rp.RolId)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne<Permiso>()
                   .WithMany()
                   .HasForeignKey(rp => rp.PermisoId)
                   .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Usuario>(entidad =>
        {
            entidad.ToTable("Usuarios");
            entidad.HasKey(u => u.Id);

            entidad.Property(u => u.Email).HasMaxLength(150).IsRequired();
            entidad.HasIndex(u => u.Email).IsUnique();

            // BCrypt, factor >= 12 — la validación de coste se aplica en Application, no en la BD
            entidad.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();

            entidad.Property(u => u.Activo).HasDefaultValue(true);

            // Incrementar para invalidar todas las sesiones activas (ver sección 9. Seguridad)
            entidad.Property(u => u.TokenVersion).HasDefaultValue(1);

            entidad.Property(u => u.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Rol>()
                   .WithMany()
                   .HasForeignKey(u => u.RolId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Usuario does not declare navigation property Empleado in Domain models
        });

        // Un Empleado siempre tiene una cuenta de Usuario (1:1), pero no todo
        // Usuario tiene ficha de Empleado (auditores externos, soporte, etc.)
        modelBuilder.Entity<Empleado>(entidad =>
        {
            entidad.ToTable("Empleados");
            entidad.HasKey(e => e.Id);

            entidad.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entidad.Property(e => e.Apellidos).HasMaxLength(150);
            entidad.Property(e => e.Telefono).HasMaxLength(20);
            entidad.Property(e => e.Direccion).HasMaxLength(250);
            entidad.Property(e => e.Salario).HasColumnType("decimal(10,2)");
            entidad.Property(e => e.FotoUrl).HasMaxLength(300);
            entidad.Property(e => e.Activo).HasDefaultValue(true);

            entidad.HasOne<Usuario>()
                   .WithOne()
                   .HasForeignKey<Empleado>(e => e.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(e => e.UsuarioId).IsUnique().HasDatabaseName("IX_Empleados_UsuarioId");
        });


        // 3. CLIENTES
        modelBuilder.Entity<Cliente>(entidad =>
        {
            entidad.ToTable("Clientes");
            entidad.HasKey(c => c.Id);

            entidad.Property(c => c.Nombre).HasMaxLength(100).IsRequired();
            entidad.Property(c => c.Apellidos).HasMaxLength(150);

            // Sin UNIQUE a nivel de BD: familias/empresas pueden compartir teléfono.
            // La unicidad "blanda" (aviso al usuario) se gestiona en Application.
            entidad.Property(c => c.Telefono).HasMaxLength(20).IsRequired();
            entidad.HasIndex(c => c.Telefono).HasDatabaseName("IX_Clientes_Telefono");

            entidad.Property(c => c.Email).HasMaxLength(150);
            entidad.Property(c => c.Observaciones).HasMaxLength(300);
            entidad.Property(c => c.EsVIP).HasDefaultValue(false);
            entidad.Property(c => c.FechaAlta).HasDefaultValueSql("GETUTCDATE()");
        });


        // 4. MESAS Y ZONAS
        modelBuilder.Entity<Zona>(entidad =>
        {
            entidad.ToTable("Zonas");
            entidad.HasKey(z => z.Id);

            entidad.Property(z => z.Nombre).HasMaxLength(80).IsRequired();
            entidad.Property(z => z.Descripcion).HasMaxLength(200);
            entidad.Property(z => z.Activa).HasDefaultValue(true);

            // Zona does not declare navigation property Mesas in Domain models
        });

        modelBuilder.Entity<Mesa>(entidad =>
        {
            entidad.ToTable("Mesas", tb => tb.HasCheckConstraint(
                "CK_Mesas_Estado",
                "Estado IN ('Libre', 'Ocupada', 'Reservada', 'FueraDeServicio')"));
            entidad.HasKey(m => m.Id);

            entidad.Property(m => m.Numero).HasMaxLength(10).IsRequired();
            entidad.HasIndex(m => m.Numero).IsUnique();

            // enum <-> string: nombres coinciden 1:1 con el CHECK de la BD
            entidad.Property(m => m.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EstadoMesa.Libre);

            entidad.Property(m => m.Activa).HasDefaultValue(true);

            entidad.HasOne<Zona>()
                   .WithMany()
                   .HasForeignKey(m => m.ZonaId)
                   .OnDelete(DeleteBehavior.Restrict);
        });

        // 5. RESERVAS
        modelBuilder.Entity<Reserva>(entidad =>
        {
            // El CHECK de la BD incluye 'NoShow'; EstadoReservas no lo tiene todavía.
            entidad.ToTable("Reservas", tb => tb.HasCheckConstraint(
                "CK_Reservas_Estado",
                "Estado IN ('Pendiente', 'Confirmada', 'Cancelada', 'Completada', 'NoShow')"));
            entidad.HasKey(r => r.Id);

            entidad.Property(r => r.DuracionMinutos).HasDefaultValue(90);

            entidad.Property(r => r.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EstadoReservas.Pendiente);

            entidad.Property(r => r.Observaciones).HasMaxLength(300);
            entidad.Property(r => r.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Cliente>()
                   .WithMany()
                   .HasForeignKey(r => r.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Mesa>()
                   .WithMany()
                   .HasForeignKey(r => r.MesaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(r => r.UsuarioCreaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(r => r.FechaHora).HasDatabaseName("IX_Reservas_FechaHora");
            entidad.HasIndex(r => r.Estado).HasDatabaseName("IX_Reservas_Estado");
            entidad.HasIndex(r => r.ClienteId).HasDatabaseName("IX_Reservas_ClienteId");
        });


        // 6. CATEGORÍAS Y PRODUCTOS
        modelBuilder.Entity<CategoriaProducto>(entidad =>
        {
            entidad.ToTable("CategoriasProducto");
            entidad.HasKey(c => c.Id);

            entidad.Property(c => c.Nombre).HasMaxLength(80).IsRequired();
            entidad.Property(c => c.Orden).HasDefaultValue(0);
            entidad.Property(c => c.Activa).HasDefaultValue(true);

            // CategoriaProducto does not declare navigation property Productos in Domain models
        });

        modelBuilder.Entity<Producto>(entidad =>
        {
            entidad.ToTable("Productos");
            entidad.HasKey(p => p.Id);

            entidad.Property(p => p.Nombre).HasMaxLength(150).IsRequired();
            entidad.Property(p => p.Descripcion).HasMaxLength(400);
            entidad.Property(p => p.Precio).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(p => p.IvaPorcentaje).HasColumnType("decimal(5,2)").HasDefaultValue(10.00m);
            entidad.Property(p => p.ImagenUrl).HasMaxLength(300);
            entidad.Property(p => p.Disponible).HasDefaultValue(true);
            entidad.Property(p => p.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<CategoriaProducto>()
                   .WithMany()
                   .HasForeignKey(p => p.CategoriaId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Producto does not declare navigation property ProductoIngredientes in Domain models
        });


        // 7. INVENTARIO CON LOTES
        // StockActual es un campo desnormalizado mantenido por Application.
        // Debe coincidir con SUM(CantidadRestante) de LotesIngrediente; la
        // vista vw_StockReal permite verificar la coherencia (ver más abajo).
        modelBuilder.Entity<Ingrediente>(entidad =>
        {
            entidad.ToTable("Ingredientes", tb => tb.HasCheckConstraint(
                "CK_Ingredientes_Stock", "StockActual >= 0"));
            entidad.HasKey(i => i.Id);

            entidad.Property(i => i.Nombre).HasMaxLength(120).IsRequired();
            entidad.Property(i => i.UnidadMedida).HasMaxLength(20).IsRequired();
            entidad.Property(i => i.StockActual).HasColumnType("decimal(10,2)").HasDefaultValue(0m);
            entidad.Property(i => i.StockMinimo).HasColumnType("decimal(10,2)").HasDefaultValue(0m);

            entidad.HasOne<Proveedor>()
                   .WithMany()
                   .HasForeignKey(i => i.ProveedorId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Ingrediente does not declare navigation collections in Domain models
        });

        // Cada entrada de mercancía genera un lote propio. Los descuentos
        // aplican FIFO por FechaCaducidad (más próxima primero).
        modelBuilder.Entity<LoteIngrediente>(entidad =>
        {
            entidad.ToTable("LotesIngrediente", tb => tb.HasCheckConstraint(
                "CK_Lotes_Cantidades", "CantidadRestante >= 0 AND CantidadRestante <= Cantidad"));
            entidad.HasKey(l => l.Id);

            entidad.Property(l => l.NumeroLote).HasMaxLength(50);
            entidad.Property(l => l.Cantidad).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(l => l.CantidadRestante).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(l => l.CosteUnitario).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(l => l.FechaEntrada).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Ingrediente>()
                   .WithMany()
                   .HasForeignKey(l => l.IngredienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            // FK añadida tras crear PedidosCompra en el script original
            entidad.HasOne<PedidoCompra>()
                   .WithMany()
                   .HasForeignKey(l => l.PedidoCompraId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(l => l.IngredienteId).HasDatabaseName("IX_LotesIngrediente_IngredienteId");
            entidad.HasIndex(l => l.FechaCaducidad).HasDatabaseName("IX_LotesIngrediente_FechaCaducidad");
            entidad.HasIndex(l => l.PedidoCompraId).HasDatabaseName("IX_LotesIngrediente_PedidoCompraId");
        });

        // Receta de cada producto: conecta ventas con consumo de inventario
        modelBuilder.Entity<ProductoIngrediente>(entidad =>
        {
            entidad.ToTable("ProductoIngredientes");
            entidad.HasKey(pi => new { pi.ProductoId, pi.IngredienteId });

            entidad.Property(pi => pi.Cantidad).HasColumnType("decimal(10,2)").IsRequired();

            entidad.HasOne<Producto>()
                   .WithMany()
                   .HasForeignKey(pi => pi.ProductoId)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne<Ingrediente>()
                   .WithMany()
                   .HasForeignKey(pi => pi.IngredienteId)
                   .OnDelete(DeleteBehavior.Restrict);
        });

        // Histórico auditado de todas las variaciones de stock
        modelBuilder.Entity<MovimientoStock>(entidad =>
        {
            // Tipo no tiene enum propio todavía (Entrada/Salida/Ajuste/Merma):
            // se mapea como string validado por el CHECK de la BD.
            entidad.ToTable("MovimientosStock", tb => tb.HasCheckConstraint(
                "CK_MovStock_Tipo", "Tipo IN ('Entrada', 'Salida', 'Ajuste', 'Merma')"));
            entidad.HasKey(m => m.Id);

            entidad.Property(m => m.Tipo).HasMaxLength(20).IsRequired();
            entidad.Property(m => m.Cantidad).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(m => m.Motivo).HasMaxLength(200);
            entidad.Property(m => m.FechaMovimiento).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Ingrediente>()
                   .WithMany()
                   .HasForeignKey(m => m.IngredienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<LoteIngrediente>()
                   .WithMany()
                   .HasForeignKey(m => m.LoteId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(m => m.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            // FK añadida tras crear PedidoLineas en el script original
            entidad.HasOne<PedidoLinea>()
                   .WithMany()
                   .HasForeignKey(m => m.PedidoLineaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(m => m.IngredienteId).HasDatabaseName("IX_MovimientosStock_IngredienteId");
            entidad.HasIndex(m => m.FechaMovimiento).HasDatabaseName("IX_MovimientosStock_FechaMovimiento");
        });

        // 8. PROVEEDORES Y COMPRAS
        modelBuilder.Entity<Proveedor>(entidad =>
        {
            entidad.ToTable("Proveedores");
            entidad.HasKey(p => p.Id);

            entidad.Property(p => p.Nombre).HasMaxLength(150).IsRequired();
            entidad.Property(p => p.CIF).HasMaxLength(20);
            entidad.Property(p => p.Telefono).HasMaxLength(20);
            entidad.Property(p => p.Email).HasMaxLength(150);
            entidad.Property(p => p.Direccion).HasMaxLength(250);
            entidad.Property(p => p.Activo).HasDefaultValue(true);

            // Proveedor does not declare navigation collections in Domain models
        });

        modelBuilder.Entity<PedidoCompra>(entidad =>
        {
            entidad.ToTable("PedidosCompra", tb => tb.HasCheckConstraint(
                "CK_PedidosCompra_Estado", "Estado IN ('Pendiente', 'Recibido', 'Cancelado')"));
            entidad.HasKey(pc => pc.Id);

            entidad.Property(pc => pc.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EstadoPedidoCompra.Pendiente);

            entidad.Property(pc => pc.FechaPedido).HasDefaultValueSql("GETUTCDATE()");
            entidad.Property(pc => pc.Total).HasColumnType("decimal(10,2)").HasDefaultValue(0m);

            entidad.HasOne<Proveedor>()
                   .WithMany()
                   .HasForeignKey(pc => pc.ProveedorId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(pc => pc.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            // PedidoCompra does not declare Lotes collection in Domain models
        });

        // El CosteUnitario aquí es el precio pactado en el pedido. Al recibir
        // la mercancía, Application crea los lotes usando este valor; si el
        // proveedor cambia el precio en el albarán, esta línea debe
        // actualizarse ANTES de marcar el pedido como Recibido.
        modelBuilder.Entity<PedidoCompraLinea>(entidad =>
        {
            entidad.ToTable("PedidoCompraLineas");
            entidad.HasKey(l => l.Id);

            entidad.Property(l => l.Cantidad).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(l => l.CosteUnitario).HasColumnType("decimal(10,2)").IsRequired();

            entidad.HasOne<PedidoCompra>()
                   .WithMany()
                   .HasForeignKey(l => l.PedidoCompraId)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne<Ingrediente>()
                   .WithMany()
                   .HasForeignKey(l => l.IngredienteId)
                   .OnDelete(DeleteBehavior.Restrict);
        });


        // 9. PEDIDOS, MESAS COMPUESTAS Y COCINA
        modelBuilder.Entity<Pedido>(entidad =>
        {
            // TipoPedido (enum) no coincide con el CHECK de la BD: la BD usa
            // 'Local' en vez de 'ComerAqui', y no tiene 'ParaRecoger'. Revisar
            // antes de persistir — ver aviso al principio del fichero.
            entidad.ToTable("Pedidos", tb =>
            {
                tb.HasCheckConstraint("CK_Pedidos_TipoPedido",
                    "TipoPedido IN ('Local', 'ParaLlevar', 'Domicilio')");
                tb.HasCheckConstraint("CK_Pedidos_Estado",
                    "Estado IN ('Abierto', 'EnCocina', 'Listo', 'Servido', 'Cerrado', 'Cancelado')");
            });
            entidad.HasKey(p => p.Id);

            entidad.Property(p => p.TipoPedido)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(TipoPedido.ComerAqui);

            // Pedidos.Estado no tiene enum propio todavía (Abierto/EnCocina/
            // Listo/Servido/Cerrado/Cancelado): se mapea como string.
            entidad.Property(p => p.Estado).HasMaxLength(20).HasDefaultValue("Abierto");

            entidad.Property(p => p.Observaciones).HasMaxLength(300);
            entidad.Property(p => p.FechaApertura).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Cliente>()
                   .WithMany()
                   .HasForeignKey(p => p.ClienteId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(p => p.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Pedido does not declare PedidoMesas or Lineas in Domain models

            entidad.HasIndex(p => p.Estado).HasDatabaseName("IX_Pedidos_Estado");
        });

        // N:M entre Pedidos y Mesas: permite juntar mesas para grupos grandes.
        // Un pedido puede ocupar varias mesas; una mesa solo un pedido abierto
        // (esa regla de negocio se valida en Application, no en la BD).
        modelBuilder.Entity<PedidoMesa>(entidad =>
        {
            entidad.ToTable("PedidoMesas");
            entidad.HasKey(pm => new { pm.PedidoId, pm.MesaId });

            entidad.HasOne<Pedido>()
                   .WithMany()
                   .HasForeignKey(pm => pm.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne<Mesa>()
                   .WithMany()
                   .HasForeignKey(pm => pm.MesaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(pm => pm.MesaId).HasDatabaseName("IX_PedidoMesas_MesaId");
        });

        // PrecioUnitario e IvaPorcentaje son snapshots fiscales del momento
        // del pedido: un cambio posterior de precio o IVA no altera facturas
        // ya emitidas (patrón Snapshot, ver sección 3.3 de la especificación).
        modelBuilder.Entity<PedidoLinea>(entidad =>
        {
            entidad.ToTable("PedidoLineas", tb => tb.HasCheckConstraint(
                "CK_PedidoLineas_Estado",
                "Estado IN ('Pendiente', 'EnPreparacion', 'Listo', 'Entregado', 'Cancelado')"));
            entidad.HasKey(l => l.Id);

            entidad.Property(l => l.Cantidad).HasDefaultValue(1);
            entidad.Property(l => l.PrecioUnitario).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(l => l.IvaPorcentaje).HasColumnType("decimal(5,2)").IsRequired();

            entidad.Property(l => l.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EstadoLineaPedido.Pendiente);

            entidad.Property(l => l.ObservacionesLinea).HasMaxLength(200);
            entidad.Property(l => l.Prioridad).HasDefaultValue(0);

            entidad.HasOne<Pedido>()
                   .WithMany()
                   .HasForeignKey(l => l.PedidoId)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne<Producto>()
                   .WithMany()
                   .HasForeignKey(l => l.ProductoId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(l => l.Estado).HasDatabaseName("IX_PedidoLineas_Estado");
            entidad.HasIndex(l => l.PedidoId).HasDatabaseName("IX_PedidoLineas_PedidoId");
        });


        // 10. FACTURACIÓN Y CAJA
        modelBuilder.Entity<MetodosPago>(entidad =>
        {
            entidad.ToTable("MetodosPago");
            entidad.HasKey(m => m.Id);
            entidad.Property(m => m.Nombre).HasMaxLength(50).IsRequired();
        });

        modelBuilder.Entity<Factura>(entidad =>
        {
            entidad.ToTable("Facturas", tb => tb.HasCheckConstraint(
                "CK_Facturas_Estado", "Estado IN ('Emitida', 'Anulada')"));
            entidad.HasKey(f => f.Id);

            entidad.Property(f => f.NumeroFactura).HasMaxLength(30).IsRequired();
            entidad.HasIndex(f => f.NumeroFactura).IsUnique();

            entidad.Property(f => f.Subtotal).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(f => f.TotalIva).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(f => f.Total).HasColumnType("decimal(10,2)").IsRequired();

            entidad.Property(f => f.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EstadoFactura.Emitida);

            entidad.Property(f => f.FechaEmision).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Pedido>()
                   .WithMany()
                   .HasForeignKey(f => f.PedidoId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(f => f.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Factura does not declare Pagos collection in Domain models

            entidad.HasIndex(f => f.FechaEmision).HasDatabaseName("IX_Facturas_FechaEmision");
        });

        // La suma de todos los FacturaPagos de una factura debe ser igual a
        // Facturas.Total; esa validación vive en Application antes de insertar.
        modelBuilder.Entity<FacturaPagos>(entidad =>
        {
            entidad.ToTable("FacturaPagos");
            entidad.HasKey(fp => fp.Id);

            entidad.Property(fp => fp.Importe).HasColumnType("decimal(10,2)").IsRequired();

            entidad.HasOne<Factura>()
                   .WithMany()
                   .HasForeignKey(fp => fp.FacturaId)
                   .OnDelete(DeleteBehavior.Cascade);

            entidad.HasOne<MetodosPago>()
                   .WithMany()
                   .HasForeignKey(fp => fp.MetodoPagoId)
                   .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CajaDiaria>(entidad =>
        {
            entidad.ToTable("CajaDiaria", tb => tb.HasCheckConstraint(
                "CK_Caja_Estado", "Estado IN ('Abierta', 'Cerrada')"));
            entidad.HasKey(c => c.Id);

            entidad.HasIndex(c => c.Fecha).IsUnique();

            entidad.Property(c => c.SaldoInicial).HasColumnType("decimal(10,2)").HasDefaultValue(0m);
            entidad.Property(c => c.SaldoFinal).HasColumnType("decimal(10,2)");

            entidad.Property(c => c.Estado)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .HasDefaultValue(EstadoCaja.Abierta);

            entidad.Property(c => c.FechaApertura).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(c => c.UsuarioAperturaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(c => c.UsuarioCierreId)
                   .OnDelete(DeleteBehavior.Restrict);

            // CajaDiaria does not declare Movimientos collection in Domain models
        });

        // Fuente de verdad para el cuadre:
        // SaldoFinal = SaldoInicial + SUM(Ingresos) - SUM(Retiradas)
        modelBuilder.Entity<MovimientosCaja>(entidad =>
        {
            // ⚠ TipoMovimientosCaja (enum) no coincide con el CHECK de la BD:
            // la BD tiene 'Apertura' y 'Cierre' (que el enum no tiene) y el
            // enum tiene 'Cancelar' (que la BD no tiene). Revisar antes de
            // persistir — ver aviso al principio del fichero.
            entidad.ToTable("MovimientosCaja", tb => tb.HasCheckConstraint(
                "CK_MovCaja_Tipo",
                "Tipo IN ('Apertura', 'Ingreso', 'Retirada', 'Correccion', 'Cierre')"));
            entidad.HasKey(m => m.Id);

            entidad.Property(m => m.Tipo)
                   .HasConversion<string>()
                   .HasMaxLength(20)
                   .IsRequired();

            entidad.Property(m => m.Importe).HasColumnType("decimal(10,2)").IsRequired();
            entidad.Property(m => m.Descripcion).HasMaxLength(200);
            entidad.Property(m => m.FechaMovimiento).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<CajaDiaria>()
                   .WithMany()
                   .HasForeignKey(m => m.CajaDiariaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Factura>()
                   .WithMany()
                   .HasForeignKey(m => m.FacturaId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(m => m.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(m => m.CajaDiariaId).HasDatabaseName("IX_MovimientosCaja_CajaDiariaId");
        });

        // 11. NOTIFICACIONES

        modelBuilder.Entity<Notificacion>(entidad =>
        {
            entidad.ToTable("Notificaciones");
            entidad.HasKey(n => n.Id);

            entidad.Property(n => n.Tipo).HasMaxLength(40).IsRequired();
            entidad.Property(n => n.Titulo).HasMaxLength(150).IsRequired();
            entidad.Property(n => n.Mensaje).HasMaxLength(400).IsRequired();
            entidad.Property(n => n.Leida).HasDefaultValue(false);
            entidad.Property(n => n.FechaCreacion).HasDefaultValueSql("GETUTCDATE()");

            // UsuarioId nulo = broadcast a todos los usuarios activos
            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(n => n.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);

            entidad.HasIndex(n => new { n.UsuarioId, n.Leida })
                   .HasDatabaseName("IX_Notificaciones_UsuarioId_Leida");
        });

        // 12. AUDITORÍA
        modelBuilder.Entity<LogsAuditoria>(entidad =>
        {
            entidad.ToTable("LogsAuditoria", tb => tb.HasCheckConstraint(
                "CK_Auditoria_Accion", "Accion IN ('Create', 'Update', 'Delete')"));
            entidad.HasKey(l => l.Id);

            entidad.Property(l => l.Entidad).HasMaxLength(80).IsRequired();
            entidad.Property(l => l.EntidadId).HasMaxLength(40);

            // Accion no tiene enum propio todavía (Create/Update/Delete): string
            entidad.Property(l => l.Accion).HasMaxLength(20).IsRequired();

            entidad.Property(l => l.DatosAnteriores).HasColumnType("nvarchar(max)");
            entidad.Property(l => l.DatosNuevos).HasColumnType("nvarchar(max)");
            entidad.Property(l => l.IP).HasMaxLength(45);
            entidad.Property(l => l.UserAgent).HasMaxLength(300);
            entidad.Property(l => l.FechaAccion).HasDefaultValueSql("GETUTCDATE()");

            entidad.HasOne<Usuario>()
                   .WithMany()
                   .HasForeignKey(l => l.UsuarioId)
                   .OnDelete(DeleteBehavior.Restrict);
        });

        // VISTA DE VERIFICACIÓN vw_StockReal (solo lectura, sin clave propia)
        // Compara Ingredientes.StockActual con SUM(CantidadRestante) de
        // LotesIngrediente activos. Se mapea como Keyless Entity Type.
        modelBuilder.Entity<StockRealVista>(entidad =>
        {
            entidad.HasNoKey();
            entidad.ToView("vw_StockReal");

            entidad.Property(v => v.StockEnTabla).HasColumnType("decimal(10,2)");
            entidad.Property(v => v.StockCalculado).HasColumnType("decimal(10,2)");
            entidad.Property(v => v.Diferencia).HasColumnType("decimal(10,2)");
        });
    }
}